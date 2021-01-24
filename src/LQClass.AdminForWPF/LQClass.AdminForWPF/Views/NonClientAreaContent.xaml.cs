using HandyControl.Tools;
using LQClass.AdminForWPF.Infrastructure.Configs;
using LQClass.AdminForWPF.Infrastructure.Events;
using Prism.Events;
using System.Windows;
using System.Windows.Controls;

namespace LQClass.AdminForWPF.Views
{
	/// <summary>
	/// NoUserContent.xaml 的交互逻辑
	/// </summary>
	public partial class NonClientAreaContent
	{
		public IEventAggregator EventAggregator { get; set; }
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
				if (langName.Equals(AppSettingsHelper.Language)) return;
				AppSettingsHelper.SetLanguage(langName);
				ConfigHelper.Instance.SetLang(langName);
				EventAggregator.GetEvent<ChangeLanguageSentEvent>().Publish();
			}
		}
	}
}
