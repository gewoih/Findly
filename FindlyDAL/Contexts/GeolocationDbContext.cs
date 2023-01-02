using FindlyLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace FindlyDAL.Contexts
{
	public class GeolocationDbContext : DbContext
	{
		public DbSet<UserGeolocation> UsersGeolocations { get; set; }

		public GeolocationDbContext(DbContextOptions options) : base(options) { }
	}
}
