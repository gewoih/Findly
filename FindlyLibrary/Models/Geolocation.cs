namespace FindlyLibrary.Models
{
	public sealed class Geolocation
	{
		public int Id { get; set; }
		public double Latitude { get; set; }
		public double Longitude { get; set; }

		public override bool Equals(object? obj)
		{
			return obj is Geolocation geolocation &&
				   Latitude == geolocation.Latitude &&
				   Longitude == geolocation.Longitude;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(Latitude, Longitude);
		}
	}
}
