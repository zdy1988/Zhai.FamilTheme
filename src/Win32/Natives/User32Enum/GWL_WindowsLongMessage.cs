﻿namespace Zhai.Famil.Win32.Natives
{
    public static partial class User32
    {
        public enum GWL_WindowsLongMessage : int
        {
            GWL_WNDPROC = -4,
            GWL_HINSTANCE = -6,
            GWL_HWNDPARENT = -8,
            GWL_STYLE = -16,
            GWL_EXSTYLE = -20,
            GWL_USERDATA = -21,
            GWL_ID = -12,
        }
    }
}
