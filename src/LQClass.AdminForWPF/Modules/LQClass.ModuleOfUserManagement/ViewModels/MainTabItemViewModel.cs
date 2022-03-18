﻿using LQClass.ModuleOfUserManagement.I18nResources;
using Prism.Mvvm;
using WpfExtensions.Xaml;

namespace LQClass.ModuleOfUserManagement.ViewModels;

public class MainTabItemViewModel : BindableBase
{
    private string _Header;

    public MainTabItemViewModel()
    {
        Header = I18nManager.Instance.Get(Language.MainTabItemView_Header).ToString();
    }

    public string Header
    {
        get => _Header;
        set => SetProperty(ref _Header, value);
    }
}