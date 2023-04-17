using System.ComponentModel.DataAnnotations;
using Infrastructure;
using kDg.FileBaseContext.Extensions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Database
{
    public class Context : DbContext
    {
        private readonly ILogger<Context> logger;

        public Context(DbContextOptions<Context> options, ILogger<Context> logger) : base(options)
        {
            logger.LogInformation("Context_ctor_start");
            this.logger = logger;
            logger.LogInformation("Context_ctor_end");
        }
        public DbSet<Vessel> Vessels { get; set; }
        public DbSet<AppInfo> AppInfos {get; set;}


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            logger.LogInformation("OnConfiguring_start");          
            switch(TheConfiguration.DatabaseType) {
                case DatabaseType.FileSystem:
                    Setup_FileSystem(optionsBuilder);
                    break;
                case DatabaseType.SqlLiteInMemory:
                    Setup_SqlLiteInMemory(optionsBuilder);
                    break;
            }               
            logger.LogInformation("OnConfiguring_end");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            logger.LogInformation("OnModelCreating_start");
            modelBuilder.Entity<Vessel>()
                .ToTable("vessels");
            logger.LogInformation("OnModelCreating_end");
        }

        private void Setup_FileSystem(DbContextOptionsBuilder opt) {
            logger.LogInformation("Setup_FileSystem_start");
            opt.UseFileBaseContextDatabase(location: "/Data");
            logger.LogInformation("Setup_FileSystem_end");
        }

        private void Setup_SqlLiteInMemory(DbContextOptionsBuilder opt) {
            logger.LogInformation("Setup_SqlLiteInMemory_start");
            var _connection = new SqliteConnection("Data Source=InMemorySample;Mode=Memory;Cache=Shared");
            _connection.Open();
            opt.UseSqlite(connection: _connection);
            logger.LogInformation("Setup_SqlLiteInMemory_end");
        }
    }



}

public class AppInfo {
    [Key]
    public int Id {get; set;}
    public string Version {get; set;}  = "not set";
}