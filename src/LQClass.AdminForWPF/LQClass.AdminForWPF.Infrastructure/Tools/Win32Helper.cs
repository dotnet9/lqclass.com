﻿using System.Runtime.InteropServices;

namespace LQClass.AdminForWPF.Infrastructure.Tools;

public class Win32Helper
{
    [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
    public static extern bool SetForegroundWindow(nint hWnd);
}