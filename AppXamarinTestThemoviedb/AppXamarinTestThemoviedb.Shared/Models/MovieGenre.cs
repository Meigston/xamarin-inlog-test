namespace AppXamarinTestThemoviedb.Models
{
	using Newtonsoft.Json;

	public class MovieGenre
	{
		[JsonProperty("id")]
		public int Id { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }
	}
}
