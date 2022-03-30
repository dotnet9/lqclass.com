using LQClass.ModuleOfUserManagement.I18nResources;
using LQClass.ModuleOfUserManagement.ViewModels;
using LQClass.ModuleOfUserManagement.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using WpfExtensions.Xaml;

namespace LQClass.ModuleOfUserManagement;

public class ModuleOfUserManagementModule : IModule
{
    public const string KEY_OF_CURRENT_MODULE = "User_Management";
    private readonly IRegionManager _regionManager;

    public ModuleOfUserManagementModule(IRegionManager regionManager)
    {
        I18nManager.Instance.Add(UiResource.ResourceManager);
        _regionManager = regionManager;
    }

    public void OnInitialized(IContainerProvider containerProvider)
    {
    }

    public void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry.RegisterForNavigation<MainTabItemView, MainTabItemViewModel>(KEY_OF_CURRENT_MODULE);
    }
}