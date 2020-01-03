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
        private Lfsr lfsr1 = new Lfsr(15);
        private Lfsr lfsr2 = new Lfsr(15);
        private Lfsr lfsr3 = new Lfsr(15);

        public MainWindow()
        {
            InitializeComponent();
            
            // display functions for registers
            function1.Text = lfsr1.FunctionToString();
            function2.Text = lfsr2.FunctionToString();
            function3.Text = lfsr3.FunctionToString();

            // display registers values
            register1.Text = lfsr1.ToString();
            register2.Text = lfsr2.ToString();
            register3.Text = lfsr3.ToString();
        }

        private void GeffeChecked(object sender, RoutedEventArgs e)
        {
            // enable needed registers
            lfsr1Panel.IsEnabled = true;
            lfsr2Panel.IsEnabled = true;
            lfsr3Panel.IsEnabled = true;
        }

        private void StopAndGoChecked(object sender, RoutedEventArgs e)
        {
            // enable needed registers
            lfsr1Panel.IsEnabled = true;
            lfsr2Panel.IsEnabled = true;
            lfsr3Panel.IsEnabled = false;
        }

        private void ShrinkingChecked(object sender, RoutedEventArgs e)
        {
            // enable needed registers
            lfsr1Panel.IsEnabled = true;
            lfsr2Panel.IsEnabled = true;
            lfsr3Panel.IsEnabled = true;
        }
        
        private void Length1DropDownClosed(object sender, EventArgs e)
        {
            int len = int.Parse(length1.Text);
            lfsr1 = new Lfsr(len);
            register1.Text = lfsr1.ToString();
            function1.Text = lfsr1.FunctionToString();
        }

        private void Length2DropDownClosed(object sender, EventArgs e)
        {
            int len = int.Parse(length2.Text);
            lfsr2 = new Lfsr(len);
            register2.Text = lfsr2.ToString();
            function2.Text = lfsr2.FunctionToString();
        }

        private void Length3DropDownClosed(object sender, EventArgs e)
        {
            int len = int.Parse(length3.Text);
            lfsr3 = new Lfsr(len);
            register3.Text = lfsr3.ToString();
            function3.Text = lfsr3.FunctionToString();
        }

        private void ButtonShift1Click(object sender, RoutedEventArgs e)
        {
            lfsr1.Shift();
            register1.Text = lfsr1.ToString();
        }

        private void ButtonShift2Click(object sender, RoutedEventArgs e)
        {
            lfsr2.Shift();
            register2.Text = lfsr2.ToString();
        }

        private void ButtonShift3Click(object sender, RoutedEventArgs e)
        {
            lfsr3.Shift();
            register3.Text = lfsr3.ToString();
        }
    }
}
