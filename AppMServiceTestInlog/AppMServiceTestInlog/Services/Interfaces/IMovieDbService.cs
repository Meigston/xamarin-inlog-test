namespace AppMServiceTestInlog.Services.Interfaces
{
	using System.Collections.ObjectModel;
	using System.Threading.Tasks;

	using AppMServiceTestInlog.Models;
	using AppMServiceTestInlog.Services.Enums;

	public interface IMovieDbService
	{
		Task<ObservableCollection<Movie>> GetMovies(string search = "", int page = 1, LanguageResearch languageResearch = LanguageResearch.PtBR);

		Task<MovieDetails> GetMovieDetails(int idMovie, LanguageResearch languageResearch = LanguageResearch.PtBR);
	}
}
