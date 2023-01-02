using FindlyLibrary.Models;
using Newtonsoft.Json;
using System.Text;

namespace FindlyApp.Services
{
	public static class UserGeolocationUtils
	{
		private readonly static HttpClient _httpClient = new();

		public static async Task UpdateUserGeolocation(Guid userId, Geolocation newGeolocation)
		{
			var content = new StringContent(JsonConvert.SerializeObject(newGeolocation), Encoding.UTF8, "application/json");
			var result = await _httpClient.PutAsync($"https://localhost:7290/update_user_geolocation?userId={userId}", content);

			if (result.IsSuccessStatusCode)
			{
				Console.WriteLine($"[{DateTime.Now}]: Geolocation updated for user {userId}");
			}
			else
			{
				Console.WriteLine($"[{DateTime.Now}]: Error while updating geolocation for user {userId} - {result}");
			}
		}
	}
}
