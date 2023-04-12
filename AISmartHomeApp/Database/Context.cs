using System.ComponentModel.DataAnnotations;
using kDg.FileBaseContext.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Database
{
    public class Context : DbContext
    {
        public DbSet<Vessel> Vessels { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Default: JSON-Serializer
            optionsBuilder.UseFileBaseContextDatabase(location: "/Data");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vessel>()
                .ToTable("vessels");
        }
    }


}