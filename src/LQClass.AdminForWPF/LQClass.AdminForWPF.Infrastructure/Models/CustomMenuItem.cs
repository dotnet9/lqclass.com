using System.Collections.ObjectModel;
using Prism.Mvvm;

namespace LQClass.AdminForWPF.Infrastructure.Models;

public class CustomMenuItem : BindableBase
{
    public const string KEY_OF_HOME = "Home";
    private bool _IsExpanded;


    private bool _IsSelected;

    public CustomMenuItem(int level, string id, string parentID, string key, string name, string icon)
    {
        Level = level;
        ID = id;
        ParentID = parentID;
        Key = key;
        Name = name;
        Icon = icon;
    }

    public string ID { get; set; }
    public string ParentID { get; set; }
    public string Key { get; set; }
    public string Name { get; set; }
    public int Level { get; set; }
    public string Icon { get; set; }

    public ObservableCollection<CustomMenuItem> Children { get; set; } = new();

    /// <summary>
    ///     new IsSelected
    /// </summary>
    public bool IsSelected
    {
        get => _IsSelected;
        set => SetProperty(ref _IsSelected, value);
    }

    /// <summary>
    ///     new IsExpanded
    /// </summary>
    public bool IsExpanded
    {
        get => _IsExpanded;
        set => SetProperty(ref _IsExpanded, value);
    }
}