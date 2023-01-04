using FindlyLibrary.Models;
using Microsoft.JSInterop;
using System.Globalization;
using System.Text.Json.Nodes;

namespace FindlyApp.Services
{
    public sealed class GeolocationService : IDisposable
    {
        public Guid UserId { get; private set; }
        public Geolocation Geolocation { get; private set; } = new();
        public List<UserGeolocation> FriendsGeolocations { get; private set; } = new();
        public Func<double, double, Task> OnUserGeolocationChanged;
        public Func<Task> OnFriendsGeolocationChanged;

        private bool _isServiceWorking = true;

		public void StartUpdatingUserGeolocation(Guid userId, IJSRuntime jsRuntime)
        {
            UserId = userId;

            ThreadPool.QueueUserWorkItem(async (object obj) =>
            {
				await jsRuntime.InvokeAsync<JsonArray>("watchGeolocation");

				while (_isServiceWorking)
                {
                    await UpdateUserGeolocationAsync(jsRuntime);
                    await UpdateFriendsGeolocationsAsync(jsRuntime);
                    await Task.Delay(5000);
                }
            });
        }

		private async Task UpdateFriendsGeolocationsAsync(IJSRuntime jsRuntime)
		{
            var newFriendsGeolocations = await UserGeolocationUtils.GetUserFriendsGeolocations(UserId);
            var updatedFriendsGeolocations = newFriendsGeolocations.Except(FriendsGeolocations);

            if (updatedFriendsGeolocations.Any())
            {
                FriendsGeolocations = newFriendsGeolocations;
                OnFriendsGeolocationChanged?.Invoke();
            }
		}

		private async Task UpdateUserGeolocationAsync(IJSRuntime jsRuntime)
        {
            try
            {
                var newLocation = await jsRuntime.InvokeAsync<JsonArray>("getGeolocation");

                var newLatitude = double.Parse(newLocation[0].ToString(), CultureInfo.InvariantCulture);
                var newLongitude = double.Parse(newLocation[1].ToString(), CultureInfo.InvariantCulture);

                if (Geolocation.Latitude != newLatitude || Geolocation.Longitude != newLongitude)
                {
                    Geolocation.Latitude = newLatitude;
                    Geolocation.Longitude = newLongitude;

                    await UserGeolocationUtils.UpdateUserGeolocationInDatabase(UserId, Geolocation);
                    OnUserGeolocationChanged?.Invoke(Geolocation.Latitude, Geolocation.Longitude);
                }
            }
            catch (JSDisconnectedException exception)
            {
                Console.WriteLine(exception.ToString());

                Dispose();
            }
        }

        public void Dispose()
		{
            _isServiceWorking = false;
		}
    }
}
