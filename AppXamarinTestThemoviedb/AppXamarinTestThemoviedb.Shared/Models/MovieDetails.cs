namespace AppXamarinTestThemoviedb.Shared.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using AppXamarinTestThemoviedb.Extensions;
	using AppXamarinTestThemoviedb.Models;
	using AppXamarinTestThemoviedb.Shared.Resources.Values;

	using Newtonsoft.Json;

	public class MovieDetails
	{
		[JsonProperty("id")]
		public int Id { get; set; }

		[JsonProperty("title")]
		public string Title { get; set; }

		[JsonProperty("release_date")]
		public string ReleaseDate { get; set; }

		public string ReleaseDateFormat
		{
			get
			{
				DateTime date;
				if (DateTime.TryParse(this.ReleaseDate, out date))
				{
					return string.Format("{0:dd} de {0:MMMM} de {0:yyyy}", date);
				}

				return "Não definido.";
			}
		}

		[JsonProperty("vote_average")]
		public float VoteAverage { get; set; }

		[JsonProperty("backdrop_path")]
		public string BackdropPath { get; set; }

		[JsonProperty("overview")]
		public string Overview { get; set; }

		[JsonProperty("budget")]
		public decimal Budget { get; set; }

		[JsonProperty("genres")]
		public List<MovieGenre> Genres { get; set; }

		public string GenresJoin => string.Join(", ", this.Genres.Select(s => s.Name));

		[JsonProperty("revenue")]
		public decimal Revenue { get; set; }

		[JsonProperty("runtime")]
		public int Runtime { get; set; }

		public string GetImageSize(ImageSize size)
		{
			return $"{Constants.UrlImage}{size.GetDescription()}{this.BackdropPath}";
		}
	}
}
