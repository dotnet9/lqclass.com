using HandyControl.Data;
using LQClass.AdminForWPF.Infrastructure.Mvvm;
using LQClass.AdminForWPF.Infrastructure.Tools;
using LQClass.ModuleOfLog.DTOs;
using LQClass.ModuleOfLog.Models;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Events;
using Prism.Modularity;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Input;
using WpfExtensions.Xaml;

namespace LQClass.ModuleOfLog.ViewModels
{
	public class MainTabItemViewModel : ViewModelBase
	{
		public MainTabItemViewModel(
			MainTabItemModel mainTabItemModel,
			IRegionManager regionManager,
			IModuleManager moduleManager,
			IDialogService dialogService,
			IEventAggregator eventAggregator) : base(regionManager, moduleManager, dialogService, eventAggregator)
		{
			this.mainTabItemModel = mainTabItemModel;
			Header = I18nManager.Instance.Get(I18nResources.Language.MainTabItemView_Header).ToString();
			RaiseSearchHandler();
		}

		#region 属性

		private string _Header;
		public string Header
		{
			get { return _Header; }
			set { SetProperty(ref _Header, value); }
		}
		private string _ITCode;
		/// <summary>
		/// 账号
		/// </summary>
		public string ITCode
		{
			get { return _ITCode; }
			set { SetProperty(ref _ITCode, value); }
		}
		private string _ActionUrl;
		/// <summary>
		/// 账号
		/// </summary>
		public string ActionUrl
		{
			get { return _ActionUrl; }
			set { SetProperty(ref _ActionUrl, value); }
		}
		private string _IP;
		/// <summary>
		/// IP
		/// </summary>
		public string IP
		{
			get { return _IP; }
			set { SetProperty(ref _IP, value); }
		}

		private ObservableCollection<LogDto> _ListDatas = new ObservableCollection<LogDto>();
		/// <summary>
		/// 数据列表
		/// </summary>
		public ObservableCollection<LogDto> ListData { get { return _ListDatas; } }

		private ObservableCollection<LogTypeInfo> _ListLogTypes = null;
		/// <summary>
		/// 日志类型列表
		/// </summary>
		public ObservableCollection<LogTypeInfo> ListLogTypes
		{
			get
			{
				if (_ListLogTypes == null || _ListLogTypes.Count <= 0)
				{
					_ListLogTypes = new ObservableCollection<LogTypeInfo>
					{
						new LogTypeInfo(0,I18nManager.Instance.Get(I18nResources.Language.Normal).ToString()),
						new LogTypeInfo(1,I18nManager.Instance.Get(I18nResources.Language.Exception).ToString()),
						new LogTypeInfo(2,I18nManager.Instance.Get(I18nResources.Language.Debug).ToString())
					};
				}
				return _ListLogTypes;
			}
		}
		/// <summary>
		/// 选择的日志类型列表
		/// </summary>
		public List<ActionLogTypesEnum> SelectedLogTypes { get; set; } = new List<ActionLogTypesEnum>();

		#endregion

		#region 命令

		private ICommand _raiseSearchCommand;
		/// <summary>
		/// 查询命令
		/// </summary>
		public ICommand RaiseSearchCommand =>
		  _raiseSearchCommand ?? (_raiseSearchCommand = new DelegateCommand(async () => await RaiseSearchHandler()));

		private ICommand _raiseAddCommand;
		/// <summary>
		/// 添加命令
		/// </summary>
		public ICommand RaiseAddCommand =>
		  _raiseAddCommand ?? (_raiseAddCommand = new DelegateCommand(async () => await RaiseAddHandler()));

		private ICommand _RaisePageUpdatedCommand;
		/// <summary>
		/// 查询命令
		/// </summary>
		public ICommand RaisePageUpdatedCommand =>
		  _RaisePageUpdatedCommand ?? (_RaisePageUpdatedCommand = new DelegateCommand<FunctionEventArgs<int>>(async (pageIndex) => await RaisePageUpdateHandler(pageIndex)));


		#endregion

		#region 私有变量

		private readonly MainTabItemModel mainTabItemModel = null;

		#endregion

		#region 私有方法

		/// <summary>
		/// 查询日志
		/// </summary>
		private async Task RaiseSearchHandler()
		{
			PageIndex = 1;
			await SearchData();
		}

		/// <summary>
		/// 添加
		/// </summary>
		/// <returns></returns>
		private async Task RaiseAddHandler()
		{

		}

		/// <summary>
		/// 分页变化事件
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
				if (IsIndeterminate)
				{
					return;
				}
				IsIndeterminate = true;
				var searchCondition = new ActionLogSearcherDto
				{
					Page = PageIndex,
					Limit = 50,
					ITCode = ITCode,
					ActionUrl = ActionUrl,
					IP = IP,
					LogType = SelectedLogTypes
				};
				if (default(DateTime) != StartTime && default(DateTime) != EndTime && StartTime <= EndTime)
				{
					searchCondition.ActionTime.Add(StartTime.ToString("yyyy-MM-dd HH:mm:ss"));
					searchCondition.ActionTime.Add(EndTime.ToString("yyyy-MM-dd HH:mm:ss"));
				}
				var response = await mainTabItemModel.Search(searchCondition);
				if (response.IsSuccessful)
				{
					var searchResult = JsonConvert.DeserializeObject<SearchLogResultDto>(response.Content);
					ListData.Clear();
					ListData.AddRange(searchResult.Data);
					this.MaxPageCount = searchResult.PageCount;
					this.PageIndex = searchResult.Page;
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
}
