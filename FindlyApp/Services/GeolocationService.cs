using Microsoft.JSInterop;
using System.Globalization;
using System.Text.Json.Nodes;

namespace FindlyApp.Services
{
    public sealed class GeolocationService : IDisposable
    {
        public Guid _userId { get; private set; }
        public double _currentLatitude { get; private set; }
        public double _currentLongitude { get; private set; }
        public Func<double, double, Task> OnGeolocationChanged;
        private bool _isServiceWorking = true;

		public void StartUpdatingUserGeolocation(Guid userId, IJSRuntime jsRuntime)
        {
            _userId = userId;

            ThreadPool.QueueUserWorkItem(async (object obj) =>
            {
				await jsRuntime.InvokeAsync<JsonArray>("watchGeolocation");

				while (_isServiceWorking)
                {
                    await UpdateGeolocationAsync(jsRuntime);
                    await Task.Delay(5000);
                }
            });
        }
		
        private async Task UpdateGeolocationAsync(IJSRuntime jsRuntime)
        {
            try
            {
                var newLocation = await jsRuntime.InvokeAsync<JsonArray>("getGeolocation");

                var newLatitude = double.Parse(newLocation[0].ToString(), CultureInfo.InvariantCulture);
                var newLongitude = double.Parse(newLocation[1].ToString(), CultureInfo.InvariantCulture);

                if (_currentLatitude != newLatitude || _currentLongitude != newLongitude)
                {
                    _currentLatitude = newLatitude;
                    _currentLongitude = newLongitude;

                    OnGeolocationChanged?.Invoke(_currentLatitude, _currentLongitude);
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
