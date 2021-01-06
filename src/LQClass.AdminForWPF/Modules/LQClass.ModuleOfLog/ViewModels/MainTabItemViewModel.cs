using LQClass.AdminForWPF.Infrastructure.Mvvm;
using LQClass.AdminForWPF.Infrastructure.Tools;
using LQClass.ModuleOfLog.DTOs;
using LQClass.ModuleOfLog.Models;
using Newtonsoft.Json;
using Prism.Commands;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfExtensions.Xaml;

namespace LQClass.ModuleOfLog.ViewModels
{
	public class MainTabItemViewModel : ViewModelBase
	{
		public MainTabItemViewModel(MainTabItemModel mainTabItemModel)
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

		private ObservableCollection<LogDto> _ListDatas = new ObservableCollection<LogDto>();
		/// <summary>
		/// 数据列表
		/// </summary>
		public ObservableCollection<LogDto> ListData { get { return _ListDatas; } }

		#endregion

		#region 命令

		private ICommand _raiseSearchCommand;
		/// <summary>
		/// 查询命令
		/// </summary>
		public ICommand RaiseSearchCommand =>
		  _raiseSearchCommand ?? (_raiseSearchCommand = new DelegateCommand(async () => await RaiseSearchHandler()));


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
			try
			{
				if (IsIndeterminate)
				{
					return;
				}
				IsIndeterminate = true;

				var response = await mainTabItemModel.Search();
				if (response.IsSuccessful)
				{
					var searchResult = JsonConvert.DeserializeObject<SearchLogResultDto>(response.Content);
					ListData.Clear();
					ListData.AddRange(searchResult.Data);
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
