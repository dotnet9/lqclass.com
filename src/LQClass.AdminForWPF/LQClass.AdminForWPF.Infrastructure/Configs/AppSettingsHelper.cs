using System;
using System.Collections.Generic;
using System.Configuration;
using WpfExtensions.Xaml;

namespace LQClass.AdminForWPF.Infrastructure.Configs
{
	/// <summary>
	/// 对应config.json文件
	/// </summary>
	public class AppSettingsHelper
	{
		#region 配置文件中的配置属性

		/// <summary>
		/// 当前界面显示的语言
		/// </summary>
		public static string Language
		{
			get { return GetValue(nameof(Language)); }
			set { SetValue(nameof(Language), value); }
		}

		/// <summary>
		/// 是否记住我
		/// </summary>
		public static bool IsRemberMe
		{
			get { return bool.Parse(GetValue(nameof(IsRemberMe))); }
			set { SetValue(nameof(IsRemberMe), value.ToString()); }
		}

		/// <summary>
		/// 是否自动登录
		/// </summary>
		public static bool IsAutoLogin
		{
			get { return bool.Parse(GetValue(nameof(IsAutoLogin))); }
			set { SetValue(nameof(IsAutoLogin), value.ToString()); }
		}

		/// <summary>
		/// 上次登录账号
		/// </summary>
		public static string UserName
		{
			get { return GetValue(nameof(UserName)); }
			set { SetValue(nameof(UserName), value); }
		}

		/// <summary>
		/// 上次登录密码
		/// </summary>
		public static string Password
		{
			get { return GetValue(nameof(Password)); }
			set { SetValue(nameof(Password), value); }
		}

		/// <summary>
		/// 使用的Web API地址
		/// </summary>
		public static string API
		{
			get { return GetValue(nameof(API)); }
			set { SetValue(nameof(API), value); }
		}


		#endregion

		/// <summary>
		/// 修改界面语言
		/// </summary>
		/// <param name="language"></param>
		public static void SetLanguage(string language = "")
		{
			if (string.IsNullOrWhiteSpace(language))
			{
				language = Language;
				if (string.IsNullOrWhiteSpace(language))
				{
					language = System.Globalization.CultureInfo.CurrentCulture.ToString();
				}
			}

			Language = language;

			var culture = new System.Globalization.CultureInfo(language);
			I18nManager.Instance.CurrentUICulture = culture;
		}

		public static string GetValue(string key, string value = default(string))
		{
			try
			{
				Configuration cfa = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

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
			Configuration cfa = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
			cfa.AppSettings.Settings[key].Value = value;

			cfa.Save();
		}
	}
}
