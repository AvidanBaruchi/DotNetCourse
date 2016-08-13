using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
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

namespace PrimesCalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Regex _number = new Regex("^[0-9]+$");
        private SynchronizationContext _synchronizationContext = SynchronizationContext.Current;
        private readonly Helper _helper = new Helper();
        private CancellationTokenSource _cancellationSource = null;
        private int _primesCount = 0;

        public MainWindow()
        {
            InitializeComponent();
            textBoxFrom.Text = "1";
            textBoxTo.Text = "5000000";
        }

        private async void buttonCalc_Click(object sender, RoutedEventArgs e)
        {
            buttonCalc.IsEnabled = false;
            labelFileMessage.Content = null;
            await CalculatePrimes();
            WriteToFile();
            buttonCalc.IsEnabled = true;
        }

        private async Task CalculatePrimes()
        {
            int from = 0;
            int to = 0;
            bool isParsed = int.TryParse(textBoxFrom.Text, out from)
                && int.TryParse(textBoxTo.Text, out to);

            if (!isParsed)
            {
                MessageBox.Show("Please insert a valid numbers!");
                return;
            }

            _cancellationSource = new CancellationTokenSource();
            var token = _cancellationSource.Token;

            _primesCount = await _helper.CountPrimesAsync(from, to, token.WaitHandle);           
            labelPrimesCount.Content = "Number of Primes: " + _primesCount;
        }

        private void WriteToFile()
        {
            var fileName = textBoxOutputFile.Text;
            var isWritten = false;

            if (string.IsNullOrEmpty(fileName))
            {
                fileName = "data.txt";
            }
            else
            {
                fileName = fileName + ".txt";
            }

            try
            {
                using (var writer = File.AppendText(fileName))
                {
                    writer.WriteLine(
                        $"{DateTime.Now}: Found {_primesCount} Prime Numbers in the Range of {textBoxFrom.Text} to {textBoxTo.Text}");
                    labelFileMessage.Content = $"{_primesCount} Written to file {fileName}";
                    isWritten = true;
                }
            }
            catch (UnauthorizedAccessException e)
            {
                Debug.WriteLine($"Writing to file threw UnauthorizedAccessException: {e.Message}");
                labelFileMessage.Content = "Could not write to file!";
            }
            catch (PathTooLongException e)
            {
                Debug.WriteLine($"Writing to file threw PathTooLongException: {e.Message}");
                labelFileMessage.Content = "Could not write to file!";
            }
            finally
            {
                if (!isWritten)
                {
                    labelFileMessage.Content = "Could not write to file!"; 
                }
            }
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            if (_cancellationSource != null && !_cancellationSource.IsCancellationRequested)
            {
                _cancellationSource.Cancel();
                buttonCalc.IsEnabled = true;
            }
        }

        private void ValidateNumber(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !_number.IsMatch(e.Text);
        }

        private void textBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }
    }
}
