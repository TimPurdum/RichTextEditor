using System;
using Android.Content;
using Xamarin.Forms;
using RichTextEditor;
using Application = Android.App.Application;

[assembly: Xamarin.Forms.Dependency(typeof(RegisterEditorDroid))]
namespace RichTextEditor
{
	public class RegisterEditorDroid
	{
		public RegisterEditorDroid(Context context)
		{
			MessagingCenter.Subscribe<HtmlEditor>(this, "register", (editor) =>
			{
				var renderer = new HtmlEditorRendererDroid(context);
				renderer.SetElement(editor);
			});

			MessagingCenter.Subscribe<TestableButton>(this, "register", (button) =>
			{
				var renderer = new ButtonCustomRendererDroid(context);
				renderer.SetElement(button);
			});
		}
	}
}
