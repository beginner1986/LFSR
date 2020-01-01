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

        private void Button1Click(object sender, RoutedEventArgs e)
        {
            function1.Text = GenerateFunction(int.Parse(lenth1.Text));
        }

        private void Button2Click(object sender, RoutedEventArgs e)
        {
            function2.Text = GenerateFunction(int.Parse(lenth2.Text));
        }
        
        private void Button3Click(object sender, RoutedEventArgs e)
        {
            function3.Text = GenerateFunction(int.Parse(lenth3.Text));
        }

        private string GenerateFunction(int length)
        {
            Random random = new Random();
            string result = "";

            for (int i = 0; i < length; i++)
                result += random.Next(2).ToString();

            return result;
        }

    }
}
