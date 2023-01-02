namespace FindlyLibrary.Models
{
	public sealed class UserGeolocation
	{
		public int Id { get; set; }
		public Guid UserId { get; set; }
		public Geolocation Geolocation { get; set; }
	}
}
