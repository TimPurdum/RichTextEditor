using System;
using Foundation;
using UIKit;
namespace RichTextEditor
{
	/*public class HtmlEditorControllerIOS : HtmlEditorController
	{
		NSAttributedString AttributedText;
		UITextView Control;

		public HtmlEditorControllerIOS(HtmlEditor editor)
		{
			Control = editor;
		}

		public void BoldClicked()
		{
			
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
	}*/
}
