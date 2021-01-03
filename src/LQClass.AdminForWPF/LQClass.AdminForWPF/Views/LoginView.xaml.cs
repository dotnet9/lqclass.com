using LQClass.AdminForWPF.ViewModels;
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
			this.NameTextBox.Focus();
			var vm = this.DataContext as LoginViewModel;
			vm.Snackbar = this.messageTips;
			vm.LoginComplete += (result) => DialogResult = result;
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
