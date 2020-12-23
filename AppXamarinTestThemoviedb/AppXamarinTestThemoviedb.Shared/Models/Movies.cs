namespace AppXamarinTestThemoviedb.Shared.Models
{
	using System.Collections.Generic;

	using Newtonsoft.Json;

	[JsonObject("results")]
	public class Movies
	{
		[JsonProperty("results")]
		public IEnumerable<Movie> MovieList { get; set; }
	}
}
