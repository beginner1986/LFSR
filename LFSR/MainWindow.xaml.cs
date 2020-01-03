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

        private void GenerateButtonClick(object sender, RoutedEventArgs e)
        {
            // check if is cpihertextLength empty
            if (cipherLenth.Text == "")
            {
                resultTextBox.Text = "Podaj długość szyfru w bitach.";
                return;
            }

            // check if given ciphertextLength is int correct value
            bool correctLength = int.TryParse(cipherLenth.Text, out int length);
            if (!correctLength)
            {
                resultTextBox.Text = "Podana długość szyfru musi być liczbą całkowitą.";
                return;
            }

            // run correct generator
            // update result TextBox and registers RextBoxes
            // generate sample stream for tests
            string sample = RunGenerators(length);

            // perform the tests and update results in GUI
            PerformTests(sample);
        }

        private string GeffeGenerator(int len)
        {
            string result = "";

            for(int i=0; i<len; i++)
            {
                bool reg1 = lfsr1.Shift();
                bool reg2 = lfsr2.Shift();
                bool reg3 = lfsr3.Shift();

                bool bit = (reg1 & reg2) ^(!reg2 & reg3);

                result += bit ? "1" : "0";
            }

            return result;
        }

        private string StopAndGoGenerator(int len)
        {
            string result = "";

            bool reg1;
            bool reg2 = false;
            bool reg3 = false;
            bool bit;

            while(result.Length < len)
            {
                reg1 = lfsr1.Shift();

                if (reg1)
                    lfsr2.Shift();
                else
                    reg3 = lfsr3.Shift();

                bit = reg2 ^ reg3;

                result += bit ? "1" : "0";
            }

            return result;
        }

        private string ShrinkingGenerator(int len)
        {
            string result = "";

            while (result.Length < len)
            {
                bool reg1 = lfsr1.Shift();
                bool reg2 = lfsr2.Shift();
                bool bit;

                if (reg1)
                {
                    bit = reg2;
                    result += bit ? "1" : "0";
                }
            }

            return result;
        }

        private bool MonobitTest(string sample)
        {
            int onesCount = 0;

            foreach(char c in sample)
            {
                if (c == '1')
                    onesCount++;
            }

            if (onesCount < 9725 || onesCount > 10275)
                return false;
            else
                return true;
        }

        private bool PokerTest(string sample)
        {
            // TODO
            return false;
        }

        private bool LongRunsTest(string sample)
        {
            // TODO
            return false;
        }

        private string RunGenerators(int length)
        {
            string sample = "";
            int sampleLenght = 20000;

            if (geffe.IsChecked == true)
            {
                resultTextBox.Text = GeffeGenerator(length);
                sample = GeffeGenerator(sampleLenght);
            }
            else if (stopAndGo.IsChecked == true)
            {
                resultTextBox.Text = StopAndGoGenerator(length);
                sample = StopAndGoGenerator(sampleLenght);
            }
            else if (shrinking.IsChecked == true)
            {
                resultTextBox.Text = ShrinkingGenerator(length);
                sample = ShrinkingGenerator(sampleLenght);
            }
            else
            {
                resultTextBox.Text = "Zaznacz typ generatora, który chcesżyć";
            }

            // update registers values in TextBoxes
            register1.Text = lfsr1.ToString();
            register2.Text = lfsr2.ToString();
            register3.Text = lfsr3.ToString();

            return sample;
        }

        private void PerformTests(string sample)
        {
            if (MonobitTest(sample))
                monobitTest.Content = "OK";
            else
                monobitTest.Content = "FAIL";

            if (PokerTest(sample))
                pokerTest.Content = "OK";
            else
                pokerTest.Content = "FAIL";

            if (LongRunsTest(sample))
                longRunsTest.Content = "OK";
            else
                longRunsTest.Content = "FAIL";
        }
    }
}
