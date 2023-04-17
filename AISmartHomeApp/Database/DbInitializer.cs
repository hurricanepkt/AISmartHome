using Infrastructure;

namespace Database;
public static class DbInitializer
{
    private static ILogger _logger { get => StaticLoggerFactory.GetStaticLogger<Initializer>(); }
    public static void Initialize(Context context)
    {
        _logger.LogInformation("DbInitializer Initialize");
        context.Database.EnsureCreated();
        _logger.LogInformation("DbInitializer Created");
        if (context.AppInfos.Any()) {
            _logger.LogInformation("DbInitializer already seeded");
            return;
        }
        _logger.LogInformation("DbInitializer seeding");
        context.AppInfos.Add(new AppInfo {
            Version = "tbd"
        });
        _logger.LogInformation("DbInitializer saviing");
        context.SaveChanges();
        _logger.LogInformation("DbInitializer saved");
    }
}
public class Initializer { }