namespace AppMServiceTestInlog
{
	using AppMServiceTestInlog.Services;
	using AppMServiceTestInlog.Services.Interfaces;
	using AppMServiceTestInlog.ViewModels;
	using AppMServiceTestInlog.Views;

	using Prism;
	using Prism.Ioc;

	using Xamarin.Essentials.Implementation;
	using Xamarin.Essentials.Interfaces;
	using Xamarin.Forms;

	public partial class App
	{
		public App(IPlatformInitializer initializer)
			: base(initializer)
		{
		}

		protected override async void OnInitialized()
		{
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
