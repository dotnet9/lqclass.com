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
    ///     ��ʼ��
    /// </summary>
    public void InitData()
    {
        CustomMenus.Clear();
        CustomMenus.AddRange(_windowModel.ReadCustomMenus());
    }

    /// <summary>
    ///     ��ʾ��ҳ
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

    #region ����

    /// <summary>
    ///     �˵��б�
    /// </summary>
    public ObservableCollection<CustomMenuItem> CustomMenus { get; } = new();

    private CustomMenuItem _SelectedMenuItem;

    /// <summary>
    ///     ѡ��Ĳ˵�
    /// </summary>
    public CustomMenuItem SelectedMenuItem
    {
        get => _SelectedMenuItem;
        set => SetProperty(ref _SelectedMenuItem, value);
    }

    #endregion

    #region ����

    private ICommand _RaiseLogoutCommand;

    /// <summary>
    ///     �û�����
    /// </summary>
    public ICommand RaiseLogoutCommand =>
        _RaiseLogoutCommand ?? (_RaiseLogoutCommand = new DelegateCommand(RaiseLogoutHandler));

    private ICommand _RaiseSelectedItemCommand;

    /// <summary>
    ///     ѡ��˵�����
    /// </summary>
    public ICommand RaiseSelectedItemCommand =>
        _RaiseSelectedItemCommand ??
        (_RaiseSelectedItemCommand = new DelegateCommand<CustomMenuItem>(RaiseSelectedItemHandler));

    private ICommand _RaiseSelectedTabItemCommand;

    /// <summary>
    ///     ѡ��TabItem�˵�����
    /// </summary>
    public ICommand RaiseSelectedTabItemCommand =>
        _RaiseSelectedTabItemCommand ??
        (_RaiseSelectedTabItemCommand = new DelegateCommand<object>(RaiseSelectedTabItemHandler));

    #endregion

    #region ˽�б���

    private readonly MainWindowModel _windowModel;
    private ISystemMenuService _systemMenuService = null;

    #endregion

    #region �������

    /// <summary>
    ///     ע������
    /// </summary>
    /// <param name="key"></param>
    private void RaiseLogoutHandler()
    {
        AppSettingsHelper.IsAutoLogin = false;
        ShellSwitcher.Switch<MainWindowView, LoginView>();
    }

    /// <summary>
    ///     ��ʾ�Ĳ˵�TabItem
    /// </summary>
    private readonly Dictionary<string, object> dictExistTabItems = new();

    /// <summary>
    ///     ������Ĳ˵���
    /// </summary>
    private readonly Dictionary<string, CustomMenuItem> dictExistMenuItems = new();

    /// <summary>
    ///     ���ڿ�������TreeItem���Ҳ��TabItemѡ��ͬʱ����һ��
    /// </summary>
    private bool isSelectedItem;

    /// <summary>
    ///     ѡ��˵�����
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
    ///     ѡ��TabItemʱ��ͬ���������TreeItemѡ��״̬
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