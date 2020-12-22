using System.Windows;

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

    private void Close_Click(object sender, RoutedEventArgs e)
    {
      this.Close();
    }
  }
}