using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UserInterface.Pages;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace UserInterface
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public static Frame ContentFrame;

        public static event EventHandler PageRendered;

        public MainWindow()
        {
            InitializeComponent();

            ContentFrame = contentFrame;

            ContentFrame.Navigate(typeof(StartPage));
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
