using LQClass.CustomControls.Helpers;
using Prism.Services.Dialogs;
using System;
using System.Windows;

namespace LQClass.CustomControls.Controls
{
	/// <summary>
	/// DialogWindow.xaml 的交互逻辑
	/// </summary>
	public partial class DialogWindow : Window, IDialogWindow
    {
        public DialogWindow()
        {
            InitializeComponent();
        }
        protected override void OnSourceInitialized(EventArgs e)
        {
            WindowsHelper.RemoveIcon(this);//使用win32函数去除Window的Icon部分
        }
        public IDialogResult Result { get; set; }

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
            Width = Owner.Width;
            Height = Owner.Height;
            Top = Owner.Top;
            Left = Owner.Left;
		}
	}
}
