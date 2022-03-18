using System.Configuration;
using System.Globalization;
using WpfExtensions.Xaml;

namespace LQClass.AdminForWPF.Infrastructure.Configs;

/// <summary>
///     对应config.json文件
/// </summary>
public class AppSettingsHelper
{
    /// <summary>
    ///     修改界面语言
    /// </summary>
    /// <param name="language"></param>
    public static void SetLanguage(string language = "")
    {
        if (string.IsNullOrWhiteSpace(language))
        {
            language = Language;
            if (string.IsNullOrWhiteSpace(language)) language = CultureInfo.CurrentCulture.ToString();
        }

        Language = language;

        var culture = new CultureInfo(language);
        I18nManager.Instance.CurrentUICulture = culture;
    }

    public static string GetValue(string key, string value = default)
    {
        try
        {
            var cfa = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            return cfa.AppSettings.Settings[key].Value;
        }
        catch
        {
            SetValue(key, value);
            return value;
        }
    }

    public static void SetValue(string key, string value)
    {
        var cfa = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        cfa.AppSettings.Settings[key].Value = value;

        cfa.Save();
    }

    #region 配置文件中的配置属性

    /// <summary>
    ///     当前界面显示的语言
    /// </summary>
    public static string Language
    {
        get => GetValue(nameof(Language));
        set => SetValue(nameof(Language), value);
    }

    /// <summary>
    ///     是否记住我
    /// </summary>
    public static bool IsRemberMe
    {
        get => bool.Parse(GetValue(nameof(IsRemberMe)));
        set => SetValue(nameof(IsRemberMe), value.ToString());
    }

    /// <summary>
    ///     是否自动登录
    /// </summary>
    public static bool IsAutoLogin
    {
        get => bool.Parse(GetValue(nameof(IsAutoLogin)));
        set => SetValue(nameof(IsAutoLogin), value.ToString());
    }

    /// <summary>
    ///     上次登录账号
    /// </summary>
    public static string UserName
    {
        get => GetValue(nameof(UserName));
        set => SetValue(nameof(UserName), value);
    }

    /// <summary>
    ///     上次登录密码
    /// </summary>
    public static string Password
    {
        get => GetValue(nameof(Password));
        set => SetValue(nameof(Password), value);
    }

    /// <summary>
    ///     使用的Web API地址
    /// </summary>
    public static string API
    {
        get => GetValue(nameof(API));
        set => SetValue(nameof(API), value);
    }

    #endregion
}