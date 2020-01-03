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

namespace LFSR
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void geffeChecked(object sender, RoutedEventArgs e)
        {
            // enable needed registers
            lfsr1Panel.IsEnabled = true;
            lfsr2Panel.IsEnabled = true;
            lfsr3Panel.IsEnabled = true;
        }

        private void stopAndGoChecked(object sender, RoutedEventArgs e)
        {
            // enable needed registers
            lfsr1Panel.IsEnabled = true;
            lfsr2Panel.IsEnabled = true;
            lfsr3Panel.IsEnabled = false;
        }

        private void shrinkingChecked(object sender, RoutedEventArgs e)
        {
            // enable needed registers
            lfsr1Panel.IsEnabled = true;
            lfsr2Panel.IsEnabled = true;
            lfsr3Panel.IsEnabled = true;
        }
    }
}
