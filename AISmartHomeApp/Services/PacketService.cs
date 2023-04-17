using System.Net;
using System.Net.Sockets;
using System.Text;
using MediatR;
using NetCoreServer;
using Services;

class PacketServer : UdpServer
{
    private ILogger<PacketServer> _logger;
    private AISService _service;

    public PacketServer(ILogger<PacketServer> logger, AISService service) : base(IPAddress.Any, 10110)
    {
        _logger = logger;
        _service = service;
    }

    protected override void OnStarted()
        {
            // Start receive datagrams
            _logger.LogInformation("Starting the Packet Server");
            ReceiveAsync();
            _logger.LogInformation("Started the Packet Server");
        }   

    
    protected override void OnReceived(EndPoint endpoint, byte[] buffer, long offset, long size)
    {

        try {
            var tmpMsg = Encoding.ASCII.GetString(buffer);
            var thing = tmpMsg.Trim().Replace("\n","").Replace("\r","").Replace("\0", "");
            Task.Run(() => BackgroundOperationAsync(thing));
            
            
            _logger.LogInformation(thing);
        } catch (Exception ex){
            _logger.LogError(1240, ex, "Problem with Packet Received");
        } 
        ReceiveAsync();        
    }

    protected override void OnError(SocketError error)
    {
        Console.WriteLine($"Echo UDP server caught an error with code {error}");
    }

    public async Task BackgroundOperationAsync(string thing)
    {
        _logger.LogWarning($"Background info -{thing}");
        await _service.Do(thing);

    }

    protected override void Dispose(bool disposingManagedResources) { _logger.LogInformation("Dispose");}
    protected override void OnSent(EndPoint endpoint, long sent) { _logger.LogInformation("OnSent");}
    protected override void OnStarting() { _logger.LogInformation("OnStarting");}
    protected override void OnStopped() { _logger.LogInformation("OnStopped");}
    protected override void OnStopping() { _logger.LogInformation("OnStopping");}
}
