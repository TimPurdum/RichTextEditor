
using System;
using Foundation;
using UIKit;
using NUnit.Framework;
using RichTextEditor;

namespace RTE.iOS.Test
{
	[TestFixture]
	public class MyTest
	{
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
		public void AddBold()
		{
			TestEditor.SetHtmlText(HtmlString);
			//var control = (UITextView)TestEditor;
			Assert.False(true);
		}

		[Test]
		[Ignore("another time")]
		public void Ignore()
		{
			Assert.True(false);
		}
	}
}
