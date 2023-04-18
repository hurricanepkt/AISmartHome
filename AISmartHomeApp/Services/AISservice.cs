using AisParser;
using Database;

namespace Services;

public class AISService {


    private Parser _parser;
    private IVesselRepository _repo;
    private ILogger<AISService> _logger;

    public AISService(Parser parser, IVesselRepository ctx, ILogger<AISService> logger)
    {
        _parser = parser;
        _repo = ctx;
        _logger = logger;
    }


    public async Task Do(string message)
    {
        try {
            var msg = message.Replace("\n", "").Replace("\r", "");
            _logger.LogInformation(msg);
            var translated = _parser.Parse(msg);
            var vessel = await GetOrCreate(translated.Mmsi);
            Copy(vessel, translated.GetType(), translated);           
            
        } catch (Exception ex) {
            _logger.LogError(ex, "Exception occurred on Parse / Save");
        }
    }

    private async Task<Vessel> GetOrCreate(uint mmsi) {
        Vessel? blah = await _repo.FindByMmsi(mmsi);
        Vessel ret = new Vessel() { Mmsi = mmsi};
        if (blah is null) {            
            await _repo.Add(ret);
            await _repo.Save();
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