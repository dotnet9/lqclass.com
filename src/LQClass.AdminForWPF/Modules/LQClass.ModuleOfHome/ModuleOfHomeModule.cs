using LQClass.AdminForWPF.Infrastructure;
using LQClass.ModuleOfHome.ViewModels;
using LQClass.ModuleOfHome.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Unity;
using WpfExtensions.Xaml;

namespace LQClass.ModuleOfHome
{
	public class ModuleOfHomeModule : IModule
	{
		private readonly IRegionManager _regionManager;
		private readonly IUnityContainer _unityContainer;

		public ModuleOfHomeModule(IRegionManager regionManager, IUnityContainer unityContainer)
		{
			I18nManager.Instance.Add(LQClass.ModuleOfHome.I18nResources.UiResource.ResourceManager);
			this._regionManager = regionManager;
			this._unityContainer = unityContainer;
		}
		public void OnInitialized(IContainerProvider containerProvider)
		{

		}

		public void RegisterTypes(IContainerRegistry containerRegistry)
		{
			if (!_regionManager.Regions.ContainsRegionWithName(RegionNames.MainTabRegion))
			{
				_regionManager.Regions.Add(RegionNames.MainTabRegion, new Region { Name = RegionNames.MainTabRegion });
			}
			_regionManager.Regions[RegionNames.MainTabRegion].Add(_unityContainer.Resolve<MainTabItemView>(), "Home");      // View命名为"Home"，主窗口调用时使用
			//_regionManager.RegisterViewWithRegion(RegionNames.MainTabRegion, typeof(MainTabItemView));
			//containerRegistry.RegisterForNavigation<MainTabItemView, MainTabItemViewModel>();
		}
	}
}