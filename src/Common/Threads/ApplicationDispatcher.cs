using System;
using System.Windows;
using System.Windows.Threading;

namespace Zhai.Famil.Common.Threads
{
    public static class ApplicationDispatcher
    {
        public static void DoEvents()
        {
            DispatcherFrame frame = new DispatcherFrame();
            DispatcherOperationCallback dispatcherOperationCallback = new DispatcherOperationCallback((object frame) =>
            {
                ((DispatcherFrame)frame).Continue = false;

                return null;
            });
            Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Background, dispatcherOperationCallback, frame);
            Dispatcher.PushFrame(frame);
        }

        public static void InvokeOnUIThread(Action action)
        {
            var dispatcher = Application.Current != null && Application.Current.Dispatcher != null
                    ? Application.Current.Dispatcher
                    : Dispatcher.CurrentDispatcher;

            if (dispatcher.CheckAccess())
            {
                action();
            }
            else
            {
                dispatcher.Invoke(action);
            }
        }
    }
}
