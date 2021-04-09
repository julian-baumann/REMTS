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
using REMTS;
using Page = System.Windows.Controls.Page;

namespace Gui.Pages
{
    /// <summary>
    /// Interaction logic for DeliveryOptimizationPage.xaml
    /// </summary>
    public partial class DeliveryOptimizationPage : Page
    {
        private DataService _dataService = DataService.Instance;

        public DeliveryOptimizationPage()
        {
            InitializeComponent();

            MainWindow.PageRendered += PageRendered;
        }

        private void GoBack(object sender, RoutedEventArgs e)
        {
            MainWindow.NavigateBack();
        }

        private void PageRendered(object sender, EventArgs e)
        {
            _dataService.RunDeliveryOptimization();
        }
    }
}
