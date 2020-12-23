namespace AppXamarinTestThemoviedb.Shared.Services
{
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Net.Http;
	using System.Threading.Tasks;

	using Acr.UserDialogs;

	using AppXamarinTestThemoviedb.Extensions;
	using AppXamarinTestThemoviedb.Services;
	using AppXamarinTestThemoviedb.Services.Enums;
	using AppXamarinTestThemoviedb.Services.Interfaces;

	using AppXamarinTestThemoviedb.Shared.Models;
	using AppXamarinTestThemoviedb.Shared.Resources.Messages;
	using AppXamarinTestThemoviedb.Shared.Resources.Values;
	using AppXamarinTestThemoviedb.Shared.Services.Interfaces;

	public class MovieDbService : IMovieDbService
	{
		private readonly IHttpServices httpServices;
		private readonly string apiKey;

		public MovieDbService()
		{
			this.httpServices = new HttpServices(Constants.UrlApiMovies, MediaTypeHeader.ApplicationJson);
			this.apiKey = Constants.ApiKeyMovies;
		}

		public async Task<ObservableCollection<Movie>> GetMovies(string search = "", int page = 1, LanguageResearch languageResearch = LanguageResearch.PtBR)
		{
			try
			{
				var queryParams = new Dictionary<string, object>();
				queryParams.Add("api_key", this.apiKey);
				queryParams.Add("language", languageResearch.GetDescription());
				queryParams.Add("page", page);

				Movies result;
				if (string.IsNullOrEmpty(search))
				{
					////result = await this.httpServices.Get<Movies>("movie/upcoming", queryParams);
					result = await this.httpServices.Get<Movies>("/movie/now_playing", queryParams);
					return result.MovieList.ToObservableCollection();
				}

				queryParams.Add("query", search);
				result = await this.httpServices.Get<Movies>("search/movie", queryParams);
				return result.MovieList.ToObservableCollection();

			}
			catch (Exception e)
			{
				await this.ExcepitonMessage(e);
				return new ObservableCollection<Movie>();
			}
		}

		public async Task<MovieDetails> GetMovieDetails(int idMovie, LanguageResearch languageResearch = LanguageResearch.PtBR)
		{
			try
			{
				var queryParams = new Dictionary<string, object>();
				queryParams.Add("api_key", this.apiKey);
				queryParams.Add("language", languageResearch.GetDescription());

				var result = await this.httpServices.Get<MovieDetails>($"movie/{idMovie}".ToString(), queryParams);
				return result;
			}
			catch (Exception e)
			{
				await this.ExcepitonMessage(e);
				return new MovieDetails();
			}
		}

		private async Task ExcepitonMessage(Exception e)
		{
			Console.WriteLine(e);
			if (typeof(HttpRequestException) == e.GetType())
			{
				await UserDialogs.Instance.AlertAsync(MessagesUI.Connection_Faild);
				return;
			}

			await UserDialogs.Instance.AlertAsync(MessagesUI.Unavaliable_Service);
		}
	}
}
