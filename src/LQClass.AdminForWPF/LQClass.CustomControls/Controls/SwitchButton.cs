﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace LQClass.CustomControls.Controls
{
    /// <summary>
    /// SwitchButton.xaml 的交互逻辑
    /// </summary>
    public class SwitchButton : CheckBox
    {
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            "Text", typeof(string), typeof(SwitchButton), new PropertyMetadata("Off"));
        /// <summary>
        /// 默认文本（未选中）
        /// </summary>
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty CheckedTextProperty = DependencyProperty.Register(
            "CheckedText", typeof(string), typeof(SwitchButton), new PropertyMetadata("On"));
        /// <summary>
        /// 选中状态文本
        /// </summary>
        public string CheckedText
        {
            get { return (string)GetValue(CheckedTextProperty); }
            set { SetValue(CheckedTextProperty, value); }
        }

        public static readonly DependencyProperty CheckedForegroundProperty =
            DependencyProperty.Register("CheckedForeground", typeof(Brush), typeof(SwitchButton), new PropertyMetadata(Brushes.WhiteSmoke));
        /// <summary>
        /// 选中状态前景样式
        /// </summary>
        public Brush CheckedForeground
        {
            get { return (Brush)GetValue(CheckedForegroundProperty); }
            set { SetValue(CheckedForegroundProperty, value); }
        }

        public static readonly DependencyProperty CheckedBackgroundProperty =
            DependencyProperty.Register("CheckedBackground", typeof(Brush), typeof(SwitchButton), new PropertyMetadata(Brushes.LimeGreen));
        /// <summary>
        /// 选中状态背景色
        /// </summary>
        public Brush CheckedBackground
        {
            get { return (Brush)GetValue(CheckedBackgroundProperty); }
            set { SetValue(CheckedBackgroundProperty, value); }
        }

        static SwitchButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SwitchButton), new FrameworkPropertyMetadata(typeof(SwitchButton)));
        }
    }
}