namespace AppMServiceTestInlog.Droid
{
	using Android.Animation;
	using Android.App;
	using Android.Content;
	using Android.Content.PM;
	using Android.OS;
	using Android.Views.Animations;

	using Com.Airbnb.Lottie;

	[Activity(Theme = "@style/Theme.Splash",
			  MainLauncher = true,
			  NoHistory = true,
			  ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
			  ScreenOrientation = ScreenOrientation.Portrait | ScreenOrientation.Nosensor)]
	public class SplashActivity : Activity, Animator.IAnimatorListener
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.Activity_Splash);

			var animationView = FindViewById<LottieAnimationView>(Resource.Id.animation_splash);
			animationView.Scale = 0.5f;
			animationView.AddAnimatorListener(this);
		}

		// Launches the startup task
		protected override void OnResume()
		{
			base.OnResume();
		}

		public void OnAnimationCancel(Animator animation)
		{
		}

		public void OnAnimationEnd(Animator animation)
		{
			StartActivity(typeof(MainActivity));
		}

		public void OnAnimationRepeat(Animator animation)
		{
		}

		public void OnAnimationStart(Animator animation)
		{
		}
	}
}
