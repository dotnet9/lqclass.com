using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Xml.Linq;

namespace LQClass.AdminForWPF.Infrastructure.Configs;

public class QuickLinksSection
{
    public static List<QuickLink> GetAllQuickLinks()
    {
        var cfa = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        var doc = XDocument.Load(cfa.FilePath);
        var quickLinks = doc.Root.Elements("QuickLinks").Elements("QuickLink");
        return quickLinks.ToList().ConvertAll(cu => new QuickLink
        {
            Name = cu.Attribute("Name").Value,
            Desc = cu.Attribute("Desc").Value,
            Version = cu.Attribute("Version").Value,
            VersionColor = cu.Attribute("VersionColor").Value,
            Url = cu.Attribute("Url").Value
        });
    }
}

/// <summary>
///     超链接
/// </summary>
public class QuickLink
{
    /// <summary>
    ///     链接名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     版本或者状态
    /// </summary>
    public string Version { get; set; }

    /// <summary>
    ///     界面显示的版本颜色
    /// </summary>
    public string VersionColor { get; set; }

    /// <summary>
    ///     链接说明
    /// </summary>
    public string Desc { get; set; }

    /// <summary>
    ///     Url
    /// </summary>
    public string Url { get; set; }
}