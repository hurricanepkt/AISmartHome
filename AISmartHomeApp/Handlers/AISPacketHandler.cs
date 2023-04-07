using System.Text.Json;
using AisParser;
using AisParser.Messages;
using Database;
using MediatR;

public class AISPacketHandler : INotificationHandler<AISNotification>
{
    // private readonly IEnumerable<IMessageStrategy> _service;
    private Parser _parser;
    private FileContext _ctx;
    private ILogger<AISPacketHandler> _logger;

    public AISPacketHandler(Parser parser, FileContext ctx, ILogger<AISPacketHandler> logger)
    {
        _parser = parser;
        _ctx = ctx;
        _logger = logger;
    }

    async Task INotificationHandler<AISNotification>.Handle(AISNotification notification, CancellationToken cancellationToken)
    {
        try {
            _logger.LogInformation(notification.Message);
            var translated = _parser.Parse(notification.Message);
            var vessel = await GetOrCreate(translated.Mmsi);
            Copy(vessel, translated.GetType(), translated);
        } catch (Exception ex) {
            _logger.LogError(ex, "Handle Notification");
        }
    }

    private async Task<Vessel> GetOrCreate(uint mmsi) {
        Vessel? blah = await _ctx.Vessels.FindAsync(mmsi);
        Vessel ret = new Vessel() { Mmsi = mmsi};
        if (blah is null) {            
            await _ctx.Vessels.AddAsync(ret);
            await _ctx.SaveChangesAsync();
        }    
        return blah ?? ret;
    
    }
    
    public static void Copy(Vessel vessel, Type childType, Object child)
    {   
        var parentProperties = vessel.GetType().GetProperties();
        var childProperties = childType.GetProperties();
        foreach (var childProperty in childProperties)
        {   
            try {                
                var pt = parentProperties.First(f=> f.Name == childProperty.Name);
                pt.SetValue(vessel, childProperty.GetValue(child));
            }
            catch(Exception ex)
            {
                throw new Exception($"Cant set property {childProperty.Name} with {childProperty.PropertyType}", ex);
            }
        }
    }
}
