namespace AppXamarinTestThemoviedb.Shared.ViewModels
{
	using AppXamarinTestThemoviedb.Models;
	using AppXamarinTestThemoviedb.Shared.Models;
	using AppXamarinTestThemoviedb.ViewModels;

	using Prism.Navigation;

	public class MovieDetailPageViewModel : ViewModelBase
	{
		public MovieDetailPageViewModel(INavigationService navigationService)
		: base(navigationService)
		{
		}

		private MovieDetails movieDetails;

		public MovieDetails MovieDetails
		{
			get { return this.movieDetails; }
			set { this.SetProperty(ref this.movieDetails, value); }
		}

		private string imagePosterFull;

		public string ImagePosterFull
		{
			get { return this.imagePosterFull; }

			set { this.SetProperty(ref this.imagePosterFull, value); }
		}

		private string imageBackGround;

		public string ImageBackGround
		{
			get { return this.imageBackGround; }

			set { this.SetProperty(ref this.imageBackGround, value); }
		}

		public void InitConfigs()
		{
			this.ImagePosterFull = this.MovieDetails.GetImageSize(ImageSize.W1066_and_H600);
			this.ImageBackGround = this.movieDetails.GetImageSize(ImageSize.W300_and_H450);
		}

		public override void OnNavigatedTo(INavigationParameters parameters)
		{
			base.OnNavigatedTo(parameters);
			this.MovieDetails = parameters["movieDetails"] as MovieDetails;
			this.InitConfigs();
		}
	}
}
