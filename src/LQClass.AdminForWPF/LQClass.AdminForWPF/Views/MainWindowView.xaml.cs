using HandyControl.Controls;
using LQClass.AdminForWPF.Infrastructure.Models;
using LQClass.AdminForWPF.ViewModels;
using MahApps.Metro.Controls;
using System.Windows;

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

		private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
		{
			var menuItem = this.tree.SelectedItem as CustomMenuItem;
			if (menuItem == null)
			{
				return;
			}
			bool isExist = false;
			foreach (var item in this.tabCtl.Items)
			{
				var tabItem = item as TabItem;
				if (tabItem == null)
				{
					continue;
				}
				if (tabItem.Header?.ToString() == menuItem.Name)
				{
					isExist = true;
					this.tabCtl.SelectedItem = tabItem;
					break;
				}
			}
			if (!isExist)
			{
				var tabItem = new TabItem();
				tabItem.Header = menuItem.Name;
				this.tabCtl.Items.Add(tabItem);
			}
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
				}
			}
		}
	}
}
