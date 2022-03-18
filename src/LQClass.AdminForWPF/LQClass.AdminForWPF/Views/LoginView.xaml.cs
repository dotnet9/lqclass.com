using System.Windows;
using System.Windows.Input;
using LQClass.AdminForWPF.ViewModels;

namespace LQClass.AdminForWPF.Views;

/// <summary>
///     LoginView.xaml 的交互逻辑
/// </summary>
public partial class LoginView : Window
{
    public LoginView()
    {
        InitializeComponent();

        NameTextBox.Focus();
        var vm = DataContext as LoginViewModel;
        //LoginViewModel.Snackbar = this.messageTips;
        vm.LoginComplete += result => DialogResult = result;
        //vm.PasswordBox = this.passwordBox;
        //this.passwordBox.Password = vm.Password;
    }

    private void MoveWindow_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (e.ChangedButton == MouseButton.Left) DragMove();
    }

    private void Close_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }
}