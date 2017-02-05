using System;

using Xamarin.Forms;

namespace RichTextEditor
{
	public class StyleBar : ContentView
	{
		public StackLayout BarLayout = new StackLayout
		{
			Orientation = StackOrientation.Horizontal
		};

		public TestableButton BoldButton = new TestableButton
		{
			Text = "B",
			FontAttributes = FontAttributes.Bold,
			BorderColor = Color.Black,
			BorderWidth = 0.3,
			VerticalOptions = LayoutOptions.Start,
			Margin = new Thickness(10, 0)
		};

		public TestableButton ItalicButton = new TestableButton
		{
			Text = "I",
			FontAttributes = FontAttributes.Italic,
			BorderColor = Color.Black,
			BorderWidth = 0.3,
			VerticalOptions = LayoutOptions.Start,
			Margin = new Thickness(10, 0)
		};

		public TestableButton UnderlineButton = new TestableButton
		{
			Text = "U",
			BorderColor = Color.Black,
			BorderWidth = 0.3,
			VerticalOptions = LayoutOptions.Start,
			Margin = new Thickness(10, 0)
		};

		public StyleBar()
		{
			BarLayout = new StackLayout
			{
				Orientation = StackOrientation.Horizontal,
				Children =
				{
					BoldButton,
					ItalicButton,
					UnderlineButton
				}
			};
			Content = BarLayout;

			ItalicButton.Clicked += (sender, e) => 
			{
				System.Diagnostics.Debug.WriteLine("Italic Button Clicked!");
				var styleArg = new HtmlEditor.StyleArgs("italic");
				MessagingCenter.Send(ItalicButton, "styleClicked", styleArg);
			};

			BoldButton.Clicked += (sender, e) =>
			{
				System.Diagnostics.Debug.WriteLine("Bold Button Clicked!");
				var styleArg = new HtmlEditor.StyleArgs("bold");
				MessagingCenter.Send(BoldButton, "styleClicked", styleArg);
			};

			UnderlineButton.Clicked += (sender, e) =>
			{
				System.Diagnostics.Debug.WriteLine("Underline Button Clicked!");
				var styleArg = new HtmlEditor.StyleArgs("underline");
				MessagingCenter.Send(UnderlineButton, "styleClicked", styleArg);
			};
		}
	}
}

