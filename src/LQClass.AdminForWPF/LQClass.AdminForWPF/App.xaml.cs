using LQClass.AdminForWPF.CommonService;
using LQClass.AdminForWPF.ICommonService;
using LQClass.AdminForWPF.Infrastructure.Configs;
using LQClass.AdminForWPF.Infrastructure.Tools;
using LQClass.AdminForWPF.Models;
using LQClass.AdminForWPF.ViewModels;
using LQClass.AdminForWPF.Views;
using LQClass.CustomControls.Controls;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Unity;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using WpfExtensions.Xaml;

namespace LQClass.AdminForWPF
{
	public partial class App : PrismApplication
	{
		private static Mutex AppMutex;

		protected override void OnStartup(StartupEventArgs e)
		{
			var current = Process.GetCurrentProcess();
			var name = System.IO.Path.GetFileNameWithoutExtension(current.MainModule.FileName);
			AppMutex = new Mutex(true, name, out var createdNew);

			if (!createdNew)
			{
				foreach (var process in Process.GetProcessesByName(current.ProcessName))
				{
					if (process.Id != current.Id)
					{
						Win32Helper.SetForegroundWindow(process.MainWindowHandle);
						break;
					}
				}
				Shutdown();
			}
			else
			{
				//var splashScreenImg = $"Images{Path.DirectorySeparatorChar}Cover.png";
				//var splashScreen = new SplashScreen(splashScreenImg);
				//splashScreen.Show(true);
				ShutdownMode = ShutdownMode.OnMainWindowClose;
				AppConfig.Instance.SetLanguage(AppConfig.Instance.Language);
				base.OnStartup(e);

			}

			DispatcherUnhandledException += App_DispatcherUnhandledException;
			AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
			TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
		}



		protected override Window CreateShell()
		{
			I18nManager.Instance.Add(LQClass.AdminForWPF.I18nResources.UiResource.ResourceManager);
			AppConfig.Instance.SetLanguage();
			return Container.Resolve<MainWindowView>();
		}

		protected override void InitializeShell(Window shell)
		{
			LoginView loginView = new LoginView();
			if (loginView.ShowDialog() == true)
			{
				var shellVM = shell.DataContext as MainWindowViewModel;
				shellVM.InitData();
				base.InitializeShell(shell);
			}
			else
			{
				Application.Current.Shutdown(-1);
			}
		}

		protected override void RegisterTypes(IContainerRegistry containerRegistry)
		{
			containerRegistry.RegisterSingleton<LoginModel>();
			containerRegistry.RegisterSingleton<MainWindowModel>();
			containerRegistry.Register<ISystemMenuService, SystemMenuService>();
			containerRegistry.RegisterDialogWindow<DialogWindow>();//注册自定义对话框窗体
			containerRegistry.RegisterDialog<QQGroupView, QQGroupViewModel>("QQGroup");
			containerRegistry.RegisterDialog<AboutView, AboutViewModel>("About");
			containerRegistry.RegisterDialog<WebView, WebViewModel>("Web");
		}

		protected override IModuleCatalog CreateModuleCatalog()
		{
			string modulePath = @".\Modules";
			if (!Directory.Exists(modulePath))
			{
				Directory.CreateDirectory(modulePath);
			}
			return new DirectoryModuleCatalog() { ModulePath = modulePath };
		}

		#region 异常处理

		/// <summary>
		/// 统一异常处理（UI线程未捕获异常处理事件）
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
		{
			Exception ex = e.Exception;
			MessageBox.Show($"程序运行出错，原因：{ex.Message}-{ex.InnerException?.Message}", "系统提示", MessageBoxButton.OK, MessageBoxImage.Error);
			e.Handled = true;//表示异常已处理，可以继续运行
		}

		/// <summary>
		/// 统一异常处理（非UI线程未捕获异常处理事件(例如自己创建的一个子线程)）
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			Exception ex = e.ExceptionObject as Exception;
			if (ex != null)
			{
				MessageBox.Show($"程序组件出错，原因：{ex.Message}", "系统提示", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		/// <summary>
		/// 统一异常处理（Task任务异常）
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
		{
			Exception ex = e.Exception;
			MessageBox.Show($"执行任务出错，原因：{ex.Message}", "系统提示", MessageBoxButton.OK, MessageBoxImage.Error);
		}

		#endregion
	}
}
