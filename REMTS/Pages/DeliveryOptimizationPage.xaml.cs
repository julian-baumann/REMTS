using System;
using System.Collections.Generic;
using System.Data;
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
using RemoteExecuter.Entities;
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

        public ConsoleResultItem[][] Items { get; set; }


        public DeliveryOptimizationPage()
        {
            InitializeComponent();

            MainWindow.PageRendered += PageRendered;
        }

        private void GoBack(object sender, RoutedEventArgs e)
        {
            MainWindow.NavigateBack();
        }

        private void RenderTable(ConsoleResultItem[][] data)
        {
            int rows = data.GetLength(0);
            int columns = data.GetLength(1);

            DataTable table = new DataTable();

            for (int i = 0; i < columns; i++)
            {
                table.Columns.Add(new DataColumn(data[i].))
            }
        }

        private void PageRendered(object sender, EventArgs e)
        {
            Items = _dataService.RunDeliveryOptimization();
        }
    }
}
