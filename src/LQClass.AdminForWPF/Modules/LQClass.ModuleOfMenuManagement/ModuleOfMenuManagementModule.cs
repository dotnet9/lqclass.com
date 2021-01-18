using LQClass.ModuleOfMenuManagement.ViewModels;
using LQClass.ModuleOfMenuManagement.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Unity;
using WpfExtensions.Xaml;

namespace LQClass.ModuleOfMenuManagement
{
	public class ModuleOfMenuManagementModule : IModule
	{
		public const string KEY_OF_CURRENT_MODULE = "MenuKey_MenuMangement";
		private readonly IRegionManager _regionManager;
		private readonly IUnityContainer _unityContainer;

		public ModuleOfMenuManagementModule(IRegionManager regionManager, IUnityContainer unityContainer)
		{
			I18nManager.Instance.Add(LQClass.ModuleOfMenuManagement.I18nResources.UiResource.ResourceManager);
			this._regionManager = regionManager;
			this._unityContainer = unityContainer;
		}
		public void OnInitialized(IContainerProvider containerProvider)
		{

		}

		public void RegisterTypes(IContainerRegistry containerRegistry)
		{
			containerRegistry.RegisterForNavigation<MainTabItemView, MainTabItemViewModel>(KEY_OF_CURRENT_MODULE);
		}
	}
}