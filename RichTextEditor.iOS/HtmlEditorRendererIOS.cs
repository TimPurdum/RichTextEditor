using System;
using Foundation;
using UIKit;
using RichTextEditor;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using System.ComponentModel;

[assembly: ExportRenderer(typeof(HtmlEditor), typeof(HtmlEditorRendererIOS))]
namespace RichTextEditor
{
	public class HtmlEditorRendererIOS : EditorRenderer
	{
		HtmlEditor ThisEditor;
		NSMutableAttributedString AttributedText;

		public HtmlEditorRendererIOS()
		{
			System.Diagnostics.Debug.WriteLine("Renderer Started!");
		}

		protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
		{
			System.Diagnostics.Debug.WriteLine("Element Changed Called in Renderer!");

			base.OnElementChanged(e);
			if (e.NewElement != null)
			{
				System.Diagnostics.Debug.WriteLine("Registering Event Handlers!");
				ThisEditor = (HtmlEditor)e.NewElement;
				ThisEditor.HtmlRequested += OnHtmlRequested;
				ThisEditor.HtmlSet += OnHtmlSet;
				ThisEditor.StyleChangeRequested += OnStyleChangeRequested;
			}
			if (e.OldElement != null)
			{
				var oldEditor = (HtmlEditor)e.OldElement;
				oldEditor.HtmlRequested -= OnHtmlRequested;
				oldEditor.HtmlSet -= OnHtmlSet;
				oldEditor.StyleChangeRequested -= OnStyleChangeRequested;
			}
		}

		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);
			System.Diagnostics.Debug.WriteLine("Property change sent: " + e.PropertyName);
			SetSelection(Control.SelectedRange);
		}

		private void SetSelection(NSRange selection)
		{
			ThisEditor.SelectionStart = (int)selection.Location;
			ThisEditor.SelectionEnd = (int)(selection.Location + selection.Length);
		}

		private void OnHtmlRequested(object sender, EventArgs e)
		{
			var editor = (HtmlEditor)sender;
			editor.SetHtmlText(ConverterIOS.AttributedStringToHtml(Control.AttributedText));
		}

		private void OnHtmlSet(object sender, HtmlEditor.HtmlArgs e)
		{
			var htmlString = e.HtmlToPass;
			Control.AttributedText = ConverterIOS.HtmlToAttributedString(htmlString);
		}

		private void OnStyleChangeRequested(object sender, HtmlEditor.StyleArgs e)
		{
			AttributedText = (NSMutableAttributedString)Control.AttributedText;
			Console.WriteLine("Style Change Requested!");

			if (e.Style == "bold")
			{
				var fontAttr = UIFontDescriptorSymbolicTraits.Bold;
				UpdateStyleAttributes(fontAttr);
			}
			else if (e.Style == "italic")
			{
				var fontAttr = UIFontDescriptorSymbolicTraits.Italic;
				UpdateStyleAttributes(fontAttr);
			}
			else if (e.Style == "underline")
			{
				var underlineAttr = UIStringAttributeKey.UnderlineStyle;
				UpdateUnderlineAttributes();
			}
		}

		void UpdateStyleAttributes(UIFontDescriptorSymbolicTraits fontAttr)
		{
			var selectionRange = Control.SelectedRange;
			bool hasFlag = false;

			AttributedText.EnumerateAttribute(UIStringAttributeKey.Font, selectionRange, NSAttributedStringEnumeration.LongestEffectiveRangeNotRequired, (NSObject value, NSRange range, ref bool stop) =>
			{
				if (value != null)
				{
					var font = (UIFont)value;
					var descriptor = font.FontDescriptor;

					if (descriptor.SymbolicTraits.HasFlag(fontAttr))
					{
						hasFlag = true;
						AttributedText.RemoveAttribute(UIStringAttributeKey.Font, range);

						var newFont = UIFont.SystemFontOfSize(font.PointSize);
						if (fontAttr == UIFontDescriptorSymbolicTraits.Bold && descriptor.SymbolicTraits.HasFlag(UIFontDescriptorSymbolicTraits.Italic))
						{
							newFont = UIFont.ItalicSystemFontOfSize(font.PointSize);
						}
						else if (fontAttr == UIFontDescriptorSymbolicTraits.Italic && descriptor.SymbolicTraits.HasFlag(UIFontDescriptorSymbolicTraits.Bold))
						{
							newFont = UIFont.BoldSystemFontOfSize(font.PointSize);
						}
						AttributedText.AddAttribute(UIStringAttributeKey.Font, newFont, range);
					}
				}
			});

			if (!hasFlag)
			{
				
				AttributedText.EnumerateAttribute(UIStringAttributeKey.Font, selectionRange, NSAttributedStringEnumeration.None, (NSObject value, NSRange range, ref bool stop) =>
				{
					if (value != null)
					{
						var font = (UIFont)value;
						var descriptor = font.FontDescriptor;
						var traits = descriptor.SymbolicTraits;
						var newDescriptor = descriptor.CreateWithTraits(fontAttr);
						var newFont = UIFont.FromDescriptor(newDescriptor, font.PointSize);
						AttributedText.RemoveAttribute(UIStringAttributeKey.Font, range);
						AttributedText.AddAttribute(UIStringAttributeKey.Font, newFont, range);
					}
				});
			}
			SaveChanges(selectionRange);
		}

		void UpdateUnderlineAttributes()
		{
			var selectionRange = Control.SelectedRange;
			bool hasFlag = false;

			AttributedText.EnumerateAttribute(UIStringAttributeKey.UnderlineStyle, selectionRange, NSAttributedStringEnumeration.None, (NSObject value, NSRange range, ref bool stop) =>
			{
				if (value != null)
				{
					var style = (NSString)value;
					if (style == "Single")
					{
						hasFlag = true;
						AttributedText.RemoveAttribute(UIStringAttributeKey.UnderlineStyle, range);
					}
				}
			});

			if (!hasFlag)
			{
				AttributedText.AddAttribute(UIStringAttributeKey.UnderlineStyle, (NSString)"Single", selectionRange);
			}

			SaveChanges(selectionRange);
		}

		void SaveChanges(NSRange selectionRange)
		{
			Control.AttributedText = AttributedText;
			Control.BecomeFirstResponder();
			Control.SelectedRange = selectionRange;
		}
	}
}
