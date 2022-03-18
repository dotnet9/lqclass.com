using System;
using System.Collections.Generic;
using System.Windows.Input;
using HandyControl.Controls;
using HandyControl.Data;
using LQClass.AdminForWPF.Infrastructure.Models;
using Prism.Commands;
using Prism.Events;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;

namespace LQClass.AdminForWPF.Infrastructure.Mvvm;

public class ViewModelBase : BindableBase
{
    public ViewModelBase(IRegionManager regionManager) : this()
    {
        RegionManager = regionManager;
    }

    public ViewModelBase(IRegionManager regionManager,
        IModuleManager moduleManager,
        IDialogService dialogService,
        IEventAggregator eventAggregator) : this()
    {
        RegionManager = regionManager;
        ModuleManager = moduleManager;
        DialogService = dialogService;
        EventAggregator = eventAggregator;
        ModuleManager.LoadModuleCompleted += _moduleManager_LoadModuleCompleted;
    }

    public ViewModelBase()
    {
    }

    private void _moduleManager_LoadModuleCompleted(object sender, LoadModuleCompletedEventArgs e)
    {
        //DialogService.ShowDialog("SuccessDialog", new DialogParameters($"message={e.ModuleInfo.ModuleName + "模块被加载了"}"), null);
    }

    #region 命令处理方法

    /// <summary>
    ///     显示对话框
    /// </summary>
    /// <param name="dlgName"></param>
    private void RaiseShowDialogHandler(string dlgName)
    {
        DialogService.ShowDialog(dlgName);
    }

    #endregion


    /// <summary>
    ///     显示界面提示信息
    /// </summary>
    /// <param name="msg"></param>
    public void ShowTipMsg(string msg)
    {
        Growl.ErrorGlobal(new GrowlInfo { Message = msg, WaitTime = 5 });
        //  Snackbar?.MessageQueue?.Enqueue(msg,
        //null,
        //null,
        //null,
        //false,
        //true,
        //TimeSpan.FromSeconds(3));
    }

    public void CurrentUICultureChanged()
    {
    }

    #region 共用属性

    private bool _IsIndeterminate;

    /// <summary>
    ///     登录时显示正在繁忙
    /// </summary>
    public bool IsIndeterminate
    {
        get => _IsIndeterminate;
        set => SetProperty(ref _IsIndeterminate, value);
    }

    private string _CurrentLanguage;

    /// <summary>
    ///     当前选择的选择
    /// </summary>
    public string CurrentLanguage
    {
        get => _CurrentLanguage;
        set => SetProperty(ref _CurrentLanguage, value);
    }

    /// <summary>
    ///     语言列表
    /// </summary>
    public List<LanguageModel> Languages => LanguageModel.GetLanguages();

    private DateTime _StartTime;

    /// <summary>
    ///     开始时间
    /// </summary>
    public DateTime StartTime
    {
        get => _StartTime;
        set => SetProperty(ref _StartTime, value);
    }

    private DateTime _EndTime;

    /// <summary>
    ///     IP
    /// </summary>
    public DateTime EndTime
    {
        get => _EndTime;
        set => SetProperty(ref _EndTime, value);
    }

    private int _MaxPageCount;

    /// <summary>
    ///     页数
    /// </summary>
    public int MaxPageCount
    {
        get => _MaxPageCount;
        set => SetProperty(ref _MaxPageCount, value);
    }

    private int _PageIndex;

    /// <summary>
    ///     页索引
    /// </summary>
    public int PageIndex
    {
        get => _PageIndex;
        set => SetProperty(ref _PageIndex, value);
    }

    /// <summary>
    ///     每页显示大小
    /// </summary>
    public int DataCountPerPage { get; set; } = 50;

    /// <summary>
    ///     提示控件，未找到设置时间等绑定属性，先由view输入viewmodel
    /// </summary>
    //public static Snackbar Snackbar { get; set; }

    #endregion

    #region 共用命令

    private ICommand _RaiseShowDialogCommand;

    /// <summary>
    ///     打开对话框
    /// </summary>
    public ICommand RaiseShowDialogCommand =>
        _RaiseShowDialogCommand ?? (_RaiseShowDialogCommand = new DelegateCommand<string>(RaiseShowDialogHandler));

    #endregion

    #region private变量

    public readonly IRegionManager RegionManager;
    public readonly IModuleManager ModuleManager;
    public readonly IDialogService DialogService;
    public readonly IEventAggregator EventAggregator;

    #endregion
}