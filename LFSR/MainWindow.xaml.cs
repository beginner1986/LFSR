using System;
using System.Windows;

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
            lfsr3Panel.IsEnabled = true;
        }

        private void ShrinkingChecked(object sender, RoutedEventArgs e)
        {
            // enable needed registers
            lfsr1Panel.IsEnabled = true;
            lfsr2Panel.IsEnabled = true;
            lfsr3Panel.IsEnabled = false;
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

        private void FreeBit1DropDownClosed(object sender, EventArgs e)
        {
            bool value = (int.Parse(freeBit1.Text) != 0 ? true : false);
            lfsr1.FreeBit = value;
        }

        private void FreeBit2DropDownClosed(object sender, EventArgs e)
        {
            bool value = (int.Parse(freeBit2.Text) != 0 ? true : false);
            lfsr2.FreeBit = value;
        }
        private void FreeBit3DropDownClosed(object sender, EventArgs e)
        {
            bool value = (int.Parse(freeBit3.Text) != 0 ? true : false);
            lfsr3.FreeBit = value;
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
            if(sample != "")
                PerformTests(sample);
        }

        private string RunGenerators(int length)
        {
            Generators generators = new Generators(lfsr1, lfsr2, lfsr3);

            // make sample for testiong
            string sample = "";
            int sampleLenght = 20000;

            // run the Geffe generator
            if (geffe.IsChecked == true)
            {
                resultTextBox.Text = generators.GeffeGenerator(length);
                sample = generators.GeffeGenerator(sampleLenght);
            }
            // run the Stop-And-Go generator
            else if (stopAndGo.IsChecked == true)
            {
                resultTextBox.Text = generators.StopAndGoGenerator(length);
                sample = generators.StopAndGoGenerator(sampleLenght);
            }
            // run the Shrinking generator
            else if (shrinking.IsChecked == true)
            {
                resultTextBox.Text = generators.ShrinkingGenerator(length);
                sample = generators.ShrinkingGenerator(sampleLenght);
            }
            // no generator type selected
            else
            {
                resultTextBox.Text = "Zaznacz typ generatora, który chces użyć";
            }

            // update registers values in TextBoxes
            register1.Text = lfsr1.ToString();
            register2.Text = lfsr2.ToString();
            register3.Text = lfsr3.ToString();

            return sample;
        }

        private void PerformTests(string sample)
        {
            // class containing test methods
            Tests tests = new Tests();

            if (tests.MonobitTest(sample))
            {
                monobitOkIcon.Visibility = Visibility.Visible;
                monobitFailIcon.Visibility = Visibility.Hidden;
            }
            else
            {
                monobitOkIcon.Visibility = Visibility.Hidden;
                monobitFailIcon.Visibility = Visibility.Visible;
            }

            if (tests.PokerTest(sample))
            {
                pokerOkIcon.Visibility = Visibility.Visible;
                pokerFailIcon.Visibility = Visibility.Hidden;
            }
            else
            {
                pokerOkIcon.Visibility = Visibility.Hidden;
                pokerFailIcon.Visibility = Visibility.Visible;
            }

            if (tests.LongRunsTest(sample))
            {
                longRunsOkIcon.Visibility = Visibility.Visible;
                longRunsFailIcon.Visibility = Visibility.Hidden;
            }
            else
            {
                longRunsOkIcon.Visibility = Visibility.Hidden;
                longRunsFailIcon.Visibility = Visibility.Visible;
            }
        }
    }
}
