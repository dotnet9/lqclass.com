using LQClass.ModuleOfLog.I18nResources;
using LQClass.ModuleOfLog.Models;
using LQClass.ModuleOfLog.ViewModels;
using LQClass.ModuleOfLog.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Unity;
using WpfExtensions.Xaml;

namespace LQClass.ModuleOfLog;

public class ModuleOfLogModule : IModule
{
    public const string KEY_OF_CURRENT_MODULE = "Log";
    private readonly IRegionManager _regionManager;
    private readonly IUnityContainer _unityContainer;

    public ModuleOfLogModule(IRegionManager regionManager, IUnityContainer unityContainer)
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
        containerRegistry.RegisterSingleton<MainTabItemModel>();

        containerRegistry.RegisterDialog<AddView, AddViewModel>("AddLogView");
    }
}