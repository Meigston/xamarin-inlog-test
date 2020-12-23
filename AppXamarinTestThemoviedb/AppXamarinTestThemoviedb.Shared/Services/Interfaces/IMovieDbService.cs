namespace AppXamarinTestThemoviedb.Shared.Services.Interfaces
{
	using System.Collections.ObjectModel;
	using System.Threading.Tasks;

	using AppXamarinTestThemoviedb.Models;
	using AppXamarinTestThemoviedb.Services.Enums;

	using AppXamarinTestThemoviedb.Shared.Models;

	public interface IMovieDbService
	{
		Task<ObservableCollection<Movie>> GetMovies(string search = "", int page = 1, LanguageResearch languageResearch = LanguageResearch.PtBR);

		Task<MovieDetails> GetMovieDetails(int idMovie, LanguageResearch languageResearch = LanguageResearch.PtBR);
	}
}
