using Prism.Mvvm;
using WpfExtensions.Xaml;

namespace LQClass.ModuleOfGroupManagement.ViewModels
{
	public class MainTabItemViewModel : BindableBase
	{

		private string _Header;
		public string Header
		{
			get { return _Header; }
			set { SetProperty(ref _Header, value); }
		}

		public MainTabItemViewModel()
		{
			Header = I18nManager.Instance.Get(I18nResources.Language.MainTabItemView_Header).ToString();
		}
	}
}
