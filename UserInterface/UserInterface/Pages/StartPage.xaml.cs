using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using RemoteExecuter.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UserInterface.Services;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace UserInterface.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class StartPage : Page
    {
        private DataService _dataService = DataService.Instance;

        public ObservableCollection<RemotePcInfo> Computers { get; set; } = new ObservableCollection<RemotePcInfo>();

        public StartPage()
        {
            InitializeComponent();
            DataContext = this;
        }


        private async void AddNewPcClicked(object sender, RoutedEventArgs e)
        {
            /*AddPcDialog dialog = new AddPcDialog();
            ContentDialogResult result = await dialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                Computers.Add(dialog.PcInfo);
            }*/
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
        private async void SaveComputerListToFileClicked(object sender, RoutedEventArgs e)
        {
            await StorageService.SavePcData(Computers.ToList());
        }

        private async void LoadComputersClicked(object sender, RoutedEventArgs e)
        {
            RemotePcInfo[] computers = await StorageService.LoadPcDataFromFile();

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

        private void DoPerfSnapClick(object sender, RoutedEventArgs e)
        {
            _dataService.ComputerList = Computers.ToArray();
            /*MainWindow.NavigateToPage(typeof(DeliveryOptimizationPage));*/
        }
    }
}
