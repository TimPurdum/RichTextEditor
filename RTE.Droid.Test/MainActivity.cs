using System.Reflection;
using RichTextEditor;
using Android.App;
using Android.OS;
using Xamarin.Android.NUnitLite;

namespace RTE.Droid.Test
{
	[Activity(Label = "RTE.Droid.Test", MainLauncher = true)]
	public class MainActivity : TestSuiteActivity
	{
		protected override void OnCreate(Bundle bundle)
		{
			RegisterEditorDroid register = new RegisterEditorDroid();
			// tests can be inside the main assembly
			AddTest(Assembly.GetExecutingAssembly());
			// or in any reference assemblies
			// AddTest (typeof (Your.Library.TestClass).Assembly);

			// Once you called base.OnCreate(), you cannot add more assemblies.
			base.OnCreate(bundle);
		}
	}
}
