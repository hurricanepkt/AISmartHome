using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Services;

public class HighPerformanceServer : BackgroundService 
{
    private const int PacketSize = 1380; 
    private static readonly IPEndPoint _blankEndpoint = new IPEndPoint(IPAddress.Any, 0);
    private readonly ILogger<HighPerformanceServer> _logger;
    private readonly IServiceProvider _service;

    public HighPerformanceServer(ILogger<HighPerformanceServer> logger, IServiceProvider service)
    {
        _logger = logger;
        _service = service;
    }    


    private async Task DoReceiveAsync(Socket udpSocket, CancellationToken cancelToken)
    {
        // Taking advantage of pre-pinned memory here using the .NET5 POH (pinned object heap).
        byte[] buffer = GC.AllocateArray<byte>(length: 512, pinned: true);
        Memory<byte> bufferMem = buffer.AsMemory();
        using var scope = _service.CreateScope();
        var aisService = scope.ServiceProvider.GetService<AISService>();

        while (!cancelToken.IsCancellationRequested)
        {
            try
            {

                var result = await udpSocket.ReceiveFromAsync(bufferMem, SocketFlags.None, _blankEndpoint);
                var tmpMsg = Encoding.ASCII.GetString(bufferMem.Slice(0, result.ReceivedBytes).ToArray());
                ArgumentNullException.ThrowIfNull(aisService);
                await aisService.Do(tmpMsg);
                
                
            }
            catch (SocketException exception)
            {
                _logger.LogError(14214, exception, "Do Receive Socket Exception");
                // Socket exception means we are finished.
            }
        }
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var udpSocket = new Socket(SocketType.Dgram, ProtocolType.Udp);
        udpSocket.Bind(new IPEndPoint(IPAddress.Any, 10110));

        Console.WriteLine("Listening on 0.0.0.0:10110");
        await DoReceiveAsync(udpSocket, stoppingToken);
    }
}