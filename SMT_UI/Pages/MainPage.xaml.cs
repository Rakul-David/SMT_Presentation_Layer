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

namespace SMT_UI.Pages
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        Creditor_Debtor creditor_deptor = new Creditor_Debtor();
        PayIn_PayOut payIn_payOut = new PayIn_PayOut();
        Invoice invoice = new Invoice();

        public MainPage()
        {
            InitializeComponent();
        }

        private void Creditor_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(creditor_deptor);
            creditor_deptor.staticDetails("CREDITOR");
        }

        private void Debtor_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(creditor_deptor);
            creditor_deptor.staticDetails("DEBTOR");
        }

        private void PayIn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(payIn_payOut);
        }

        private void PayOut_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(payIn_payOut);
        }

        private void Creditor_Invoice_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(invoice);
            invoice.staticDetails("CREDITOR");
        }
        private void Deptor_Invoice_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(invoice);
        }
    }
}
