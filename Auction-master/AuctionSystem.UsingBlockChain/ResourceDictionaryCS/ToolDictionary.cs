using System.Windows;
using System.Windows.Input;
using System.Text.RegularExpressions;

namespace AuctionSystem.UsingBlockChain.ResourceDictionaryCS
{
    partial class ToolDictionary : ResourceDictionary
    {
        public ToolDictionary()
        {
            InitializeComponent();
        }
        private void PolicyID_ValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex(@"^[0-9A-Za-z_-]+$");
            e.Handled = !regex.IsMatch(e.Text);
        }
        private void HEX_ValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex(@"\A\b[0-9a-fA-F]+\b\Z");
            e.Handled = !regex.IsMatch(e.Text);
        }
        private void DateType_ValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex(@"^[0-9/]+$");
            e.Handled = !regex.IsMatch(e.Text);
        }
        private void IP_ValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex(@"^[0-9.-_]+$");
            e.Handled = !regex.IsMatch(e.Text);
        }
        private void Number_ValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex(@"^[0-9.]+$");
            e.Handled = !regex.IsMatch(e.Text);
        }
        private void SpaceCut(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void HandleCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (e.Command == ApplicationCommands.Cut ||
                e.Command == ApplicationCommands.Copy ||
                e.Command == ApplicationCommands.Paste)
            {
                e.CanExecute = false;
                e.Handled = true;
            }
        }
    }
}
