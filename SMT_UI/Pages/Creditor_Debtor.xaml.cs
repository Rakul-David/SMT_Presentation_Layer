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
    /// Interaction logic for Creditor_Debtor.xaml
    /// </summary>
    public partial class Creditor_Debtor : Page
    {
        String CreditorOrDebtor = "";
        public Creditor_Debtor()
        {
            InitializeComponent();
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            MainPage mainPage = new MainPage();
            this.NavigationService.Navigate(mainPage);
        }

        public void staticDetails(String type) {
            this.CreditorOrDebtor = type;
            this.Title_lbl.Content = type + " DETAILS";
            this.Add_radio.Content = "ADD " + type;
            this.Edit_radio.Content = "EDIT " + type;
            this.Delete_radio.Content = "DELETE " + type;
        }

        private void Add_radio_Checked(object sender, RoutedEventArgs e)
        {
            this.Add_Logic_btn.Visibility = Visibility.Visible;
            this.Update_Logic_btn.Visibility = Visibility.Hidden;
            this.Delete_Logic_btn.Visibility = Visibility.Hidden;
            this.Add_Logic_btn.Content = "ADD";
            this.Dropdown_Cmbx.IsEnabled = false;
            this.FullName_txt.IsEnabled = true;
            this.Address_txt.IsEnabled = true;
            this.MobileNo_txt.IsEnabled = true;
            this.AlternateNo_txt.IsEnabled = true;
            this.Balance_txt.IsEnabled = true;
            this.ComboBox_lbl.Content = "";
            this.Dropdown_Cmbx.SelectedIndex = -1;
        }

        private void Edit_radio_Checked(object sender, RoutedEventArgs e)
        {
            this.Add_Logic_btn.Visibility = Visibility.Hidden;
            this.Update_Logic_btn.Visibility = Visibility.Visible;
            this.Delete_Logic_btn.Visibility = Visibility.Hidden;
            this.Update_Logic_btn.Content = "UPDATE";
            this.Dropdown_Cmbx.IsEnabled = true;
            this.FullName_txt.IsEnabled = false;
            this.Address_txt.IsEnabled = false;
            this.MobileNo_txt.IsEnabled = false;
            this.AlternateNo_txt.IsEnabled = false;
            this.Balance_txt.IsEnabled = false;
            this.ComboBox_lbl.Content = "Select the " + CreditorOrDebtor + " to Edit";

        }

        private void Delete_radio_Checked(object sender, RoutedEventArgs e)
        {
            this.Add_Logic_btn.Visibility = Visibility.Hidden;
            this.Update_Logic_btn.Visibility = Visibility.Hidden;
            this.Delete_Logic_btn.Visibility = Visibility.Visible;
            this.Delete_Logic_btn.Content = "DELETE";
            this.Dropdown_Cmbx.IsEnabled = true;
            this.FullName_txt.IsEnabled = false;
            this.Address_txt.IsEnabled = false;
            this.MobileNo_txt.IsEnabled = false;
            this.AlternateNo_txt.IsEnabled = false;
            this.Balance_txt.IsEnabled = false;
            this.ComboBox_lbl.Content = "Select the " + CreditorOrDebtor + " to Delete";
        }
    }
}
