using LQClass.AdminForWPF.Dtos;
using LQClass.AdminForWPF.Infrastructure.Mvvm;
using System.Collections.ObjectModel;

namespace LQClass.AdminForWPF.ViewModels
{
	public class WebViewModel : DialogViewModelBase
	{
		private ObservableCollection<AvatarDto> _ListDatas;
		public ObservableCollection<AvatarDto> ListDatas
		{
			get
			{
				if (_ListDatas == null)
				{
					_ListDatas = new ObservableCollection<AvatarDto>()
					{
						new AvatarDto{ DisplayName="Dotnet9",Link="https://dotnet9.com", AvatarUri="https://img.dotnet9.com/logo.png"}
					};
				}
				return _ListDatas;
			}
		}
	}
}
