using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Zhai.FamilTheme;

namespace Zhai.FamilThemeDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : TransparentWindow
    {
        MainWindowViewModel ViewModel => this.DataContext as MainWindowViewModel;

        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = new MainWindowViewModel();
        }

        private void HintTestButton_Click(object sender, RoutedEventArgs e)
        {
            this.ViewModel.HintText = $"time:{DateTime.Now}";
        }

        private void ListBox_DataChange_Click(object sender, RoutedEventArgs e)
        {
            this.ViewModel.ListLines = this.ViewModel.ListLines.Count() == 3 ? ViewModel.TextLines : ViewModel.TextLines3;
        }
    }
}
