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
		UIFont CurrentFont;
		NSMutableDictionary CurrentTypingAttributes = new NSMutableDictionary();
		bool CurrentUnderline;
		bool ChangeUnderline;

		public HtmlEditorRendererIOS()
		{
			System.Diagnostics.Debug.WriteLine("Renderer Started!");
		}

		protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
		{
			System.Diagnostics.Debug.WriteLine("Renderer Element Changed.");

			base.OnElementChanged(e);
			if (e.NewElement != null)
			{
				System.Diagnostics.Debug.WriteLine("Registering Event Handlers!");
				ThisEditor = (HtmlEditor)e.NewElement;
				Control.ScrollEnabled = false;
				ThisEditor.HtmlRequested += OnHtmlRequested;
				ThisEditor.HtmlSet += OnHtmlSet;
				ThisEditor.SelectionChangeHandler += OnSelectionChanged;
				ThisEditor.StyleChangeRequested += OnStyleChangeRequested;
			}
			if (e.OldElement != null)
			{
				var oldEditor = (HtmlEditor)e.OldElement;
				oldEditor.HtmlRequested -= OnHtmlRequested;
				oldEditor.HtmlSet -= OnHtmlSet;
				oldEditor.SelectionChangeHandler -= OnSelectionChanged;
				oldEditor.StyleChangeRequested -= OnStyleChangeRequested;
			}
		}

		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);
			System.Diagnostics.Debug.WriteLine("Property change sent: " + e.PropertyName);
			if(e.PropertyName.Equals("Text"))
			{
				Control.SizeToFit();
			}
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
			AttributedText = new NSMutableAttributedString(Control.AttributedText);
			if (Control.TypingAttributes != null)
			{
				CurrentTypingAttributes = new NSMutableDictionary(Control.TypingAttributes);
			}
			System.Diagnostics.Debug.WriteLine("Rendering Style Change: " + e.Style);

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
			SaveChanges(Control.SelectedRange);
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
						CurrentFont = newFont;
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
						var newTraits = (UIFontDescriptorSymbolicTraits)((uint)fontAttr + (uint)traits);
						var newDescriptor = descriptor.CreateWithTraits(newTraits);
						var newFont = UIFont.FromDescriptor(newDescriptor, font.PointSize);
						AttributedText.RemoveAttribute(UIStringAttributeKey.Font, range);
						AttributedText.AddAttribute(UIStringAttributeKey.Font, newFont, range);
						CurrentFont = newFont;
					}
				});
			}
		}

		void UpdateUnderlineAttributes()
		{
			var selectionRange = Control.SelectedRange;
			bool hasFlag = false;

			AttributedText.EnumerateAttribute(UIStringAttributeKey.UnderlineStyle, selectionRange, NSAttributedStringEnumeration.LongestEffectiveRangeNotRequired, (NSObject value, NSRange range, ref bool stop) =>
			{
				if (value != null)
				{
					var style = (NSNumber)value;
					if (style.Equals(1))
					{
						hasFlag = true;
						AttributedText.RemoveAttribute(UIStringAttributeKey.UnderlineStyle, range);
						CurrentUnderline = false;
					}
				}
			});

			if (!hasFlag)
			{
				AttributedText.AddAttribute(UIStringAttributeKey.UnderlineStyle, (NSNumber)1, selectionRange);
				CurrentUnderline = true;
			}
			ChangeUnderline = true;
		}

		void SaveChanges(NSRange selectionRange)
		{
			Control.AttributedText = AttributedText;
			Control.BecomeFirstResponder();
			SetTypingAttributes();
			Control.SelectedRange = selectionRange;
		}

		void OnSelectionChanged(object sender, HtmlEditor.SelectionArgs args)
		{
			var start = args.Start;
			var end = args.End;
			Control.SelectedRange = new NSRange(start, (end - start));
			ThisEditor.SelectionStart = start;
			ThisEditor.SelectionEnd = end;
		}

		void SetTypingAttributes()
		{
			if (CurrentFont != null)
			{
				var name = CurrentFont.Name;
				CurrentTypingAttributes[UIStringAttributeKey.Font] = CurrentFont;
				if (ChangeUnderline)
				{
					if (CurrentUnderline)
					{
						CurrentTypingAttributes[UIStringAttributeKey.UnderlineStyle] = (NSNumber)1;
					}
					else
					{
						CurrentTypingAttributes[UIStringAttributeKey.UnderlineStyle] = (NSNumber)0;
					}
					ChangeUnderline = false;
				}
				Control.TypingAttributes = CurrentTypingAttributes;
			}
		}
	}
}
