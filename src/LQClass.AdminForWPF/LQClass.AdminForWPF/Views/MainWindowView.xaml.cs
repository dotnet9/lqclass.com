using HandyControl.Controls;
using LQClass.AdminForWPF.ViewModels;
using MahApps.Metro.Controls;

namespace LQClass.AdminForWPF.Views
{
	/// <summary>
	/// MainWindowView.xaml 的交互逻辑
	/// </summary>
	public partial class MainWindowView : MetroWindow
	{
		public MainWindowView()
		{
			InitializeComponent();
			this.Loaded += (sender, args) =>
			{
				var vm = this.DataContext as MainWindowViewModel;
				MainWindowViewModel.Snackbar = this.messageTips;
				if (vm.NeedShowHome)
				{
					vm.ShowHome();
					vm.NeedShowHome = false;
				}
			};
		}
	}
}
