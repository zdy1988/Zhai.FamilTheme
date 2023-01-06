
using System;
using System.Runtime.InteropServices;

namespace Zhai.Famil.Win32.Natives
{
    public static class Kernel32
    {
        private const string DLL_NAME = "kernel32.dll";

        [DllImport(DLL_NAME, EntryPoint = "GetPrivateProfileString")]
        public static extern int GetPrivateProfileString(string section, string key, string def, byte[] retVal, int size, string filePath);
        
        [DllImport(DLL_NAME, EntryPoint = "WritePrivateProfileString")]
        public static extern int WritePrivateProfileString(string section, string key, string val, string filePath);

        [DllImport(DLL_NAME, EntryPoint = "GetModuleHandle")]
        public static extern IntPtr GetModuleHandle(string name);
        
        [DllImport(DLL_NAME, EntryPoint = "GetCurrentThreadId")]
        private static extern int GetCurrentThreadId();

        [DllImport(DLL_NAME, EntryPoint = "SetProcessWorkingSetSize")]
        public static extern bool SetProcessWorkingSetSize(IntPtr proc, int min, int max);
    }
}
