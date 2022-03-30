using LQClass.ModuleOfHome.I18nResources;
using LQClass.ModuleOfHome.ViewModels;
using LQClass.ModuleOfHome.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using WpfExtensions.Xaml;

namespace LQClass.ModuleOfHome;

public class ModuleOfHomeModule : IModule
{
    public const string KEY_OF_CURRENT_MODULE = "Home";
    private readonly IRegionManager _regionManager;

    public ModuleOfHomeModule(IRegionManager regionManager)
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