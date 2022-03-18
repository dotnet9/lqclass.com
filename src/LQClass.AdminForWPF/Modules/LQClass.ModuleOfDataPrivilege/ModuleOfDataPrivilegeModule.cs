using LQClass.ModuleOfDataPrivilege.I18nResources;
using LQClass.ModuleOfDataPrivilege.ViewModels;
using LQClass.ModuleOfDataPrivilege.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Unity;
using WpfExtensions.Xaml;

namespace LQClass.ModuleOfDataPrivilege;

public class ModuleOfDataPrivilegeModule : IModule
{
    public const string KEY_OF_CURRENT_MODULE = "Data_Privilege";
    private readonly IRegionManager _regionManager;
    private readonly IUnityContainer _unityContainer;

    public ModuleOfDataPrivilegeModule(IRegionManager regionManager, IUnityContainer unityContainer)
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