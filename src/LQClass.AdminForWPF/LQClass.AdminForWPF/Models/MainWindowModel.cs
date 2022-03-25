using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using LQClass.AdminForWPF.I18nResources;
using LQClass.AdminForWPF.Infrastructure.Models;
using LQClass.AdminForWPF.Infrastructure.Mvvm;
using Prism.Regions;
using WpfExtensions.Xaml;

namespace LQClass.AdminForWPF.Models;

public class MainWindowModel : ViewModelBase
{
    public MainWindowModel(IRegionManager regionManager) : base(regionManager)
    {
    }

    /// <summary>
    ///     获取数据菜单
    /// </summary>
    /// <returns></returns>
    public ObservableCollection<CustomMenuItem> ReadCustomMenus()
    {
        var oldMenus = LoginResultDto.Instance.Attributes.Menus;
        var customMenus = new ObservableCollection<CustomMenuItem>();

        // 添加首页菜单
        var mainMenu = GetMenu(CustomMenuItem.KEY_OF_HOME);
        var newMenu = new CustomMenuItem(1, "1", "", mainMenu.Key, mainMenu.Value, $"./../Images/{mainMenu.Key}.png");
        customMenus.Add(newMenu);

        ReadChildren(1, string.Empty, oldMenus, customMenus);
        return customMenus;
    }

    /// <summary>
    ///     读取子菜单
    /// </summary>
    /// <param name="parentID"></param>
    /// <param name="customMenus"></param>
    private void ReadChildren(int level, string parentID, List<MenuInfo> soureMenus,
        ObservableCollection<CustomMenuItem> customMenus)
    {
        var findResults = soureMenus.ToList().FindAll(cu => cu.ParentIdIgnore == parentID);
        if (findResults == null || findResults.Count <= 0) return;
        foreach (var menuItem in findResults)
        {
            var key = menuItem.Text.Replace(' ', '_');
            var mainMenu = GetMenu(key);
            var newMenu = new CustomMenuItem(level, menuItem.Id, parentID, mainMenu.Key, mainMenu.Value,
                $"./../Images/{mainMenu.Key}.png");
            customMenus.Add(newMenu);
            ReadChildren(level + 1, newMenu.ID, soureMenus, newMenu.Children);
        }
    }

    /// <summary>
    ///     获取菜单对应的中文值
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public MainMenuItem GetMenu(string key)
    {
        var t = typeof(Language);
        var result = t.GetField(key, BindingFlags.Public | BindingFlags.Static);
        var resKey = Language.Home;
        if (result != null) resKey = result.GetValue(null) as ComponentResourceKey;
        var menuText = I18nManager.Instance.Get(resKey).ToString();
        return new MainMenuItem { Key = key, Value = menuText, Icon = "home.png" };
    }
}