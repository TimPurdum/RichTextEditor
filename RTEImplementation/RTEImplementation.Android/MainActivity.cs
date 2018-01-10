using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using RichTextEditor;
using Android.Content;

namespace RTEImplementation.Droid
{
	[Activity (Label = "RTEImplementation", Icon = "@drawable/icon", Theme="@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
        Context context;

		protected override void OnCreate (Bundle bundle)
		{
		    context = Application.Context;
			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar; 

			base.OnCreate (bundle);

			Xamarin.Forms.Forms.Init (this, bundle);
			LoadApplication (new RTEImplementation.App ());
		}
	}
}

