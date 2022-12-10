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
using Zhai.FamilyContorls;

namespace Zhai.FamilyContorlsDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : TransparentWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            this.IconList.ItemsSource = Enum.GetValues<IconKind>();
        }
    }
}
