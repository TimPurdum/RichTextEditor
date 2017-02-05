using System;
using Xamarin.Forms;
using RichTextEditor;

[assembly: Xamarin.Forms.Dependency(typeof(RegisterEditorDroid))]
namespace RichTextEditor
{
	public class RegisterEditorDroid
	{
		public RegisterEditorDroid()
		{
			MessagingCenter.Subscribe<HtmlEditor>(this, "register", (editor) =>
			{
				var renderer = new HtmlEditorRendererDroid();
				renderer.SetElement(editor);
			});

			MessagingCenter.Subscribe<TestableButton>(this, "register", (button) =>
			{
				var renderer = new ButtonCustomRendererDroid();
				renderer.SetElement(button);
			});
		}
	}
}
