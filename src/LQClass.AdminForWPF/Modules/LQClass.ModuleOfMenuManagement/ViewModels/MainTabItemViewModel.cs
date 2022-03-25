using HandyControl.Data;
using LQClass.AdminForWPF.Infrastructure.Mvvm;
using LQClass.AdminForWPF.Infrastructure.Tools;
using LQClass.ModuleOfMenuManagement.DTOs;
using LQClass.ModuleOfMenuManagement.I18nResources;
using LQClass.ModuleOfMenuManagement.Models;
using Newtonsoft.Json;
using Prism.Events;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using WpfExtensions.Xaml;

namespace LQClass.ModuleOfMenuManagement.ViewModels;

public class MainTabItemViewModel : ViewModelBase
{
    #region 私有变量

    private readonly MainTabItemModel mainTabItemModel;

    #endregion

    public MainTabItemViewModel(
        MainTabItemModel mainTabItemModel,
        IRegionManager regionManager,
        IModuleManager moduleManager,
        IDialogService dialogService,
        IEventAggregator eventAggregator) : base(regionManager, moduleManager, dialogService, eventAggregator)
    {
        this.mainTabItemModel = mainTabItemModel;
        Header = I18nManager.Instance.Get(Language.MainTabItemView_Header).ToString();
        RaiseSearchHandler();
    }

    #region 属性

    private string _Header;

    public string Header
    {
        get => _Header;
        set => SetProperty(ref _Header, value);
    }

    private string _ITCode;

    /// <summary>
    ///     账号
    /// </summary>
    public string ITCode
    {
        get => _ITCode;
        set => SetProperty(ref _ITCode, value);
    }

    private string _ActionUrl;

    /// <summary>
    ///     账号
    /// </summary>
    public string ActionUrl
    {
        get => _ActionUrl;
        set => SetProperty(ref _ActionUrl, value);
    }

    private string _IP;

    /// <summary>
    ///     IP
    /// </summary>
    public string IP
    {
        get => _IP;
        set => SetProperty(ref _IP, value);
    }

    /// <summary>
    ///     数据列表
    /// </summary>
    public ObservableCollection<MenuDTO> ListData { get; } = new();



    #endregion

    #region 命令



    #endregion

    #region 私有方法

    /// <summary>
    ///     查询日志
    /// </summary>
    private async Task RaiseSearchHandler()
    {
        PageIndex = 1;
        await SearchData();
    }

    /// <summary>
    ///     添加
    /// </summary>
    /// <returns></returns>
    private async Task RaiseAddHandler()
    {
    }

    /// <summary>
    ///     分页变化事件
    /// </summary>
    /// <returns></returns>
    private async Task RaisePageUpdateHandler(FunctionEventArgs<int> pageIndex)
    {
        PageIndex = pageIndex.Info;
        await SearchData();
    }

    private async Task SearchData()
    {
        try
        {
            if (IsIndeterminate) return;
            IsIndeterminate = true;
            var searchCondition = new MenuSearcherDTO
            {
                Page = PageIndex,
                Limit = 50,
            };
            var response = await mainTabItemModel.Search(searchCondition);
            if (response.IsSuccessful)
            {
                var searchResult = JsonConvert.DeserializeObject<MenuResultDTO>(response.Content);
                ListData.Clear();
                ListData.AddRange(searchResult.Data);
                MaxPageCount = searchResult.PageCount;
                PageIndex = searchResult.Page;
            }
            else
            {
                ShowTipMsg($"{JsonHelper.FormatJsonString(response.Content)}");
            }
        }
        catch (Exception ex)
        {
            ShowTipMsg(ex.Message);
        }
        finally
        {
            IsIndeterminate = false;
        }
    }

    #endregion
}