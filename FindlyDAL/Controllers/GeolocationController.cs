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
		public async Task UpdateUserGeolocation(Guid userId, [FromBody] Geolocation geolocation)
		{
			var userGeolocation = await _geolocationDbContext.UsersGeolocations.FirstOrDefaultAsync(g => g.UserId == userId);

			if (userGeolocation == null)
			{
				var newUserGeolocation = new UserGeolocation()
				{
					UserId = userId,
					Geolocation = geolocation
				};

				await _geolocationDbContext.UsersGeolocations.AddAsync(newUserGeolocation);
			}
			else
			{
				userGeolocation.Geolocation = geolocation;
				
				_geolocationDbContext.UsersGeolocations.Update(userGeolocation);
			}


			await _geolocationDbContext.SaveChangesAsync();
		}
	}
}
