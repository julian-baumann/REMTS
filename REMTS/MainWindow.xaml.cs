using Gui.Dialogs;
using ModernWpf.Controls;
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
using RemoteExecuter.Entities;
using System.Collections.ObjectModel;
using Gui;
using Gui.Pages;
using Frame = ModernWpf.Controls.Frame;
using ModernWpf.Media.Animation;

namespace REMTS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Frame ContentFrame;

        private static Type _startPageInstance;

        public MainWindow()
        {
            InitializeComponent();

            ContentFrame = contentFrame;

            ContentFrame.Navigate(typeof(StartPage));

            _startPageInstance = ContentFrame.CurrentSourcePageType;
        }

        public static void NavigateToPage(Type page)
        {
            ContentFrame.Navigate(page, null, new DrillInNavigationTransitionInfo());
        }

        public static void NavigateBack()
        {
            ContentFrame.GoBack();
        }
    }
}
