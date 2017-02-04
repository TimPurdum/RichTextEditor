using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace RichTextEditor
{
	public partial class RichTextBox : ContentView
	{
		public HtmlEditor TextEditor;

		public RichTextBox()
		{
			InitializeComponent();
			RegisterButtons();
			HideButtons();
			TextEditor = ContentEditor;

			TextEditor.Focused += (sender, e) =>
			{
				ShowButtons();
			};
			TextEditor.Unfocused += (sender, e) =>
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
				TextEditor.BoldChanged();
			};

			Italic.Clicked += (sender, e) =>
			{
				TextEditor.ItalicChanged();
			};

			Underline.Clicked += (sender, e) =>
			{
				TextEditor.UnderlineChanged();
			};

			HTMLButton.Clicked += (sender, e) =>
			{
				TextEditor.GetHtmlText();
			};
		}
	}
}
