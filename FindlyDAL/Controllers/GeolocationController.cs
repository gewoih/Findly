using FindlyDAL.Contexts;
using FindlyLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FindlyDAL.Controllers
{
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
		public async Task<Dictionary<Guid, UserGeolocation?>> GetUsersGeolocations([FromHeader] Guid[] usersGuids)
		{
			var usersGeolocations = new Dictionary<Guid, UserGeolocation?>();

			foreach (var userGuid in usersGuids)
			{
				var geolocation = await _geolocationDbContext.UsersGeolocations.FirstOrDefaultAsync(ug => ug.UserId == userGuid);
				usersGeolocations.Add(userGuid, geolocation);
			}

			return usersGeolocations;
		}
	}
}
