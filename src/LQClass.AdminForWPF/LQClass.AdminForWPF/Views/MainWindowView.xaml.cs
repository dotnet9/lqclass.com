using HandyControl.Controls;
using LQClass.AdminForWPF.ViewModels;
using Prism.Events;

namespace LQClass.AdminForWPF.Views;

/// <summary>
///     MainWindowView.xaml 的交互逻辑
/// </summary>
public partial class MainWindowView : BlurWindow
{
    public MainWindowView(IEventAggregator eventAggregator)
    {
        InitializeComponent();
        Loaded += (sender, args) =>
        {
            var vm = DataContext as MainWindowViewModel;
            //MainWindowViewModel.Snackbar = this.messageTips;
            if (vm.NeedShowHome)
            {
                vm.ShowHome();
                vm.NeedShowHome = false;
            }
        };
        titleBar.EventAggregator = eventAggregator;
    }
}