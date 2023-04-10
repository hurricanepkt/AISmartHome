using Database;
using Microsoft.Extensions.Caching.Memory;

public class ReadOnlyDBRepo
{
    private readonly FileContext _ctx;
    private readonly ILogger<ReadOnlyDBRepo> _logger;
    private readonly IMemoryCache _memoryCache;

    public ReadOnlyDBRepo(FileContext ctx, ILogger<ReadOnlyDBRepo> logger, IMemoryCache memoryCache)
    {
        _ctx = ctx;
        _logger = logger;
        _memoryCache = memoryCache;
    }
    private MemoryCacheEntryOptions _options = new MemoryCacheEntryOptions() {
        SlidingExpiration = TimeSpan.FromSeconds(5)
    };
    private static readonly object CacheLockObject = new object();

    public List<Vessel> GetVessels()
    {
        _logger.LogInformation("ReadOnlyDBRepo:GetVessels");
        string cacheKey = "GetVessels";
        var cachedValue = _memoryCache.Get<List<Vessel>>(cacheKey);
        if (cachedValue != null)
        {
            return cachedValue;
        }

        lock (CacheLockObject)
        {
            _logger.LogInformation("Caching " + cacheKey);
            _memoryCache.Set<List<Vessel>>(cacheKey, _ctx.Vessels.ToList(), _options);
        }
        return _memoryCache.Get<List<Vessel>>(cacheKey) ?? new List<Vessel>();
    }
}