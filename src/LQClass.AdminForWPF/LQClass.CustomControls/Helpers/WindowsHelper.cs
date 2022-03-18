using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace LQClass.CustomControls.Helpers;

public static class WindowsHelper
{
    private const int GWL_EXSTYLE = -20;
    private const int WS_EX_DLGMODALFRAME = 0x0001;
    private const int SWP_NOSIZE = 0x0001;
    private const int SWP_NOMOVE = 0x0002;
    private const int SWP_NOZORDER = 0x0004;
    private const int SWP_FRAMECHANGED = 0x0020;
    private const uint WM_SETICON = 0x0080;

    [DllImport("user32.dll")]
    private static extern int GetWindowLong(IntPtr hwnd, int index);

    [DllImport("user32.dll")]
    private static extern int SetWindowLong(IntPtr hwnd, int index, int newStyle);

    [DllImport("user32.dll")]
    private static extern bool SetWindowPos(IntPtr hwnd, IntPtr hwndInsertAfter,
        int x, int y, int width, int height, uint flags);

    [DllImport("user32.dll")]
    private static extern IntPtr SendMessage(IntPtr hwnd, uint msg,
        IntPtr wParam, IntPtr lParam);

    public static void RemoveIcon(Window window)
    {
        //获取窗体的句柄
        var hwnd = new WindowInteropHelper(window).Handle;

        //改变窗体的样式
        var extendedStyle = GetWindowLong(hwnd, GWL_EXSTYLE);
        SetWindowLong(hwnd, GWL_EXSTYLE, extendedStyle | WS_EX_DLGMODALFRAME);

        //更新窗口的非客户区，以反映变化
        SetWindowPos(hwnd, IntPtr.Zero, 0, 0, 0, 0, SWP_NOMOVE |
                                                    SWP_NOSIZE | SWP_NOZORDER | SWP_FRAMECHANGED);
    }
}