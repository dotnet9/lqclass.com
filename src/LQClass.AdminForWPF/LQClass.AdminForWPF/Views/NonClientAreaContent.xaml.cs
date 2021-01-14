using HandyControl.Properties.Langs;
using HandyControl.Tools;
using LQClass.AdminForWPF.Infrastructure.Configs;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace LQClass.AdminForWPF.Views
{
	/// <summary>
	/// NoUserContent.xaml 的交互逻辑
	/// </summary>
	public partial class NonClientAreaContent
	{
		public NonClientAreaContent()
		{
			InitializeComponent();
		}

		private void ButtonConfig_OnClick(object sender, RoutedEventArgs e) => PopupConfig.IsOpen = true;

		private void ButtonSkins_OnClick(object sender, System.Windows.RoutedEventArgs e)
		{

		}

		private void ButtonLangs_OnClick(object sender, System.Windows.RoutedEventArgs e)
		{
			if (e.OriginalSource is Button button && button.Tag is string langName)
			{
				PopupConfig.IsOpen = false;
				if (langName.Equals(AppConfig.Instance.Language)) return;
				AppConfig.Instance.SetLanguage(langName);
				ConfigHelper.Instance.SetLang(langName);
				//LangProvider.Culture = new CultureInfo(langName);
				//Messenger.Default.Send<object>(null, "LangUpdated");

				//GlobalData.Config.Lang = langName;
				//GlobalData.Save();
			}
		}
	}
}
