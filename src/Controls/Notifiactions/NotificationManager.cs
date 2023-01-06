using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Threading;
using Zhai.Famil.Common;

namespace Zhai.Famil.Controls.Notifiactions
{
    public class NotificationManager : INotificationManager
    {
        public static NotificationManager Default => SingletonProvider.Get<NotificationManager>();

        private readonly Dispatcher _dispatcher;
        private static readonly List<NotificationArea> Areas = new List<NotificationArea>();
        private static NotificationsOverlayWindow _window;

        public NotificationManager(Dispatcher dispatcher = null)
        {
            if (dispatcher == null)
            {
                dispatcher = Application.Current?.Dispatcher ?? Dispatcher.CurrentDispatcher;
            }

            _dispatcher = dispatcher;
        }

        public NotificationManager()
            : this(null)
        { }

        public void Show(object content, string areaName = "", TimeSpan? expirationTime = null, Action onClick = null, Action onClose = null)
        {
            if (!_dispatcher.CheckAccess())
            {
                _dispatcher.BeginInvoke(
                    new Action(() => Show(content, areaName, expirationTime, onClick, onClose)));
                return;
            }

            if (expirationTime == null) expirationTime = TimeSpan.FromSeconds(5);

            if (areaName == string.Empty && _window == null)
            {
                var workArea = SystemParameters.WorkArea;

                _window = new NotificationsOverlayWindow
                {
                    Left = workArea.Left,
                    Top = workArea.Top,
                    Width = workArea.Width,
                    Height = workArea.Height
                };

                _window.Closed += _window_Closed;

                _window.Show();
            }

            foreach (var area in Areas.Where(a => a.Name == areaName))
            {
                area.Show(content, (TimeSpan)expirationTime, onClick, () =>
                {
                    onClose?.Invoke();

                    if (Areas.All(t => t.ItemsCount <= 0))
                    {
                        _window?.Close();
                    }
                });
            }
        }

        public void Show(object content, Action onClick = null, Action onClose = null) => Show(content, "", null, onClick, onClose);

        private void _window_Closed(object sender, EventArgs e)
        {
            _window.Closed -= _window_Closed;
            _window = null;
        }

        internal static void AddArea(NotificationArea area)
        {
            Areas.Add(area);
        }
    }
}