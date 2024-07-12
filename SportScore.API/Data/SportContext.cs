using Microsoft.EntityFrameworkCore;
using SportScore.API.Models;

namespace SportScore.API.Data
{
    public class SportContext : DbContext
    {
        protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "SportDb");
        }
        public DbSet<SportMatch> Matches { get; set; }
    }
}
