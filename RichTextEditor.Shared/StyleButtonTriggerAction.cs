using System;
using Xamarin.Forms;
namespace RichTextEditor
{
	public class StyleButtonTriggerAction : TriggerAction<Button>
	{
		protected override void Invoke(Button sender)
		{
			System.Diagnostics.Debug.WriteLine("Button Invoked!");
		}
	}
}
