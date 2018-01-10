using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using RichTextEditor;
using Android.Content;

[assembly: ExportRenderer(typeof(TestableButton), typeof(ButtonCustomRendererDroid))]
namespace RichTextEditor
{
	public class ButtonCustomRendererDroid : ButtonRenderer
	{
        public ButtonCustomRendererDroid(Context context) : base(context)
        {
        }

	    public ButtonCustomRendererDroid()
	    {

	    }

        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
		{
			base.OnElementChanged(e);

			if (e.NewElement != null)
			{
				var button = (TestableButton)e.NewElement;
				button.TestClickHandler += (sender, f) =>
				{
					Control.PerformClick();
				};
			}

		}
	}
}
