using LQClass.ModuleOfUserManagement.ViewModels;
using LQClass.ModuleOfUserManagement.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Unity;
using WpfExtensions.Xaml;

namespace LQClass.ModuleOfUserManagement
{
	public class ModuleOfUserManagementModule : IModule
	{
		public const string KEY_OF_CURRENT_MODULE = "User_Management";
		private readonly IRegionManager _regionManager;
		private readonly IUnityContainer _unityContainer;

		public ModuleOfUserManagementModule(IRegionManager regionManager, IUnityContainer unityContainer)
		{
			I18nManager.Instance.Add(LQClass.ModuleOfUserManagement.I18nResources.UiResource.ResourceManager);
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