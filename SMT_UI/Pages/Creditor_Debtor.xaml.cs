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
    /// Interaction logic for Creditor_Debtor.xaml
    /// </summary>
    public partial class Creditor_Debtor : Page
    {
        String CreditorOrDebtor = "";

        List<String> CreditorNames;
        String[] DebtorNames;
        List<Creditor> CreditorList;
        List<Deptor> DebtorList;
        SMT_DataRepository repository;
        public Creditor_Debtor()
        {
            InitializeComponent();
            CreditorList = new List<Creditor>();
            DebtorList = new List<Deptor>();
            repository = new SMT_DataRepository();
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            MainPage mainPage = new MainPage();
            this.NavigationService.Navigate(mainPage);
        }

        public void staticDetails(String type)
        {
            this.CreditorOrDebtor = type;
            this.Title_lbl.Content = type + " DETAILS";
            this.Add_radio.Content = "ADD " + type;
            this.Edit_radio.Content = "EDIT " + type;
            this.Delete_radio.Content = "DELETE " + type;
            this.CreditorList = repository.GetAllCreditor();
            this.DebtorList = repository.GetAllDeptor();
            this.CreditorNames = this.CreditorList.Select(x => x.name).ToList();
            this.Dropdown_Cmbx.ItemsSource = this.CreditorNames; //new List<string> { "Item1", "Item2", "Item3" };

        }

        private void Add_radio_Checked(object sender, RoutedEventArgs e)
        {
            this.Add_Logic_btn.Visibility = Visibility.Visible;
            this.Update_Logic_btn.Visibility = Visibility.Hidden;
            this.Delete_Logic_btn.Visibility = Visibility.Hidden;
            this.Dropdown_Cmbx.Visibility = Visibility.Hidden;
            this.Add_Logic_btn.IsEnabled = false;
            this.Dropdown_Cmbx.IsEnabled = false;
            this.FullName_txt.IsEnabled = true;
            this.Address_txt.IsEnabled = true;
            this.MobileNo_txt.IsEnabled = true;
            this.AlternateNo_txt.IsEnabled = true;
            this.Balance_txt.IsEnabled = true;
            this.ComboBox_lbl.Content = "";
            this.Dropdown_Cmbx.SelectedIndex = -1;
            clearAll();
        }

        private void Edit_radio_Checked(object sender, RoutedEventArgs e)
        {

            this.Add_Logic_btn.Visibility = Visibility.Hidden;
            // this.Dropdown_Cmbx.IsEditable = false;
            this.Update_Logic_btn.Visibility = Visibility.Visible;
            this.Delete_Logic_btn.Visibility = Visibility.Hidden;
            this.Dropdown_Cmbx.Visibility = Visibility.Visible;
            this.Update_Logic_btn.IsEnabled = false;
            this.Dropdown_Cmbx.IsEnabled = true;
            this.FullName_txt.IsEnabled = false;
            this.Address_txt.IsEnabled = false;
            this.MobileNo_txt.IsEnabled = false;
            this.AlternateNo_txt.IsEnabled = false;
            this.Balance_txt.IsEnabled = false;
            this.ComboBox_lbl.Content = "Select the " + CreditorOrDebtor + " to Edit";
            clearAll();

        }

        private void Delete_radio_Checked(object sender, RoutedEventArgs e)
        {
            this.Add_Logic_btn.Visibility = Visibility.Hidden;
            //this.Dropdown_Cmbx.IsEditable = false;
            this.Update_Logic_btn.Visibility = Visibility.Hidden;
            this.Delete_Logic_btn.Visibility = Visibility.Visible;
            this.Dropdown_Cmbx.Visibility = Visibility.Visible;
            this.Delete_Logic_btn.IsEnabled = false;
            this.Dropdown_Cmbx.IsEnabled = true;
            this.FullName_txt.IsEnabled = false;
            this.Address_txt.IsEnabled = false;
            this.MobileNo_txt.IsEnabled = false;
            this.AlternateNo_txt.IsEnabled = false;
            this.Balance_txt.IsEnabled = false;
            this.ComboBox_lbl.Content = "Select the " + CreditorOrDebtor + " to Delete";
            clearAll();
        }

        public void clearAll()
        {
            this.Dropdown_Cmbx.SelectedIndex = -1;
            this.FullName_txt.Clear();
            this.Address_txt.Clear();
            this.MobileNo_txt.Clear();
            this.AlternateNo_txt.Clear();
            this.Balance_txt.Clear();
        }

        private void NumberValidation(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void No_Spaces(object sender, KeyEventArgs e)
        {
            e.Handled = e.Key == Key.Space;
        }

        public String Textbox_Text()
        {
            string name = FullName_txt.Text;
            string address = Address_txt.Text;
            string mob = MobileNo_txt.Text;
            string altno = AlternateNo_txt.Text;
            string bal = Balance_txt.Text;
            String temp = "";
            if (name != "" && address != "" && mob != "" && bal != "" && mob.Length == 10 && (altno.Length == 10 || altno.Length == 0))
            {
                temp = "yes";
            }
            else if (name.Length < 2)
            {
                MessageBox.Show("Enter a valid name!", "Validation Failed", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (address.Length < 2)
            {
                MessageBox.Show("Enter a valid address!", "Validation Failed", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (mob.Length != 10)
            {
                MessageBox.Show("Enter Valid mobile numbers", "Validation Failed", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (altno == "" || altno.Length < 10)
            {
                MessageBox.Show("Enter Valid Alternate mobile numbers", "Validation Failed", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (bal == "")
            {
                MessageBox.Show("Enter a valid balance!", "Validation Failed", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            return temp;
        }

        private void Add_Logic_btn_Click(object sender, RoutedEventArgs e)
        {
            String Temp = Textbox_Text();
            if (Temp == "yes")
            {
                Creditor TempObj = new Creditor();
                TempObj.name = FullName_txt.Text;
                TempObj.address = Address_txt.Text;
                TempObj.phone = MobileNo_txt.Text;
                TempObj.alternate = (string.IsNullOrEmpty(AlternateNo_txt.Text)) ? String.Empty : AlternateNo_txt.Text;
                TempObj.standingBalance = Double.Parse(Balance_txt.Text);
                if (repository.AddCreditor(TempObj))
                {
                    MessageBox.Show("Submitted!", "Add suceeded", MessageBoxButton.OK, MessageBoxImage.Information);
                    clearAll();
                }
            }

        }

        private void Delete_Logic_btn_Click(object sender, RoutedEventArgs e)
        {
            if (this.Dropdown_Cmbx.SelectedIndex == -1)
            {
                MessageBox.Show("Please select valid client to be deleted! ", "Deletion", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Data for " + FullName_txt.Text + " client is deleted! ", "Deletion", MessageBoxButton.OK, MessageBoxImage.Information);
                clearAll();
            }
        }

        private void Update_Logic_btn_Click(object sender, RoutedEventArgs e)
        {
            if (this.Dropdown_Cmbx.SelectedIndex == -1)
            {
                MessageBox.Show("Please select valid client to be updated! ", "Deletion", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                String Temp = Textbox_Text();
                if (Temp == "yes")
                {
                    MessageBox.Show("Data for " + FullName_txt.Text + " client is updated! ", "Deletion", MessageBoxButton.OK, MessageBoxImage.Information);
                    clearAll();
                }
            }

        }

        private void Dropdown_Cmbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.Dropdown_Cmbx.SelectedIndex == 0)
            {
                FullName_txt.Text = "ASIAN PAINTS";
                Address_txt.Text = "DELHI";
                MobileNo_txt.Text = "8767878798";
                AlternateNo_txt.Text = "8978563512";
                Balance_txt.Text = "55000";
            }

            if (Edit_radio.IsChecked == true && Dropdown_Cmbx.SelectedIndex != -1)
            {
                this.FullName_txt.IsEnabled = true;
                this.Address_txt.IsEnabled = true;
                this.MobileNo_txt.IsEnabled = true;
                this.AlternateNo_txt.IsEnabled = true;
                this.Balance_txt.IsEnabled = true;
                this.Update_Logic_btn.IsEnabled = true;
            }
            if (Delete_radio.IsChecked == true && Dropdown_Cmbx.SelectedIndex != -1)
            {
                this.Delete_Logic_btn.IsEnabled = true;
            }
            //clearAll();
        }
        private void Combokey(object sender, KeyEventArgs e)
        {
            Dropdown_Cmbx.IsDropDownOpen = true;
        }
        public void EnableButton(object sender, KeyEventArgs e)
        {
            if (FullName_txt.Text != "" && Address_txt.Text != "" && MobileNo_txt.Text != "" && Balance_txt.Text != "")
            {                
                if(this.Add_radio.IsChecked == true)
                {
                    this.Add_Logic_btn.IsEnabled = true;
                }
                else if (this.Edit_radio.IsChecked == true)
                {
                    this.Update_Logic_btn.IsEnabled = true;
                }
            }
            else
            {
                this.Add_Logic_btn.IsEnabled = false;
                this.Update_Logic_btn.IsEnabled = false;
            }
        }
    }
}
