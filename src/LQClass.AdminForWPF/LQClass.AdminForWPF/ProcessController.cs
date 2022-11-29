using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;

namespace LQClass.AdminForWPF;

public static class ProcessController
{
    private const int SW_SHOW_NORMAL = 1;
    private const int SW_RESTORE = 9;
    private const string PROCESS_NAME = "LQClass.AdminForWPF";

    private static Mutex _mutex;

    public static void OnWindowLoaded(object sender, RoutedEventArgs e)
    {
        var hwnd = ((HwndSource)PresentationSource.FromVisual((Visual)sender)).Handle;
        Settings.Default.WindowHandle = hwnd;
    }

    public static void Restart()
    {
        Settings.Default.IsRestarting = true;
        Process.Start($"{Directory.GetCurrentDirectory()}/{PROCESS_NAME}.exe");
        Application.Current.Shutdown();
    }

    public static void CheckSingleton()
    {
        _mutex = new Mutex(true, PROCESS_NAME, out var isNew);
        if (isNew || Settings.Default.IsRestarting)
        {
            Settings.Default.IsRestarting = false;
            return;
        }

        ActivateExistedWindow();
        Application.Current.Shutdown();
    }

    private static void ActivateExistedWindow()
    {
        var windowHandle = (nint)Settings.Default.WindowHandle;

        SetForegroundWindow(windowHandle);
        ShowWindowAsync(windowHandle, IsIconic(windowHandle) ? SW_RESTORE : SW_SHOW_NORMAL);
        GetForegroundWindow();
        FlashWindow(windowHandle, true);
    }

    #region Win32 API functions

    [DllImport("user32.dll")]
    private static extern bool ShowWindowAsync(nint hWnd, int nCmdShow);

    [DllImport("user32.dll")]
    private static extern bool SetForegroundWindow(nint hWnd);

    [DllImport("user32.dll")]
    private static extern nint GetForegroundWindow();

    [DllImport("user32.dll")]
    private static extern bool IsIconic(nint hWnd);

    [DllImport("user32.dll")]
    private static extern bool IsZoomed(nint hWnd);

    [DllImport("user32.dll")]
    private static extern bool FlashWindow(nint hWnd, bool bInvert);

    #endregion
}