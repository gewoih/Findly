using FindlyLibrary.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;

namespace FindlyApp.Services
{
	public static class UserGeolocationUtils
	{
		private readonly static HttpClient _httpClient = new();

		public static async Task UpdateUserGeolocationInDatabase(Guid userId, Geolocation newGeolocation)
		{
			var content = new StringContent(JsonConvert.SerializeObject(newGeolocation), Encoding.UTF8, "application/json");
			var result = await _httpClient.PutAsync($"https://localhost:7290/api/geolocation/update_user_geolocation?userId={userId}", content);

			if (result.IsSuccessStatusCode)
			{
				Console.WriteLine($"[{DateTime.Now}]: Geolocation updated for user {userId}");
			}
			else
			{
				Console.WriteLine($"[{DateTime.Now}]: Error while updating geolocation for user {userId} - {result}");
			}
		}

		public static async Task<List<UserGeolocation>> GetUserFriendsGeolocations(Guid userId)
		{
			var result = await _httpClient.GetAsync($"https://localhost:7290/api/users/get_user_friends?userId={userId}");
			var friendsIds = JsonConvert.DeserializeObject<List<User>>(await result.Content.ReadAsStringAsync()).Select(u => new Guid(u.Id));

			var message = new HttpRequestMessage
			{
				Method = HttpMethod.Get,
				RequestUri = new Uri("https://localhost:7290/api/geolocation/get_users_geolocations"),
				Content = new StringContent(JsonConvert.SerializeObject(friendsIds), Encoding.UTF8, "application/json")
			};

			result = await _httpClient.SendAsync(message);

			var friendsGeolocations = JsonConvert.DeserializeObject<List<UserGeolocation>>(await result.Content.ReadAsStringAsync());
			return friendsGeolocations;
		}
	}
}
