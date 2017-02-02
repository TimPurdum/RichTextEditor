﻿
using System;
using Foundation;
using UIKit;
using NUnit.Framework;
using Xamarin.Forms;
using RichTextEditor;

namespace RTE.iOS.Test
{
	[TestFixture]
	public class IOSTests
	{

		HtmlEditor OpenNewUIController()
		{
			
			UINavigationController root = (UINavigationController)UIApplication.SharedApplication.KeyWindow.RootViewController;
			var vc = new UIViewController();
			root.PresentViewControllerAsync(vc, true);
			//vc.View.Add(TestEditor);

			return TestEditor;
		}

		string HtmlString = "<p>Hello, <b>Bold</b> <i>italic</i> <b><i>flavor!</i></b></p><p><u>Underline</u> this?</p>";
		HtmlEditor TestEditor = new HtmlEditor();

		[Test]
		public void ConvertHtml()
		{
			NSAttributedString attrString = ConverterIOS.HtmlToAttributedString(HtmlString);
			string returnString = ConverterIOS.AttributedStringToHtml(attrString);
			Console.WriteLine("Return String: " + returnString);
			Assert.True(HtmlString.Equals(returnString));
		}

		[Test]
		public void SetTextInEditor()
		{
			TestEditor.Text = HtmlString;
			Console.WriteLine("Text set: " + TestEditor.Text);
		}

		[Test]
		public void SetHtmlInEditor()
		{
			var testEditor = OpenNewUIController();
			testEditor.SetHtmlText(HtmlString);
			Console.WriteLine("Text set: " + testEditor.Text);
		}

		[Test]
		public void AddBold()
		{
			TestEditor.SetHtmlText(HtmlString);
			TestEditor.SelectionStart = 0;
			TestEditor.SelectionEnd = 4;
			TestEditor.BoldChanged();
		}

		[Test]
		[Ignore("another time")]
		public void Ignore()
		{
			Assert.True(false);
		}
	}
}