namespace AppXamarinTestThemoviedb.Shared
{
	using AppXamarinTestThemoviedb.Services;
	using AppXamarinTestThemoviedb.Services.Interfaces;
	using AppXamarinTestThemoviedb.Shared.Services;
	using AppXamarinTestThemoviedb.Shared.Services.Interfaces;
	using AppXamarinTestThemoviedb.Shared.ViewModels;
	using AppXamarinTestThemoviedb.Shared.Views;

	using Prism;
	using Prism.Ioc;

	using Xamarin.Essentials.Implementation;
	using Xamarin.Essentials.Interfaces;
	using Xamarin.Forms;

	using MainPage = AppXamarinTestThemoviedb.Shared.Views.MainPage;

	public partial class App
	{
		public App(IPlatformInitializer initializer)
			: base(initializer)
		{
		}

		protected override async void OnInitialized()
		{
			Device.SetFlags(new[] { "Shapes_Experimental", "Brush_Experimental" });
			InitializeComponent();
			XF.Material.Forms.Material.Init(this);
#if DEBUG
			HotReloader.Current.Run(this);
#endif

			await NavigationService.NavigateAsync("NavigationPage/MainPage");
		}

		protected override void RegisterTypes(IContainerRegistry containerRegistry)
		{
			containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();

			containerRegistry.RegisterForNavigation<NavigationPage>();
			containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
			containerRegistry.RegisterForNavigation<MovieDetailPage, MovieDetailPageViewModel>();
			this.RegisterServices(containerRegistry);
		}

		private void RegisterServices(IContainerRegistry containerRegistry)
		{
			containerRegistry.Register<IHttpServices, HttpServices>();
			containerRegistry.Register<IMovieDbService, MovieDbService>();
		}
	}
}
