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
		public const string KEY_OF_CURRENT_MODULE = "Home";
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
			containerRegistry.RegisterForNavigation<MainTabItemView, MainTabItemViewModel>(KEY_OF_CURRENT_MODULE);
		}
	}
}