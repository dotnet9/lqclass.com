using System;
using System.Windows;
using LQClass.CustomControls.Helpers;
using Prism.Services.Dialogs;

namespace LQClass.CustomControls.Controls;

/// <summary>
///     DialogWindow.xaml 的交互逻辑
/// </summary>
public partial class DialogWindow : Window, IDialogWindow
{
    public DialogWindow()
    {
        InitializeComponent();
    }

    public IDialogResult Result { get; set; }

    protected override void OnSourceInitialized(EventArgs e)
    {
        WindowsHelper.RemoveIcon(this); //使用win32函数去除Window的Icon部分
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        Width = Owner.Width;
        Height = Owner.Height;
        Top = Owner.Top;
        Left = Owner.Left;
    }
}