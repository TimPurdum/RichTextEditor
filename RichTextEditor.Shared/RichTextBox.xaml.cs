using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace RichTextEditor
{
	public partial class RichTextBox : ContentView
	{
		public RichTextBox()
		{
			InitializeComponent();
			RegisterButtons();
			HideButtons();

			TextField.Focused += (sender, e) =>
			{
				ShowButtons();
			};
			TextField.Unfocused += (sender, e) =>
			{
				HideButtons();
			};
		}

		void HideButtons()
		{
			StyleRow.IsVisible = false;
			this.ForceLayout();
		}

		void ShowButtons()
		{
			StyleRow.IsVisible = true;
			this.ForceLayout();
		}

		void RegisterButtons()
		{
			Bold.Clicked += (sender, e) =>
			{
				System.Diagnostics.Debug.WriteLine("Bold Clicked!");
				TextField.BoldChanged();
			};

			Italic.Clicked += (sender, e) =>
			{
				TextField.ItalicChanged();
			};

			Underline.Clicked += (sender, e) =>
			{
				TextField.UnderlineChanged();
			};
		}
	}
}
