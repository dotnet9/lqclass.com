﻿using LQClass.CustomControls.TabControlHelper;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using WpfExtensions.Xaml;

namespace LQClass.ModuleOfHome.Views
{
	/// <summary>
	/// Interaction logic for ViewA.xaml
	/// </summary>
	public partial class MainTabItemView : UserControl, ICloseable
	{
		public MainTabItemView()
		{
			InitializeComponent();
			this.Closer = new CloseableHeader(ModuleOfHomeModule.KEY_OF_CURRENT_MODULE, I18nManager.Instance.Get(I18nResources.Language.MainTabItemView_Header).ToString(), false);
		}

		public CloseableHeader Closer { get; private set; }
	}
}
