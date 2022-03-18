using LQClass.ModuleOfMenuManagement.I18nResources;
using LQClass.ModuleOfMenuManagement.ViewModels;
using LQClass.ModuleOfMenuManagement.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Unity;
using WpfExtensions.Xaml;

namespace LQClass.ModuleOfMenuManagement;

public class ModuleOfMenuManagementModule : IModule
{
    public const string KEY_OF_CURRENT_MODULE = "Menu_Mangement";
    private readonly IRegionManager _regionManager;
    private readonly IUnityContainer _unityContainer;

    public ModuleOfMenuManagementModule(IRegionManager regionManager, IUnityContainer unityContainer)
    {
        I18nManager.Instance.Add(UiResource.ResourceManager);
        _regionManager = regionManager;
        _unityContainer = unityContainer;
    }

    public void OnInitialized(IContainerProvider containerProvider)
    {
    }

    public void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry.RegisterForNavigation<MainTabItemView, MainTabItemViewModel>(KEY_OF_CURRENT_MODULE);
    }
}