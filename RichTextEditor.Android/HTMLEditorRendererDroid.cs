using System;
using Android.Text.Style;
using Android.Graphics;
using Android.Views;
using RichTextEditor;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Text;
using System.ComponentModel;

[assembly: ExportRenderer(typeof(HtmlEditor), typeof(HtmlEditorRendererDroid))]
namespace RichTextEditor
{
	public class HtmlEditorRendererDroid : EditorRenderer
	{
		HtmlEditor ThisEditor;
		IEditable EditableText;

		public HtmlEditorRendererDroid()
		{
			
		}

		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			SetSelection();
		}

		private void SetSelection()
		{
			ThisEditor.SelectionStart = Control.SelectionStart;
			ThisEditor.SelectionEnd = Control.SelectionEnd;
		}

		protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
		{
			System.Diagnostics.Debug.WriteLine("Renderer Created");
			Console.WriteLine("Element Changed Called in Renderer!");

			base.OnElementChanged(e);
			if (e.NewElement != null)
			{
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

		private void OnHtmlRequested(object sender, EventArgs e)
		{
			var editor = (HtmlEditor)sender;
			editor.SetHtmlText(ConverterDroid.SpannedToHtml(Control.EditableText));
		}

		private void OnHtmlSet(object sender, HtmlEditor.HtmlArgs e)
		{
			var htmlString = e.HtmlToPass;
			Control.TextFormatted = ConverterDroid.HtmlToSpanned(htmlString);
		}

		private void OnStyleChangeRequested(object sender, HtmlEditor.StyleArgs e)
		{
			EditableText = Control.EditableText;
			Console.WriteLine("Style Change Requested!");

			if (e.Style == "bold")
			{
				var flag = TypefaceStyle.Bold;
				UpdateStyleSpans(flag);
			}
			else if (e.Style == "italic")
			{
				var flag = TypefaceStyle.Italic;
				UpdateStyleSpans(flag);
			}
			else if (e.Style == "underline")
			{
				UpdateUnderlineSpans();
			}
		}

		void UpdateStyleSpans(TypefaceStyle flagStyle)
		{
			var selectionStart = Control.SelectionStart;
			var selectionEnd = Control.SelectionEnd;
			var styleSpans = EditableText.GetSpans(selectionStart, selectionEnd, Java.Lang.Class.FromType(typeof(StyleSpan)));
			bool hasFlag = false;
			var spanType = SpanTypes.InclusiveInclusive;

			foreach (StyleSpan span in styleSpans)
			{
				var spanStart = EditableText.GetSpanStart(span);
				var spanEnd = EditableText.GetSpanEnd(span);
				var newStart = spanStart;
				var newEnd = spanEnd;
				var startsBefore = false;
				var endsAfter = false;

				if (spanStart < selectionStart)
				{
					newStart = selectionStart;
					startsBefore = true;
				}
				if (spanEnd > selectionEnd)
				{
					newEnd = selectionEnd;
					endsAfter = true;
				}

				if (span.Style == flagStyle)
				{
					hasFlag = true;
					EditableText.RemoveSpan(span);
					EditableText.SetSpan(new StyleSpan(TypefaceStyle.Normal), newStart, newEnd, spanType);
				}
				else if (span.Style == TypefaceStyle.BoldItalic)
				{
					hasFlag = true;
					EditableText.RemoveSpan(span);
					var flagLeft = TypefaceStyle.Bold;
					if (flagStyle == TypefaceStyle.Bold)
					{
						flagLeft = TypefaceStyle.Italic;
					}
					EditableText.SetSpan(new StyleSpan(flagLeft), newStart, newEnd, spanType);
				}

				if (startsBefore)
				{
					EditableText.SetSpan(new StyleSpan(span.Style), spanStart, newStart, SpanTypes.ExclusiveExclusive);
				}
				if (endsAfter)
				{
					EditableText.SetSpan(new StyleSpan(span.Style), newEnd, spanEnd, SpanTypes.ExclusiveExclusive);
				}

			}
			if (!hasFlag)
			{
				EditableText.SetSpan(new StyleSpan(flagStyle), selectionStart, selectionEnd, spanType);
			}

			SaveChanges(selectionStart, selectionEnd);
		}

		void UpdateUnderlineSpans()
		{
			var selectionStart = Control.SelectionStart;
			var selectionEnd = Control.SelectionEnd;
			var underlineSpans = EditableText.GetSpans(selectionStart, selectionEnd, Java.Lang.Class.FromType(typeof(UnderlineSpan)));

			bool hasFlag = false;
			var spanType = SpanTypes.InclusiveInclusive;

			foreach (UnderlineSpan span in underlineSpans)
			{
				hasFlag = true;

				var spanStart = EditableText.GetSpanStart(span);
				var spanEnd = EditableText.GetSpanEnd(span);
				var newStart = spanStart;
				var newEnd = spanEnd;
				var startsBefore = false;
				var endsAfter = false;

				if (spanStart < selectionStart)
				{
					newStart = selectionStart;
					startsBefore = true;
				}
				if (spanEnd > selectionEnd)
				{
					newEnd = selectionEnd;
					endsAfter = true;
				}

				EditableText.RemoveSpan(span);

				if (startsBefore)
				{
					EditableText.SetSpan(new UnderlineSpan(), spanStart, newStart, SpanTypes.ExclusiveExclusive);
				}
				if (endsAfter)
				{
					EditableText.SetSpan(new UnderlineSpan(), newEnd, spanEnd, SpanTypes.ExclusiveExclusive);
				}
			}

			if (!hasFlag)
			{
				EditableText.SetSpan(new UnderlineSpan(), selectionStart, selectionEnd, spanType);
			}

			SaveChanges(selectionStart, selectionEnd);
		}

		void SaveChanges(int selectionStart, int selectionEnd)
		{
			Control.TextFormatted = EditableText;
			Control.RequestFocus();
			Control.SetSelection(selectionStart, selectionEnd);
		}
	}
	/*
	class StyleCallback : ActionMode.Callback2
	{
		public override bool OnCreateActionMode(ActionMode mode, IMenu menu)
		{
			var inflater = mode.MenuInflater;
			inflater.
		}
	}*/
}
