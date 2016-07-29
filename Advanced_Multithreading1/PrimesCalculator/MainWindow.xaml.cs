using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
        private Regex number = new Regex("^[0-9]+$");
        private SynchronizationContext _synchronizationContext = SynchronizationContext.Current;
        private Helper _helper = new Helper();
        private CancellationTokenSource _cancellationSource = null;
        private ObservableCollection<int> _primes = new ObservableCollection<int>();

        public MainWindow()
        {
            InitializeComponent();
            textBoxFrom.Text = "1";
            textBoxTo.Text = "5000000";
            listBox.ItemsSource = _primes;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            _primes.Clear();
            CalculatePrimes();
        }

        private void CalculatePrimes()
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

            Task.Run(() =>
            {
                var primes = _helper.CalcPrimesCancelable(from, to, token.WaitHandle);

                _primes = new ObservableCollection<int>(primes);

                InvokeOnUiThread(() =>
                {
                    listBox.ItemsSource = _primes;
                });
            }, token);
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            if (_cancellationSource != null && !_cancellationSource.IsCancellationRequested)
            {
                _cancellationSource.Cancel();
            }
        }

        private void InvokeOnUiThread(Action action)
        {
            _synchronizationContext.Send(o =>
            {
                try
                {
                    if (action != null)
                    {
                        action.Invoke();
                    }
                }
                catch (NotSupportedException e)
                {

                }
            }, null);
        }

        private void ValidateNumber(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !number.IsMatch(e.Text);
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
