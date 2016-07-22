using System;
using System.Collections.Generic;
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
        Helper _helper = new Helper();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            listBox.Items.Clear();
            CalculatePrimes();
        }

        private void CalculatePrimes()
        {
            int from = int.Parse(textBoxFrom.Text);
            int to = int.Parse(textBoxTo.Text);

            Task.Run(() =>
            {
                var primes = _helper.CalcPrimes(from, to);

                invokeOnUiThread(() =>
                {
                    foreach (var prime in primes)
                    {
                        listBox.Items.Add(prime); 
                    }
                });
            });
        }

        private void invokeOnUiThread(Action action)
        {
            _synchronizationContext.Send(o =>
            {
                if (action != null)
                {
                    action.Invoke(); 
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
