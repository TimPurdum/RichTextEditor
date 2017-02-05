using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using UIKit;
using RichTextEditor;

[assembly: ExportRenderer(typeof(RichTextBox), typeof(RichTextBoxRendererIOS))]
namespace RichTextEditor
{
	public class RichTextBoxRendererIOS : ViewRenderer<ContentView, UIView>
	{
		public RichTextBoxRendererIOS()
		{
		}

		protected override void OnElementChanged(ElementChangedEventArgs<ContentView> e)
		{
			base.OnElementChanged(e);
			if (e.NewElement != null)
			{
				var box = (RichTextBox)e.NewElement;
				var editorRenderer = new HtmlEditorRendererIOS();
				editorRenderer.SetElement(box.TextEditor);
			}
		}
	}
}
