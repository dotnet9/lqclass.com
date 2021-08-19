using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Xaml.Behaviors;
using System.Windows.Controls;
//using HandyControl.Controls;
namespace LQClass.AdminForWPF.Helper
{
	public static class PasswordBoxHelper
	{
		public static readonly DependencyProperty PasswordProperty =
			DependencyProperty.RegisterAttached("Password",
				typeof(string), typeof(PasswordBoxHelper),
				new FrameworkPropertyMetadata(string.Empty, OnPasswordPropertyChanged));

		private static void OnPasswordPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
		{
			var passwordBox = sender as PasswordBox;

			var password = (string)e.NewValue;

			if (passwordBox != null && passwordBox.Password != password) passwordBox.Password = password;
		}

		public static string GetPassword(DependencyObject dp)
		{
			return (string)dp.GetValue(PasswordProperty);
		}

		public static void SetPassword(DependencyObject dp, string value)
		{
			dp.SetValue(PasswordProperty, value);
		}
	}

	public class PasswordBoxBehavior : Behavior<PasswordBox>
	{
		protected override void OnAttached()
		{
			base.OnAttached();

			AssociatedObject.PasswordChanged += OnPasswordChanged;
		}

		private static void OnPasswordChanged(object sender, RoutedEventArgs e)
		{
			var passwordBox = sender as PasswordBox;

			var password = PasswordBoxHelper.GetPassword(passwordBox);

			if (passwordBox != null && passwordBox.Password != password)
				PasswordBoxHelper.SetPassword(passwordBox, passwordBox.Password);
		}

		protected override void OnDetaching()
		{
			base.OnDetaching();

			AssociatedObject.PasswordChanged -= OnPasswordChanged;
		}
	}
}
