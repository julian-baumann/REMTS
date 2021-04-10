using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Printing;
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
using Namotion.Reflection;
using RemoteExecuter;
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

        public bool IsLoading { get; set; } = true;

        public string CurrentStatusText { get; set; }

        public DataTable TableData { get; set; } = new DataTable();

        public DeliveryOptimizationPage()
        {
            InitializeComponent();
            DataContext = this;

            Task.Run(() => PageRendered());
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
        }

        private void PageRendered()
        {
            if (_dataService.ComputerList?.Length > 0)
            {
                List<RemotePcInfo> errors = new List<RemotePcInfo>();

                bool headersSet = false;

                foreach (ConsoleResult consoleResult in DeliveryOptimization.RunFromList(_dataService.ComputerList))
                {
                    if (consoleResult.State == ConsoleResultStates.Running)
                    {
                        string updateStatus = $"{consoleResult.Pc.IpAddress} ({consoleResult.Pc.DisplayName}), {consoleResult.Index + 1} of {_dataService.ComputerList.Length}";
                        Application.Current.Dispatcher.Invoke(new Action(() => { statusTextBlock.Text = updateStatus; }));
                    }
                    else if (consoleResult.State == ConsoleResultStates.Done)
                    {

                        if (!headersSet)
                        {
                            TableData.Columns.Add("Variable-Name");

                            foreach (var item in consoleResult.Data)
                            {
                                DataRow newLabelRow = TableData.NewRow();
                                newLabelRow[0] = item.Name;
                                TableData.Rows.Add(newLabelRow);
                            }

                            headersSet = true;
                        }

                        string name = consoleResult.Pc.DisplayName?.Length > 0 ? consoleResult.Pc.DisplayName : consoleResult.Pc.IpAddress;

                        TableData.Columns.Add(new DataColumn(name));

                        for (int index = 0; index < consoleResult.Data.Length; index++)
                        {
                            TableData.Rows[index][TableData.Columns.IndexOf(name)] = consoleResult.Data[index].Value;
                        }
                    }
                    else if (consoleResult.State == ConsoleResultStates.Error)
                    {
                        errors.Add(consoleResult.Pc);
                    }

                    Application.Current.Dispatcher.Invoke(new Action(() => { dataGridTable.ItemsSource = TableData.AsDataView(); }));
                }


                string text = "Test";

                if (errors.Count > 0)
                {
                    text = $"Errors appeared on {string.Join(", ", errors.Select(x => $"{x.IpAddress} ({x.DisplayName})"))}";
                }

                Application.Current.Dispatcher.Invoke(new Action(() =>
                {
                    statusTextBlock.Text = text;
                    statusTextBlock.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("Red");
                    loadingSpinner.Visibility = Visibility.Hidden;
                }));
            }
        }
    }
}
