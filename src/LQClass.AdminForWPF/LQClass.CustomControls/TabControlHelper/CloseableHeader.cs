using Prism.Commands;
using System;
using System.Windows;
using System.Windows.Input;

namespace LQClass.CustomControls.TabControlHelper
{
	public class CloseableHeader
	{
		public CloseableHeader(string key, string title, bool canClose)
		{
			this.Key = key;
			this.Title = title;
			this.CanClose = canClose;
			this.CloseCommand = new DelegateCommand(Close);
		}

		public event Action RequestClose;

		public ICommand CloseCommand { get; private set; }
		private void Close()
		{
			if (this.RequestClose != null)
			{
				this.RequestClose();
			}
		}
		public string Key { get; private set; }
		public string Title { get; private set; }
		public bool CanClose { get; private set; }
		public Visibility Visibility
		{
			get { return CanClose ? Visibility.Visible : Visibility.Collapsed; }
		}
	}
}
