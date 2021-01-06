namespace AppXamarinTestThemoviedb.Shared
{
	using AppXamarinTestThemoviedb.Services;
	using AppXamarinTestThemoviedb.Services.Interfaces;
	using AppXamarinTestThemoviedb.Shared.Services;
	using AppXamarinTestThemoviedb.Shared.Services.Interfaces;
	using AppXamarinTestThemoviedb.Shared.ViewModels;
	using AppXamarinTestThemoviedb.Shared.Views;

	using Microsoft.AppCenter;
	using Microsoft.AppCenter.Analytics;
	using Microsoft.AppCenter.Crashes;

	using Prism;
	using Prism.Ioc;

	using Xamarin.Essentials.Implementation;
	using Xamarin.Essentials.Interfaces;
	using Xamarin.Forms;

	using DeviceXforms = Xamarin.Forms.Device;
	using MainPage = AppXamarinTestThemoviedb.Shared.Views.MainPage;



	public partial class App
	{
		public App(IPlatformInitializer initializer)
			: base(initializer)
		{
		}

		protected override async void OnInitialized()
		{
			DeviceXforms.SetFlags(new[] { "Shapes_Experimental", "Brush_Experimental" });
			InitializeComponent();
			XF.Material.Forms.Material.Init(this);

			AppCenter.Start(
				"ios=58700844-1f8b-4af7-b72c-5b5d043c4873;" + "android=48a21bd8-5bfc-4cf8-86d6-e694520f7b32",
				typeof(Analytics),
				typeof(Crashes));
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
