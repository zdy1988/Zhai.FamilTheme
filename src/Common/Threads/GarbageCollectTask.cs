using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Threading;

namespace Zhai.Famil.Common.Threads
{
    public class GarbageCollectTask
    {
        DispatcherTimer GCTimer;

        public void Run(TimeSpan interval)
        {
            if (interval == default || interval == TimeSpan.Zero) return;

            GCTimer = new DispatcherTimer
            {
                Interval = interval
            };
            GCTimer.Start();
            GCTimer.Tick += (s, e) =>
            {

                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                {
                    Win32.Natives.Kernel32.SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);
                }
            };
        }
    }

    public static class GarbageCollectIntervalTaskExtension
    {
        public static void UseGarbageCollectIntervalTask(this System.Windows.Application application, TimeSpan interval)
        {
            new GarbageCollectTask().Run(interval);
        }
    }
}
