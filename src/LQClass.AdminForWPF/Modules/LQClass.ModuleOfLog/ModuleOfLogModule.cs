using LQClass.ModuleOfLog.Models;
using LQClass.ModuleOfLog.ViewModels;
using LQClass.ModuleOfLog.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;
using Unity;
using WpfExtensions.Xaml;

namespace LQClass.ModuleOfLog
{
	public class ModuleOfLogModule : IModule
	{
		public const string KEY_OF_CURRENT_MODULE = "MenuKey_Log";
		private readonly IRegionManager _regionManager;
		private readonly IUnityContainer _unityContainer;

		public ModuleOfLogModule(IRegionManager regionManager, IUnityContainer unityContainer)
		{
			I18nManager.Instance.Add(LQClass.ModuleOfLog.I18nResources.UiResource.ResourceManager);
			this._regionManager = regionManager;
			this._unityContainer = unityContainer;
		}
		public void OnInitialized(IContainerProvider containerProvider)
		{

		}

		public void RegisterTypes(IContainerRegistry containerRegistry)
		{
			containerRegistry.RegisterForNavigation<MainTabItemView, MainTabItemViewModel>(KEY_OF_CURRENT_MODULE);
			containerRegistry.RegisterSingleton<MainTabItemModel>();
		}
	}
}