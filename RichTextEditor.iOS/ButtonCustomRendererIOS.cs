using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using RichTextEditor;

[assembly: ExportRenderer(typeof(TestableButton), typeof(ButtonCustomRendererIOS))]
namespace RichTextEditor
{
	public class ButtonCustomRendererIOS : ButtonRenderer
	{
		public ButtonCustomRendererIOS()
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
					Control.SendActionForControlEvents(UIKit.UIControlEvent.TouchUpInside);
				};
			}

		}
	}
}
