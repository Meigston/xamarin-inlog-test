﻿namespace AppXamarinTestThemoviedb.Droid
{
	using Acr.UserDialogs;

	using Android.App;
	using Android.Content.PM;
	using Android.Gms.Ads;
	using Android.OS;

	using AppXamarinTestThemoviedb.Shared;

	using FFImageLoading.Forms.Platform;

	using Prism;
	using Prism.Ioc;

	[Activity(Label = "CineLançamentos",
		Theme = "@style/MainTheme",
				ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize,
				ScreenOrientation = ScreenOrientation.Portrait | ScreenOrientation.Nosensor)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar;

			base.OnCreate(savedInstanceState);

			global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
			global::Xamarin.Forms.FormsMaterial.Init(this, savedInstanceState);
			UserDialogs.Init(this);
			CachedImageRenderer.Init(null);
			////AnimationViewRenderer.Init();
			MobileAds.Initialize(ApplicationContext);
			this.LoadApplication(new App(new AndroidInitializer()));
		}

		public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
		{
			Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

			base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
		}
	}

	public class AndroidInitializer : IPlatformInitializer
	{
		public void RegisterTypes(IContainerRegistry containerRegistry)
		{
			// Register any platform specific implementations
		}
	}
}

