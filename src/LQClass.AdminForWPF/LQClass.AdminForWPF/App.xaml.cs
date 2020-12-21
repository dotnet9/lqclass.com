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

namespace LQClass.AdminForWPF
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : PrismApplication
	{
		protected override Window CreateShell()
		{
			return Container.Resolve<LoginView>();
		}

		protected override void RegisterTypes(IContainerRegistry containerRegistry)
		{
			containerRegistry.RegisterForNavigation<LoginView, LoginViewModel>();
      containerRegistry.RegisterSingleton<LoginModel>();
		}
	}
}
