using System.Diagnostics;
using System.Windows;
using System.Windows.Documents;

namespace LQClass.AdminForWPF.Views
{
	/// <summary>
	/// LoginView.xaml 的交互逻辑
	/// </summary>
	public partial class LoginView : Window
	{
		public LoginView()
		{
			InitializeComponent();
		}

		private void MoveWindow_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			if (e.ChangedButton == System.Windows.Input.MouseButton.Left)
			{
				DragMove();
			}
		}

		private void Hyperlink_Click(object sender, RoutedEventArgs e)
		{
			Hyperlink link = sender as Hyperlink;
			Process.Start(new ProcessStartInfo("cmd", $"/c start {link.NavigateUri.AbsoluteUri}")
			{
				UseShellExecute = false,
				CreateNoWindow = true
			});
		}
	}
}
