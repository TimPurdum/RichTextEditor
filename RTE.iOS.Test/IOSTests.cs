
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
		string HtmlString = "<p>Hello, <b>Bold</b> <i>italic</i> <b><i>flavor!</i></b></p><p><u>Underline</u> this?</p>";
		HtmlEditor TestEditor;
		StyleBar TestStyleBar;

		public IOSTests()
		{
		}

		[SetUp]
		public void SetupBeforeTest()
		{
			TestEditor = new HtmlEditor();

			TestStyleBar = new StyleBar();

			System.Diagnostics.Debug.WriteLine("Setting up test!");

		}

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
			TestEditor.SetHtmlText(HtmlString);
			Console.WriteLine("Text set: " + TestEditor.Text);
		}

		[Test]
		public void AddBold()
		{
			TestEditor.SetHtmlText(HtmlString);
			TestEditor.SetSelection(0, 5);
			TestStyleBar.BoldButton.TestClick();
			var result = TestEditor.GetHtmlText();
			var expected = "<p><b>Hello</b>, <b>Bold</b> <i>italic</i> <b><i>flavor!</i></b></p><p><u>Underline</u> this?</p>";
			Assert.AreEqual(expected, result);
		}

		[Test]
		public void RemoveBold()
		{
			TestEditor.SetHtmlText(HtmlString);
			TestEditor.SetSelection(7, 11);
			TestStyleBar.BoldButton.TestClick();
			var result = TestEditor.GetHtmlText();
			var expected = "<p>Hello, Bold <i>italic</i> <b><i>flavor!</i></b></p><p><u>Underline</u> this?</p>";
			Assert.AreEqual(expected, result);
		}

		[Test]
		public void AddItalics()
		{
			TestEditor.SetHtmlText(HtmlString);
			TestEditor.SetSelection(7, 11);
			TestStyleBar.ItalicButton.TestClick();
			var result = TestEditor.GetHtmlText();
			var expected = "<p>Hello, <b><i>Bold</i></b> <i>italic</i> <b><i>flavor!</i></b></p><p><u>Underline</u> this?</p>";
			Assert.AreEqual(expected, result);
		}

		[Test]
		public void RemoveItalics()
		{
			TestEditor.SetHtmlText(HtmlString);
			TestEditor.SetSelection(19, 26);
			TestStyleBar.ItalicButton.TestClick();
			var result = TestEditor.GetHtmlText();
			var expected = "<p>Hello, <b>Bold</b> <i>italic</i> <b>flavor!</b></p><p><u>Underline</u> this?</p>";
			Assert.AreEqual(expected, result);
		}

		[Test]
		public void AddUnderline()
		{
			TestEditor.SetHtmlText(HtmlString);
			TestEditor.SetSelection(0, 5);
			TestStyleBar.UnderlineButton.TestClick();
			var result = TestEditor.GetHtmlText();
			var expected = "<p><u>Hello</u>, <b>Bold</b> <i>italic</i> <b><i>flavor!</i></b></p><p><u>Underline</u> this?</p>";
			Assert.AreEqual(expected, result);
		}

		[Test]
		public void RemoveUnderline()
		{
			TestEditor.SetHtmlText(HtmlString);
			TestEditor.SetSelection(27, 36);
			TestStyleBar.UnderlineButton.TestClick();
			var result = TestEditor.GetHtmlText();
			var expected = "<p>Hello, <b>Bold</b> <i>italic</i> <b><i>flavor!</i></b></p><p>Underline this?</p>";
			Assert.AreEqual(expected, result);
		}

		[Test]
		public void ChangeAWord()
		{
			TestEditor.SetHtmlText(HtmlString);
		}

		[Test]
		public void SendStyleFromStyleBar()
		{
			var styleBar = new StyleBar();
			TestEditor.SetHtmlText(HtmlString);

		}
	}
}
