using LQClass.AdminForWPF.Infrastructure.Configs;
using LQClass.AdminForWPF.Models;
using LQClass.AdminForWPF.ViewModels;
using LQClass.AdminForWPF.Views;
using Prism.Ioc;
using Prism.Unity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WpfExtensions.Xaml;

namespace LQClass.AdminForWPF
{
  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  public partial class App : PrismApplication
  {
    protected override void OnStartup(StartupEventArgs e)
    {
      base.OnStartup(e);

      DispatcherUnhandledException += App_DispatcherUnhandledException;
      AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
      TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
    }



    protected override Window CreateShell()
    {
      I18nManager.Instance.Add(I18nResources.UiResource.ResourceManager);
      AppConfig.Instance.SetLanguage();
      return Container.Resolve<LoginView>();
    }

    protected override void RegisterTypes(IContainerRegistry containerRegistry)
    {
      containerRegistry.RegisterForNavigation<LoginView, LoginViewModel>();
      containerRegistry.RegisterSingleton<LoginModel>();
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
