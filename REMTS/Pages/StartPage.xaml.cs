using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Gui.Dialogs;
using ModernWpf.Controls;
using RemoteExecuter.Entities;
using ModernWpf.Media.Animation;
using Page = System.Windows.Controls.Page;

namespace Gui.Pages
{
    /// <summary>
    /// Interaction logic for StartPage.xaml
    /// </summary>
    public partial class StartPage : Page
    {
        public StartPage()
        {
            InitializeComponent();
            DataContext = this;
        }

        public ObservableCollection<RemotePcInfo> Computers { get; set; } = new ObservableCollection<RemotePcInfo>();

        public async void AddNewPcClicked(object sender, RoutedEventArgs e)
        {
            AddPcDialog dialog = new AddPcDialog();
            ContentDialogResult result = await dialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                Computers.Add(dialog.PcInfo);
            }
        }

        private void DeletePcClicked(object sender, RoutedEventArgs e)
        {
            while (computersList.SelectedItems.Count > 0)
            {
                if ((RemotePcInfo)computersList.SelectedItem != null)
                {
                    Computers.Remove((RemotePcInfo)computersList.SelectedItem);
                }
            }

        }
        private void SaveComputerListToFileClicked(object sender, RoutedEventArgs e)
        {
            StorageService.SavePcData(Computers.ToList());
        }

        private void LoadComputersClicked(object sender, RoutedEventArgs e)
        {
            RemotePcInfo[] computers = StorageService.LoadPcDataFromFile();

            if (computers.Length > 0)
            {
                Computers.Clear();

                foreach (RemotePcInfo pc in computers)
                {
                    Computers.Add(pc);
                }
            }

        }

        private void ComputerSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            deletePcButton.IsEnabled = computersList.SelectedItems.Count > 0;
        }

        private void SaveCommandPressed(object sender, ExecutedRoutedEventArgs args)
        {
            StorageService.SavePcData(Computers.ToList());
        }

        private void DoPerfSnapClick(object sender, RoutedEventArgs e)
        {
            REMTS.MainWindow.NavigateToPage(typeof(DeliveryOptimizationPage));
        }
    }
}
