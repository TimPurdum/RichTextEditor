using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace RichTextEditor
{
	public class HtmlEditor : Editor, INotifyPropertyChanged
	{
		public static StyleBar StaticStyleBar = new StyleBar();

		public event EventHandler HtmlRequested;
		public event EventHandler<HtmlArgs> HtmlSet;
		public event EventHandler<StyleArgs> StyleChangeRequested;
		public event EventHandler<SelectionArgs> SelectionChangeHandler;

		string HtmlString;
		public int SelectionStart;
		public int SelectionEnd;

		public HtmlEditor()
		{
			MessagingCenter.Send(this, "register");
			this.PropertyChanged += (sender, e) => 
			{
				this.InvalidateMeasure();
			};
			MessagingCenter.Subscribe<TestableButton, StyleArgs>(this, "styleClicked", (sender, arg) =>
			{
				System.Diagnostics.Debug.WriteLine("Recieved Style click");
				StyleChangeRequested(this, arg);
			});
		}

		public class SelectionArgs : EventArgs
		{
			public int Start;
			public int End;
			public SelectionArgs(int start, int end)
			{
				Start = start;
				End = end;
			}
		}

		public void SetSelection(int start, int end)
		{
			var args = new SelectionArgs(start, end);
			SelectionChangeHandler(this, args);
		}

		public void SetHtmlText(string htmlString)
		{
			HtmlString = htmlString;
			var args = new HtmlArgs(htmlString);
			HtmlSet(this, args);
		}

		public class HtmlArgs : EventArgs
		{
			public string HtmlToPass;
			public HtmlArgs(string htmlToPass)
			{
				HtmlToPass = htmlToPass;
			}
		}

		public string GetHtmlText()
		{
			
			HtmlRequested(this, new EventArgs());
			return HtmlString;
		}

		public virtual void BoldChanged()
		{
			StyleChangeRequested(this, new StyleArgs("bold"));
		}

		public void ItalicChanged()
		{
			StyleChangeRequested(this, new StyleArgs("italic"));
		}

		public void UnderlineChanged()
		{
			StyleChangeRequested(this, new StyleArgs("underline"));
		}

		public class StyleArgs : EventArgs
		{
			public string Style;
			public StyleArgs(string style)
			{
				Style = style;
			}
		}
		/*
		bool CanPaste()
		{
			return false;
		}*/


	}
}
