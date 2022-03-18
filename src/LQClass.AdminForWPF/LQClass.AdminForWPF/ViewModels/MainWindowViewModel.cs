using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using LQClass.AdminForWPF.ICommonService;
using LQClass.AdminForWPF.Infrastructure;
using LQClass.AdminForWPF.Infrastructure.Configs;
using LQClass.AdminForWPF.Infrastructure.Events;
using LQClass.AdminForWPF.Infrastructure.Models;
using LQClass.AdminForWPF.Infrastructure.Mvvm;
using LQClass.AdminForWPF.Models;
using LQClass.AdminForWPF.Views;
using LQClass.CustomControls.TabControlHelper;
using Prism.Commands;
using Prism.Events;
using Prism.Modularity;
using Prism.Regions;
using Prism.Services.Dialogs;
using WpfExtensions.Xaml;

namespace LQClass.AdminForWPF.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public MainWindowViewModel(MainWindowModel mainWindowModel,
        IRegionManager regionManager,
        IModuleManager moduleManager,
        IDialogService dialogService,
        IEventAggregator eventAggregator) : base(regionManager, moduleManager, dialogService, eventAggregator)
    {
        _windowModel = mainWindowModel;
        eventAggregator.GetEvent<ChangeLanguageSentEvent>().Subscribe(ReceivedChangeLanguage);
    }

    public bool NeedShowHome { get; set; } = true;

    /// <summary>
    ///     初始化
    /// </summary>
    public void InitData()
    {
        CustomMenus.Clear();
        CustomMenus.AddRange(_windowModel.ReadCustomMenus());
    }

    /// <summary>
    ///     显示首页
    /// </summary>
    public void ShowHome()
    {
        if (NeedShowHome)
        {
            SelectedMenuItem = CustomMenus.First(cu => cu.Key == CustomMenuItem.KEY_OF_HOME);
            RaiseSelectedItemHandler(SelectedMenuItem);
        }
    }


    public void CurrentUICultureChanged(CurrentUICultureChangedEventArgs args)
    {
    }

    #region 属性

    /// <summary>
    ///     菜单列表
    /// </summary>
    public ObservableCollection<CustomMenuItem> CustomMenus { get; } = new();

    private CustomMenuItem _SelectedMenuItem;

    /// <summary>
    ///     选择的菜单
    /// </summary>
    public CustomMenuItem SelectedMenuItem
    {
        get => _SelectedMenuItem;
        set => SetProperty(ref _SelectedMenuItem, value);
    }

    #endregion

    #region 命令

    private ICommand _RaiseLogoutCommand;

    /// <summary>
    ///     用户命令
    /// </summary>
    public ICommand RaiseLogoutCommand =>
        _RaiseLogoutCommand ?? (_RaiseLogoutCommand = new DelegateCommand(RaiseLogoutHandler));

    private ICommand _RaiseSelectedItemCommand;

    /// <summary>
    ///     选择菜单命令
    /// </summary>
    public ICommand RaiseSelectedItemCommand =>
        _RaiseSelectedItemCommand ??
        (_RaiseSelectedItemCommand = new DelegateCommand<CustomMenuItem>(RaiseSelectedItemHandler));

    private ICommand _RaiseSelectedTabItemCommand;

    /// <summary>
    ///     选择TabItem菜单命令
    /// </summary>
    public ICommand RaiseSelectedTabItemCommand =>
        _RaiseSelectedTabItemCommand ??
        (_RaiseSelectedTabItemCommand = new DelegateCommand<object>(RaiseSelectedTabItemHandler));

    #endregion

    #region 私有变量

    private readonly MainWindowModel _windowModel;
    private ISystemMenuService _systemMenuService = null;

    #endregion

    #region 命令处理方法

    /// <summary>
    ///     注销命令
    /// </summary>
    /// <param name="key"></param>
    private void RaiseLogoutHandler()
    {
        AppSettingsHelper.IsAutoLogin = false;
        ShellSwitcher.Switch<MainWindowView, LoginView>();
    }

    /// <summary>
    ///     显示的菜单TabItem
    /// </summary>
    private readonly Dictionary<string, object> dictExistTabItems = new();

    /// <summary>
    ///     点击过的菜单项
    /// </summary>
    private readonly Dictionary<string, CustomMenuItem> dictExistMenuItems = new();

    /// <summary>
    ///     用于控制左侧的TreeItem与右侧的TabItem选择，同时触发一个
    /// </summary>
    private bool isSelectedItem;

    /// <summary>
    ///     选择菜单命令
    /// </summary>
    private void RaiseSelectedItemHandler(CustomMenuItem menuItem)
    {
        try
        {
            if (isSelectedItem) return;
            isSelectedItem = true;
            if (menuItem == null
                || !RegionManager.Regions.ContainsRegionWithName(RegionNames.MainTabRegion))
                return;
            dictExistMenuItems[menuItem.Key] = menuItem;
            var region = RegionManager.Regions[RegionNames.MainTabRegion];
            if (dictExistTabItems.ContainsKey(menuItem.Key))
            {
                region.Activate(dictExistTabItems[menuItem.Key]);
                return;
            }

            region.RequestNavigate(menuItem.Key);
            dictExistTabItems.Clear();

            foreach (var viewObj in region.Views)
            {
                var view = viewObj as ICloseable;
                if (view != null)
                {
                    dictExistTabItems[view.Closer.Key] = view;
                    view.Closer.Icon = $"./../Images/{view.Closer.Key}OfTab.png";
                    if (view.Closer.CanClose)
                        view.Closer.RequestClose += () =>
                        {
                            dictExistTabItems.Remove(view.Closer.Key);
                            if (region.Views.Contains(view)) region.Remove(view);
                        };
                }
            }
        }
        finally
        {
            isSelectedItem = false;
        }
    }

    /// <summary>
    ///     选择TabItem时，同步更新左侧TreeItem选择状态
    /// </summary>
    /// <param name="obj"></param>
    private void RaiseSelectedTabItemHandler(object obj)
    {
        try
        {
            if (isSelectedItem) return;
            isSelectedItem = true;
            var view = obj as ICloseable;
            if (view == null || !dictExistMenuItems.ContainsKey(view.Closer.Key)) return;
            dictExistMenuItems[view.Closer.Key].IsSelected = true;
        }
        finally
        {
            isSelectedItem = false;
        }
    }

    private void ReceivedChangeLanguage()
    {
        InitData();
    }

    #endregion
}