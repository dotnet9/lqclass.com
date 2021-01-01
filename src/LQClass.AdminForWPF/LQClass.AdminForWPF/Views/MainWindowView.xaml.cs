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
			(DataContext as MainWindowViewModel).AddTabItem += AddTabItem;
		}

		private void AddTabItem(object view)
		{
			if (view is TabItem newTabItem)
			{
				bool isExist = false;
				foreach (var item in this.tabCtl.Items)
				{
					var tabItem = item as TabItem;
					if (tabItem == null)
					{
						continue;
					}
					if (tabItem.Header?.ToString() == newTabItem.Header?.ToString())
					{
						isExist = true;
						this.tabCtl.SelectedItem = tabItem;
						break;
					}
				}
				if (!isExist)
				{
					this.tabCtl.Items.Add(newTabItem);
					this.tabCtl.SelectedItem = newTabItem;
				}
			}
		}
	}
}
