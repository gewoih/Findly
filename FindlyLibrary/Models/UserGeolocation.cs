namespace FindlyLibrary.Models
{
	public sealed class UserGeolocation
	{
		public int Id { get; set; }
		public Guid UserId { get; set; }
		public Geolocation Geolocation { get; set; }

		public override bool Equals(object? obj)
		{
			return obj is UserGeolocation geolocation &&
				   UserId.Equals(geolocation.UserId) &&
				   EqualityComparer<Geolocation>.Default.Equals(Geolocation, geolocation.Geolocation);
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(UserId, Geolocation);
		}
	}
}
