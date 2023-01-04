using FindlyDAL.Contexts;
using FindlyLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FindlyDAL.Controllers
{
	[Route("api/[controller]")]
	public class GeolocationController : Controller
	{
		private readonly GeolocationDbContext _geolocationDbContext;

		public GeolocationController(GeolocationDbContext geolocationDbContext)
		{
			_geolocationDbContext = geolocationDbContext;
		}

		[HttpPut]
		[Route("update_user_geolocation")]
		public async Task UpdateUserGeolocation(Guid userId, [FromBody] Geolocation newGeolocation)
		{
			var userGeolocation = await _geolocationDbContext.UsersGeolocations.Include(ug => ug.Geolocation).FirstOrDefaultAsync(g => g.UserId == userId);

			if (userGeolocation == null)
			{
				var newUserGeolocation = new UserGeolocation()
				{
					UserId = userId,
					Geolocation = newGeolocation
				};

				await _geolocationDbContext.UsersGeolocations.AddAsync(newUserGeolocation);
			}
			else
			{
				userGeolocation.Geolocation.Latitude = newGeolocation.Latitude;
				userGeolocation.Geolocation.Longitude = newGeolocation.Longitude;
			}

			await _geolocationDbContext.SaveChangesAsync();
		}

		[HttpGet]
		[Route("get_users_geolocations")]
		public async Task<List<UserGeolocation>> GetUsersGeolocations([FromBody] Guid[] usersGuids)
		{
			var usersGeolocations = new List<UserGeolocation>();

			foreach (var userGuid in usersGuids)
			{
				var userGeolocation = await _geolocationDbContext.UsersGeolocations
					.AsNoTracking()
					.Include(ug => ug.Geolocation)
					.FirstOrDefaultAsync(ug => ug.UserId == userGuid);

				var newUserGeolocation = new Geolocation
				{
					Latitude = 0,
					Longitude = 0,
				};

				if (userGeolocation is not null && userGeolocation.Geolocation is not null)
				{
					newUserGeolocation.Latitude = userGeolocation.Geolocation.Latitude;
					newUserGeolocation.Longitude = userGeolocation.Geolocation.Longitude;
				}

				usersGeolocations.Add(new UserGeolocation
				{
					UserId = userGuid,
					Geolocation = newUserGeolocation
				});
			}

			return usersGeolocations;
		}
	}
}
