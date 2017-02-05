using System;
using Xamarin.Forms;

namespace RichTextEditor
{
	public class RegisterEditorIOS : RegisterEditor
	{
		public RegisterEditorIOS()
		{
			MessagingCenter.Subscribe<HtmlEditor>(this, "register", (editor) =>
			{
				var renderer = new HtmlEditorRendererIOS();
				renderer.SetElement(editor);


			});

			MessagingCenter.Subscribe<TestableButton>(this, "register", (button) =>
			{
				var renderer = new ButtonCustomRendererIOS();
				renderer.SetElement(button);
			});
		}
	}
}
