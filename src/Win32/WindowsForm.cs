using System;
using System.Runtime.InteropServices;
using static Zhai.Famil.Win32.Natives.User32;
using Zhai.Famil.Win32.Utility;

namespace Zhai.Famil.Win32
{
    public static class WindowsForm
    {
        public static void Resize(IntPtr intPtr, WindowResizeDirection direction)
        {
            SendMessage(intPtr, WM_WindowsMessage.WM_SYSCOMMAND, (IntPtr)(61440 + direction), IntPtr.Zero);
        }

        public static void EnableBlur(IntPtr hwnd, AccentFlagsType style = AccentFlagsType.Window)
        {
            var accent = new AccentPolicy();
            var accentStructSize = Marshal.SizeOf(accent);
            // ウィンドウ背景のぼかしを行うのはWindows10の場合のみ
            // OSのバージョンに従い、AccentStateを切り替える
            var currentVersion = SystemInfo.Version.Value;
            if (currentVersion >= VersionInfos.Windows10_1903)
            {
                // Windows10 1903以降では、ACCENT_ENABLE_ACRYLICBLURBEHINDを用いると、ウィンドウのドラッグ移動などでマウス操作に追従しなくなる。
                // SetWindowCompositionAttribute関数の動作が修正されるまで、ACCENT_ENABLE_ACRYLICBLURBEHINDは使用しない。
                accent.AccentState = AccentState.ACCENT_ENABLE_BLURBEHIND;
            }
            else if (currentVersion >= VersionInfos.Windows10_1809)
            {
                accent.AccentState = AccentState.ACCENT_ENABLE_ACRYLICBLURBEHIND;
            }
            else if (currentVersion >= VersionInfos.Windows10)
            {
                accent.AccentState = AccentState.ACCENT_ENABLE_BLURBEHIND;
            }
            else
            {
                accent.AccentState = AccentState.ACCENT_ENABLE_TRANSPARENTGRADIENT;
            }

            if (style == AccentFlagsType.Window)
            {
                accent.AccentFlags = 2;
            }
            else
            {
                accent.AccentFlags = 0x20 | 0x40 | 0x80 | 0x100;
            }

            //accent.GradientColor = 0x99FFFFFF;  // 60%の透明度が基本
            accent.GradientColor = 0x00FFFFFF;  // Tint Colorはここでは設定せず、Bindingで外部から変えられるようにXAML側のレイヤーとして定義

            var accentPtr = Marshal.AllocHGlobal(accentStructSize);
            Marshal.StructureToPtr(accent, accentPtr, false);

            var data = new WindowCompositionAttributeData
            {
                Attribute = WindowCompositionAttribute.WCA_ACCENT_POLICY,
                SizeOfData = accentStructSize,
                Data = accentPtr
            };

            SetWindowCompositionAttribute(hwnd, ref data);

            Marshal.FreeHGlobal(accentPtr);
        }
    }

    public enum WindowResizeDirection
    {
        Left = 1,
        Right = 2,
        Top = 3,
        TopLeft = 4,
        TopRight = 5,
        Bottom = 6,
        BottomLeft = 7,
        BottomRight = 8,
    }
}
