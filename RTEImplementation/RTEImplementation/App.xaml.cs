using RichTextEditor;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace RTEImplementation
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();

			MainPage = new RTEImplementation.MainPage();
		}

		protected override void OnStart ()
		{
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
