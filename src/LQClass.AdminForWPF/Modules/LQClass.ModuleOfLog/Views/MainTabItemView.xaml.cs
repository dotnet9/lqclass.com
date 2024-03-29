﻿using System;
using System.Windows.Controls;
using HandyControl.Controls;
using LQClass.CustomControls.TabControlHelper;
using LQClass.ModuleOfLog.DTOs;
using LQClass.ModuleOfLog.ViewModels;
using WpfExtensions.Xaml;

namespace LQClass.ModuleOfLog.Views;

/// <summary>
///     Interaction logic for ViewA.xaml
/// </summary>
public partial class MainTabItemView : UserControl, ICloseable
{
    public MainTabItemView()
    {
        InitializeComponent();
        Closer = new CloseableHeader(ModuleOfLogModule.KEY_OF_CURRENT_MODULE,
            I18nManager.Instance.Get(I18nResources.Language.MainTabItemView_Header).ToString(), true);
    }

    public CloseableHeader Closer { get; }

    private void LogTypeSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var vm = DataContext as MainTabItemViewModel;
        var selectedItems = (sender as CheckComboBox).SelectedItems;
        vm.SelectedLogTypes.Clear();
        foreach (var item in selectedItems)
        {
            var key = (item as LogTypeInfo).Key;
            var logType = (ActionLogTypesEnum)Enum.Parse(typeof(ActionLogTypesEnum), key.ToString());
            vm.SelectedLogTypes.Add(logType);
        }
    }
}