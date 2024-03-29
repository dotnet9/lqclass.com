﻿using System.Windows;
using System.Windows.Media.Imaging;

namespace LQClass.CustomControls.Controls;

public partial class Avatar
{
    public static readonly DependencyProperty SourceProperty = DependencyProperty.Register(
        "Source", typeof(BitmapFrame), typeof(Avatar), new PropertyMetadata(default(BitmapFrame)));

    public static readonly DependencyProperty UserNameProperty = DependencyProperty.Register(
        "DisplayName", typeof(string), typeof(Avatar), new PropertyMetadata(default(string)));

    public static readonly DependencyProperty LinkProperty = DependencyProperty.Register(
        "Link", typeof(string), typeof(Avatar), new PropertyMetadata(default(string)));

    public Avatar()
    {
        InitializeComponent();
    }

    public BitmapFrame Source
    {
        get => (BitmapFrame)GetValue(SourceProperty);
        set => SetValue(SourceProperty, value);
    }

    public string DisplayName
    {
        get => (string)GetValue(UserNameProperty);
        set => SetValue(UserNameProperty, value);
    }

    public string Link
    {
        get => (string)GetValue(LinkProperty);
        set => SetValue(LinkProperty, value);
    }
}