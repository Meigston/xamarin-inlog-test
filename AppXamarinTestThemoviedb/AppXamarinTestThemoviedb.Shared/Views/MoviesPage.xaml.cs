namespace AppXamarinTestThemoviedb.Shared.Views
{
	using AppXamarinTestThemoviedb.Shared.ViewModels;

	using Xamarin.Forms;

	public partial class MoviesPage : ContentPage
	{
		public MoviesPage()
		{
			InitializeComponent();
		}

		private void InputView_OnTextChanged(object sender, TextChangedEventArgs e)
		{
			if (!string.IsNullOrWhiteSpace(e.NewTextValue) && e.NewTextValue.Length >= 3)
			{
				(this.BindingContext as MainPageViewModel)?.SearchMoviesCommand.Execute();
			}
		}
	}
}
