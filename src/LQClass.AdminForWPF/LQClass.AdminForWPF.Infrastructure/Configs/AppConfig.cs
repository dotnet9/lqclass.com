using LQClass.AdminForWPF.Infrastructure.Tools;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using WpfExtensions.Xaml;

namespace LQClass.AdminForWPF.Infrastructure.Configs
{
  /// <summary>
  /// 对应config.json文件
  /// </summary>
  public class AppConfig
  {
    #region 配置文件中的配置属性

    /// <summary>
    /// 应用名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 当前界面显示的语言
    /// </summary>
    public string Language { get; set; }

    /// <summary>
    /// 是否记住我
    /// </summary>
    public bool IsRemberMe { get; set; }

    /// <summary>
    /// 是否自动登录
    /// </summary>
    public bool IsAutoLogin { get; set; }

    /// <summary>
    /// 上次登录账号
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// 上次登录密码
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    /// 使用的配置类型
    /// </summary>
    public string ConfigType { get; set; }

    /// <summary>
    /// 超链接
    /// </summary>
    public List<QuickLink> QuickLinks { get; set; }

    #endregion

    /// <summary>
    /// 运行配置，分为开发环境、生产环境
    /// </summary>
    public ConfigDesc RunningConfig { get; set; }

    private static object lockObj = new object();

    private static AppConfig _Instance = null;

    /// <summary>
    /// 配置单例
    /// </summary>
    public static AppConfig Instance
    {
      get
      {
        if (_Instance != null)
        {
          return _Instance;
        }
        lock (lockObj)
        {
          var configFile = $"{AppDomain.CurrentDomain.BaseDirectory}config.json";
          var configContent = File.ReadAllText(configFile);
          _Instance = JsonConvert.DeserializeObject<AppConfig>(configContent);

          var configDescFile = $"{AppDomain.CurrentDomain.BaseDirectory}config.{_Instance.ConfigType}.json";
          var configDescContent = File.ReadAllText(configDescFile);
          _Instance.RunningConfig = JsonConvert.DeserializeObject<ConfigDesc>(configDescContent);

          // 配置文件中的账号和密码为加密后的密文
          if (!string.IsNullOrWhiteSpace(_Instance.UserName))
          {
            _Instance.UserName = DESHelper.Decrypt3Des(_Instance.UserName);
          }
          if (!string.IsNullOrWhiteSpace(_Instance.Password))
          {
            _Instance.Password = DESHelper.Decrypt3Des(_Instance.Password);
          }
        }
        return _Instance;
      }
    }

    /// <summary>
    /// 修改界面语言
    /// </summary>
    /// <param name="language"></param>
    public void SetLanguage(string language = "")
    {
      if (string.IsNullOrWhiteSpace(language))
      {
        language = _Instance.Language;
        if (string.IsNullOrWhiteSpace(language))
        {
          language = System.Globalization.CultureInfo.CurrentCulture.ToString();
        }
      }

      _Instance.Language = language;

      var culture = new System.Globalization.CultureInfo(language);
      I18nManager.Instance.CurrentUICulture = culture;
    }

    /// <summary>
    /// 保存配置文件
    /// </summary>
    public void Save()
    {
      var jsonStr = JsonConvert.SerializeObject(_Instance);
      var formatStr = JsonHelper.FormatJsonString(jsonStr);
      var configFile = $"{AppDomain.CurrentDomain.BaseDirectory}config.json";
      if (File.Exists(configFile))
      {
        File.Delete(configFile);
      }
      File.AppendAllText(configFile, formatStr);
    }
    private AppConfig() { }
  }

  /// <summary>
  /// 配置详细信息
  /// </summary>
  public class ConfigDesc
  {
    /// <summary>
    /// api地址
    /// </summary>
    public string API { get; set; }
  }

  /// <summary>
  /// 超链接
  /// </summary>
  public class QuickLink
  {
    /// <summary>
    /// 链接名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 链接说明
    /// </summary>
    public string Desc { get; set; }

    /// <summary>
    /// Url
    /// </summary>
    public string Url { get; set; }
  }
}