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
using ModernWpf.Controls;
using Page = System.Windows.Controls.Page;

namespace Gui.Pages
{
    /// <summary>
    /// Interaction logic for DeliveryOptimizationPage.xaml
    /// </summary>
    public partial class DeliveryOptimizationPage : Page
    {
        public DeliveryOptimizationPage()
        {
            InitializeComponent();
        }

        private void GoBack(object sender, RoutedEventArgs e)
        {
            REMTS.MainWindow.NavigateBack();
        }
    }
}
