using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using Zhai.Famil.Controls.Notifiactions;

namespace Zhai.Famil.Demo
{
    /// <summary>
    /// MainWindowContent.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindowContent : UserControl
    {
        MainWindowViewModel ViewModel => this.DataContext as MainWindowViewModel;

        public MainWindowContent()
        {
            InitializeComponent();

            this.DataContext = new MainWindowViewModel();

            //var content = new NotificationContent
            //{
            //    Message = "Pink string from another thread!Pink string from another thread!Pink string from another thread!",
            //    Type = NotificationType.Information
            //};

            //var timer = new Timer { Interval = 3000 };
            //timer.Elapsed += (sender, args) => NotificationManager.Default.Show(content);
            //timer.Start();
        }

        private void HintTestButton_Click(object sender, RoutedEventArgs e)
        {
            this.ViewModel.HintText = $"time:{DateTime.Now}";
        }

        private void ListBox_DataChange_Click(object sender, RoutedEventArgs e)
        {
            this.ViewModel.ListLines = this.ViewModel.ListLines.Count() == 3 ? ViewModel.TextLines : ViewModel.TextLines3;
        }

        private void NativeWindow_Button_Click(object sender, RoutedEventArgs e)
        {
            (new NativeWindow()).Show(); 
        }

        private void TransparentWindow_Button_Click(object sender, RoutedEventArgs e)
        {
            (new MainWindow()).Show();
        }

        private void GlassesWindow_Button_Click(object sender, RoutedEventArgs e)
        {
            (new MainWindow2()).Show();
        }

        private void ColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            
        }
    }
}
