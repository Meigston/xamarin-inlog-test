namespace AppMServiceTestInlog.ViewModels
{
	using Acr.UserDialogs;

	using AppMServiceTestInlog.Models;
	using AppMServiceTestInlog.Services.Interfaces;

	using Prism.Commands;
	using Prism.Navigation;

	using Xamarin.Forms.Extended;

	public class MainPageViewModel : ViewModelBase
	{
		private readonly IMovieDbService movieDbService;

		public MainPageViewModel(INavigationService navigationService, IMovieDbService movieDbService)
			: base(navigationService)
		{
			this.movieDbService = movieDbService;
			Title = "Ultimos Lançamentos";

			this._isBusy = false;
			DefineInfiniteScroll();
			this.SearchMoviesCommand.Execute();
		}


		private InfiniteScrollCollection<Movie> _movies;

		public InfiniteScrollCollection<Movie> Movies
		{
			get { return this._movies; }
			set { SetProperty(ref this._movies, value); }
		}

		private string _searchText;

		public string SearchText
		{
			get => this._searchText;
			set => SetProperty(ref this._searchText, value);
		}

		private bool _isBusy;

		public bool IsBusy
		{
			get => this._isBusy;
			set => SetProperty(ref this._isBusy, value);
		}

		private int page = 1;

		public DelegateCommand SearchMoviesCommand
		{
			get
			{
				return new DelegateCommand(async () =>
				{
					this.IsBusy = true;
					UserDialogs.Instance.ShowLoading("Buscando Filmes...");
					this.page = 1;
					this.Movies.Clear();
					this.Movies.AddRange(await this.movieDbService.GetMovies(search: SearchText, page: this.page));

					UserDialogs.Instance.HideLoading();
					this.IsBusy = false;
				});
			}
		}

		public DelegateCommand<Movie> DetailsMovie
		{
			get
			{
				return new DelegateCommand<Movie>(async (movie) =>
				{
					UserDialogs.Instance.ShowLoading("Carregando Detalhes...");
					var movieDetails = await this.movieDbService.GetMovieDetails(movie.Id);
					var parameters = new NavigationParameters();
					parameters.Add("movieDetails", movieDetails);

					UserDialogs.Instance.HideLoading();
					await this.NavigationService.NavigateAsync("MovieDetailPage", parameters);
				});
			}
		}

		public void DefineInfiniteScroll()
		{
			this.Movies = new InfiniteScrollCollection<Movie>()
			{
				OnLoadMore = async () =>
				{
					this.IsBusy = true;
					this.page++;

					var movies = await this.movieDbService.GetMovies(search: SearchText, page: this.page);
					this.IsBusy = false;

					return movies;
				}
			};
		}
	}
}