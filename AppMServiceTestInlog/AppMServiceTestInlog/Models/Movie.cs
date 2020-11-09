namespace AppMServiceTestInlog.Models
{
	using System;

	using AppMServiceTestInlog.Extensions;
	using AppMServiceTestInlog.Resources.Values;

	using Newtonsoft.Json;

	public class Movie
	{
		[JsonProperty("id")]
		public int Id { get; set; }

		[JsonProperty("title")]
		public string Title { get; set; }

		[JsonProperty("release_date")]
		public string ReleaseDate { get; set; }

		[JsonProperty("vote_average")]
		public float VoteAverage { get; set; }

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

		private string _posterPath;

		[JsonProperty("poster_path")]
		public string PosterPath
		{
			get
			{
				return $"{Constants.UrlImage}{ImageSize.W300_and_H450.GetDescription()}{this._posterPath}";
			}

			set
			{
				this._posterPath = value;
			}
		}
	}
}
