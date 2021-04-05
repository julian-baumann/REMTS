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

namespace REMTS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string HelloWorld { get; set; } = "Hello World, this is a Test";
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;


            DataService dataService = new DataService();
            HelloWorld = dataService.Test();
        }
    }
}
