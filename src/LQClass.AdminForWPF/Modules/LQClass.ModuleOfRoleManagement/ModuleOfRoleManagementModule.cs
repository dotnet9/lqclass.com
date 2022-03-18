using LQClass.ModuleOfRoleManagement.I18nResources;
using LQClass.ModuleOfRoleManagement.ViewModels;
using LQClass.ModuleOfRoleManagement.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Unity;
using WpfExtensions.Xaml;

namespace LQClass.ModuleOfRoleManagement;

public class ModuleOfRoleManagementModule : IModule
{
    public const string KEY_OF_CURRENT_MODULE = "Role_Management";
    private readonly IRegionManager _regionManager;
    private readonly IUnityContainer _unityContainer;

    public ModuleOfRoleManagementModule(IRegionManager regionManager, IUnityContainer unityContainer)
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