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
using SMT_DataLayer;

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
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex);
            }
        }

        private void Creditor_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.NavigationService.Navigate(creditor_deptor);
                this.creditor_deptor.staticDetails("CREDITOR");
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex);
            }
        }

        private void Debtor_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.NavigationService.Navigate(creditor_deptor);
                this.creditor_deptor.staticDetails("DEBTOR");
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex);
            }
        }

        private void PayIn_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
            //    this.NavigationService.Navigate(payIn_payOut);
            //    this.payIn_payOut.staticDetails("IN");
            //}
            //catch (Exception ex)
            //{
            //    ErrorLog.Log(ex);
            //}
        }

        private void PayOut_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
            //    this.NavigationService.Navigate(payIn_payOut);
            //    this.payIn_payOut.staticDetails("OUT");
            //}
            //catch (Exception ex)
            //{
            //    ErrorLog.Log(ex);
            //}
        }

        private void Creditor_Invoice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.NavigationService.Navigate(invoice);
                this.invoice.staticDetails("CREDITOR");
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex);
            }
        }
        private void Deptor_Invoice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.NavigationService.Navigate(invoice);
                this.invoice.staticDetails("DEBTOR");
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex);
            }
        }
    }
}
