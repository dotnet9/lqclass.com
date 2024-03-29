﻿using System.Windows.Controls;
using LQClass.CustomControls.TabControlHelper;
using WpfExtensions.Xaml;

namespace LQClass.ModuleOfUserManagement.Views;

/// <summary>
///     Interaction logic for ViewA.xaml
/// </summary>
public partial class MainTabItemView : UserControl, ICloseable
{
    public MainTabItemView()
    {
        InitializeComponent();
        Closer = new CloseableHeader(ModuleOfUserManagementModule.KEY_OF_CURRENT_MODULE,
            I18nManager.Instance.Get(I18nResources.Language.MainTabItemView_Header).ToString(), true);
    }

    public CloseableHeader Closer { get; }
}