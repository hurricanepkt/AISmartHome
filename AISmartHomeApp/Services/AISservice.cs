using System.Net;
using System.Net.Sockets;
using System.Text;
using MediatR;

public class AISservice  : BackgroundService
{
    private readonly IMediator _mediator;
    public AISservice(IMediator mediator)
    {
        _mediator = mediator;
    }
    private int listenPort = 10110;
    protected override async Task ExecuteAsync(CancellationToken cancellationToken) 
    {
        UdpClient listener = new UdpClient(listenPort);
        IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, listenPort);
        try
        {
            while(true) {
                var data = (await listener.ReceiveAsync()).Buffer;
                await _mediator.Publish(new AISNotification(data), cancellationToken);
            }
        }
        catch (SocketException e)
        {
            Console.WriteLine(e);
        }
        finally
        {
            listener.Close();
        }
    }
}
public class AISNotification : INotification
{
    public AISNotification(byte[] data)
    {
        Data = data;
        var tmpMsg = Encoding.ASCII.GetString(data);
        Message = tmpMsg.Replace("\n","").Replace("\r","");
    }
    public byte[] Data {get; set;}
    public string Message {get;set;}
}
