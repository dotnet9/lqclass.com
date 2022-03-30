using LQClass.ModuleOfGroupManagement.I18nResources;
using LQClass.ModuleOfGroupManagement.ViewModels;
using LQClass.ModuleOfGroupManagement.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using WpfExtensions.Xaml;

namespace LQClass.ModuleOfGroupManagement;

public class ModuleOfGroupManagementModule : IModule
{
    public const string KEY_OF_CURRENT_MODULE = "Group_Management";
    private readonly IRegionManager _regionManager;

    public ModuleOfGroupManagementModule(IRegionManager regionManager)
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