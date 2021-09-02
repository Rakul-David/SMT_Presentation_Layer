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
using System.Text.RegularExpressions;
using SMT_DataLayer;
using SMT_DataLayer.Repository;

namespace SMT_UI.Pages
{
    /// <summary>
    /// Interaction logic for Invoice.xaml
    /// </summary>
    public partial class Invoice : Page
    {
        public Invoice()
        {
            InitializeComponent();
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            MainPage mainPage = new MainPage();
            this.NavigationService.Navigate(mainPage);
        }

        private void NumberValidation(object sender, TextCompositionEventArgs e)
        {
            try
            {
                Regex regex = new Regex("[^0-9]+");
                e.Handled = regex.IsMatch(e.Text);
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex);
            }
        }
        private void No_Spaces(object sender, KeyEventArgs e)
        {
            try
            {
                e.Handled = e.Key == Key.Space;
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex);
            }
        }


    }
}
