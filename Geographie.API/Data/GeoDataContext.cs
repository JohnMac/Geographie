using Geographie.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Geographie.API.Data
{
    public class GeoDataContext : DbContext
    {
        public GeoDataContext(DbContextOptions<GeoDataContext> options) : base(options) { }
        public DbSet<GeographieData> Geographies { get; set; }
    }
}
