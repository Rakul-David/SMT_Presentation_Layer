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
        String CreditorOrDebtor;
        List<String> AllNames;
        List<Creditor> CreditorList;
        List<Deptor> DebtorList;
        SMT_DataRepository repository;
        public Creditor_Debtor()
        {
            InitializeComponent();
            CreditorList = new List<Creditor>();
            DebtorList = new List<Deptor>();
            repository = new SMT_DataRepository();
            CreditorOrDebtor = "";
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MainPage mainPage = new MainPage();
                this.NavigationService.Navigate(mainPage);
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex);
            }
        }

        public void staticDetails(String type)
        {
            try
            {
                this.CreditorOrDebtor = type;
                this.Title_lbl.Content = type + " DETAILS";
                this.Add_radio.Content = "ADD " + type;
                this.Edit_radio.Content = "EDIT " + type;
                this.Delete_radio.Content = "DELETE " + type;
                if (type == "CREDITOR")
                {
                    this.CreditorList = repository.GetAllCreditor();
                }
                else if (type == "DEBTOR")
                {
                    this.DebtorList = repository.GetAllDeptor();
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex);
            }
        }

        private void Add_radio_Checked(object sender, RoutedEventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                ErrorLog.Log(ex);
            }
        }

        private void Edit_radio_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Update_Logic_btn.Visibility = Visibility.Visible;
                this.Delete_Logic_btn.Visibility = Visibility.Hidden;
                this.EditDeleteCommon("Edit");
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex);
            }
        }

        private void Delete_radio_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Update_Logic_btn.Visibility = Visibility.Hidden;
                this.Delete_Logic_btn.Visibility = Visibility.Visible;
                this.EditDeleteCommon("Delete");
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex);
            }
        }

        private void EditDeleteCommon(String radioChecked)
        {
            try
            {
                if (this.CreditorOrDebtor == "CREDITOR")
                {
                    if (this.CreditorList == null || this.CreditorList.Count() == 0)
                    {
                        this.nullList(radioChecked);
                        return;
                    }
                    else
                    {
                        this.AllNames = this.CreditorList.Select(x => x.name).ToList();
                    }
                }
                else if (this.CreditorOrDebtor == "DEBTOR")
                {
                    if (this.DebtorList == null || this.DebtorList.Count() == 0)
                    {
                        this.nullList(radioChecked);
                        return;
                    }
                    else
                    {
                        this.AllNames = this.DebtorList.Select(x => x.name).ToList();
                    }
                }
                this.Dropdown_Cmbx.ItemsSource = this.AllNames;
                this.Add_Logic_btn.Visibility = Visibility.Hidden;
                this.Dropdown_Cmbx.Visibility = Visibility.Visible;
                this.Dropdown_Cmbx.Text = "";
                this.Update_Logic_btn.IsEnabled = false;
                this.Dropdown_Cmbx.IsEnabled = true;
                this.FullName_txt.IsEnabled = false;
                this.Address_txt.IsEnabled = false;
                this.MobileNo_txt.IsEnabled = false;
                this.AlternateNo_txt.IsEnabled = false;
                this.Balance_txt.IsEnabled = false;
                this.ComboBox_lbl.Visibility = Visibility.Visible;
                this.ComboBox_lbl.Content = "Select the " + CreditorOrDebtor + " to " + radioChecked;
                clearAll();
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex);
            }
        }

        private void nullList(String radioChecked)
        {
            if (MessageBox.Show("No " + this.CreditorOrDebtor + " to " + radioChecked, "Error!", MessageBoxButton.OK, MessageBoxImage.Error) == MessageBoxResult.OK)
            {
                this.Edit_radio.IsChecked = false;
                this.Delete_radio.IsChecked = false;
                this.Update_Logic_btn.Visibility = Visibility.Hidden;
                this.Delete_Logic_btn.Visibility = Visibility.Hidden;
                this.Add_Logic_btn.Visibility = Visibility.Hidden;
                this.FullName_txt.IsEnabled = false;
                this.Address_txt.IsEnabled = false;
                this.MobileNo_txt.IsEnabled = false;
                this.AlternateNo_txt.IsEnabled = false;
                this.Balance_txt.IsEnabled = false;
                this.Dropdown_Cmbx.IsEnabled = false;
                this.Dropdown_Cmbx.Visibility = Visibility.Hidden;
                this.ComboBox_lbl.Visibility = Visibility.Hidden;
            }
        }

        private void clearAll()
        {
            try
            {
                this.Dropdown_Cmbx.SelectedIndex = -1;
                this.FullName_txt.Clear();
                this.Address_txt.Clear();
                this.MobileNo_txt.Clear();
                this.AlternateNo_txt.Clear();
                this.Balance_txt.Clear();
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex);
            }
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

        private void Add_Logic_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool isValid = FieldValidations();
                if (isValid == true)
                {
                    if (this.CreditorOrDebtor == "CREDITOR")
                    {
                        Creditor CreditorObj = new Creditor();
                        CreditorObj.name = FullName_txt.Text;
                        CreditorObj.address = Address_txt.Text;
                        CreditorObj.phone = MobileNo_txt.Text;
                        CreditorObj.alternate = string.IsNullOrEmpty(AlternateNo_txt.Text) ? String.Empty : AlternateNo_txt.Text;
                        CreditorObj.standingBalance = Double.Parse(Balance_txt.Text);
                        if (repository.AddCreditor(CreditorObj))
                        {
                            this.CreditorList = repository.GetAllCreditor();
                            this.Add_Logic_btn.IsEnabled = false;
                            MessageBox.Show("Submitted!", "Add suceeded", MessageBoxButton.OK, MessageBoxImage.Information);
                            clearAll();
                        }
                    }
                    else if (this.CreditorOrDebtor == "DEBTOR")
                    {
                        Deptor DebtorObj = new Deptor();
                        DebtorObj.name = FullName_txt.Text;
                        DebtorObj.address = Address_txt.Text;
                        DebtorObj.phone = MobileNo_txt.Text;
                        DebtorObj.alternate = string.IsNullOrEmpty(AlternateNo_txt.Text) ? String.Empty : AlternateNo_txt.Text;
                        DebtorObj.standingBalance = Double.Parse(Balance_txt.Text);
                        if (repository.AddDeptor(DebtorObj))
                        {
                            this.DebtorList = repository.GetAllDeptor();
                            this.Add_Logic_btn.IsEnabled = false;
                            MessageBox.Show("Submitted!", "Add suceeded", MessageBoxButton.OK, MessageBoxImage.Information);
                            clearAll();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex);
            }
        }

        private void Update_Logic_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool isValid = FieldValidations();
                if (isValid == true)
                {
                    if (this.CreditorOrDebtor == "CREDITOR")
                    {
                        Creditor CreditorObj = new Creditor();
                        Creditor Obj = new Creditor();
                        Obj = (Creditor)this.CreditorList.FirstOrDefault(x => x.name == this.Dropdown_Cmbx.SelectedItem.ToString());
                        CreditorObj.id = Obj.id;
                        CreditorObj.name = FullName_txt.Text;
                        CreditorObj.address = Address_txt.Text;
                        CreditorObj.phone = MobileNo_txt.Text;
                        CreditorObj.alternate = string.IsNullOrEmpty(AlternateNo_txt.Text) ? String.Empty : AlternateNo_txt.Text;
                        CreditorObj.standingBalance = Double.Parse(Balance_txt.Text);
                        if (repository.EditCreditor(CreditorObj))
                        {
                            if (MessageBox.Show("Submitted!", "Update suceeded", MessageBoxButton.OK, MessageBoxImage.Information) == MessageBoxResult.OK)
                            {
                                this.CreditorList = repository.GetAllCreditor();
                                this.EditDeleteCommon("Edit");
                                clearAll();
                            }
                        }
                    }
                    else if (this.CreditorOrDebtor == "DEBTOR")
                    {
                        Deptor DebtorObj = new Deptor();
                        DebtorObj.name = FullName_txt.Text;
                        DebtorObj.address = Address_txt.Text;
                        DebtorObj.phone = MobileNo_txt.Text;
                        DebtorObj.alternate = string.IsNullOrEmpty(AlternateNo_txt.Text) ? String.Empty : AlternateNo_txt.Text;
                        DebtorObj.standingBalance = Double.Parse(Balance_txt.Text);
                        if (repository.EditDeptor(DebtorObj))
                        {
                            if (MessageBox.Show("Submitted!", "Update suceeded", MessageBoxButton.OK, MessageBoxImage.Information) == MessageBoxResult.OK)
                            {
                                this.DebtorList = repository.GetAllDeptor();
                                this.EditDeleteCommon("Edit");
                                clearAll();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex);
            }
        }

        private void Delete_Logic_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.CreditorOrDebtor == "CREDITOR")
                {
                    Creditor deleteCred = new Creditor();
                    deleteCred = (Creditor)this.CreditorList.FirstOrDefault(x => x.name == this.FullName_txt.Text);
                    if (repository.DeleteCreditor(deleteCred))
                    {
                        if (MessageBox.Show("Data for " + FullName_txt.Text + " client is deleted! ", "Deletion", MessageBoxButton.OK, MessageBoxImage.Information) == MessageBoxResult.OK)
                        {
                            this.CreditorList = repository.GetAllCreditor();
                            this.EditDeleteCommon("Delete");
                            clearAll();
                        }
                    }
                }
                else if (this.CreditorOrDebtor == "DEBTOR")
                {
                    Deptor deleteDept = new Deptor();
                    deleteDept = (Deptor)this.DebtorList.FirstOrDefault(x => x.name == this.FullName_txt.Text);
                    if (repository.DeleteDeptor(deleteDept))
                    {
                        if (MessageBox.Show("Data for " + FullName_txt.Text + " client is deleted! ", "Deletion", MessageBoxButton.OK, MessageBoxImage.Information) == MessageBoxResult.OK)
                        {
                            this.DebtorList = repository.GetAllDeptor();
                            this.EditDeleteCommon("Delete");
                            clearAll();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex);
            }
        }

        private bool FieldValidations()
        {
            try
            {
                string name = this.FullName_txt.Text;
                string address = this.Address_txt.Text.Replace(" ", "").Replace("\r\n", "");
                string mob = this.MobileNo_txt.Text;
                string altno = this.AlternateNo_txt.Text;
                string bal = this.Balance_txt.Text;
                bool isValid = false;
                List<String> duplicateName = this.CreditorList.Where(x => x.name == this.FullName_txt.Text).Select(x=>x.name).ToList();
                if (duplicateName != null && duplicateName.Count() > 0)
                {
                    MessageBox.Show("Enter a valid name!", "Name " + FullName_txt.Text + " already exists", MessageBoxButton.OK, MessageBoxImage.Information);
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
                else if (altno != "" && altno.Length < 10 && altno.Length > 0)
                {
                    MessageBox.Show("Enter Valid Alternate mobile numbers", "Validation Failed", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else if (bal == "")
                {
                    MessageBox.Show("Enter a valid balance!", "Validation Failed", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else if (name != "" && address != "" && mob != "" && bal != "" && mob.Length == 10 && (altno.Length == 10 || altno.Length == 0))
                {
                    isValid = true;
                }
                return isValid;
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex);
                return false;
            }
        }

        private void Dropdown_Cmbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (this.Dropdown_Cmbx.SelectedIndex != -1)
                {
                    this.Dropdown_Cmbx.IsDropDownOpen = true;
                    if (this.CreditorOrDebtor == "CREDITOR")
                    {
                        Creditor creditorList = new Creditor();
                        creditorList = (Creditor)this.CreditorList.FirstOrDefault(x => x.name == this.Dropdown_Cmbx.SelectedItem.ToString());
                        if (creditorList != null)
                        {
                            this.FullName_txt.Text = creditorList.name;
                            this.Address_txt.Text = creditorList.address;
                            this.MobileNo_txt.Text = creditorList.phone;
                            this.AlternateNo_txt.Text = creditorList.alternate;
                            this.Balance_txt.Text = creditorList.standingBalance.ToString();
                        }
                    }
                    else if (this.CreditorOrDebtor == "DEBTOR")
                    {
                        Deptor debtorList = (Deptor)this.DebtorList.Select(x => x.name == this.Dropdown_Cmbx.Text);
                        if (debtorList != null)
                        {
                            this.FullName_txt.Text = debtorList.name;
                            this.Address_txt.Text = debtorList.address;
                            this.MobileNo_txt.Text = debtorList.phone;
                            this.AlternateNo_txt.Text = debtorList.alternate;
                            this.Balance_txt.Text = debtorList.standingBalance.ToString();
                        }
                    }
                    if (Edit_radio.IsChecked == true)
                    {
                        this.FullName_txt.IsEnabled = true;
                        this.Address_txt.IsEnabled = true;
                        this.MobileNo_txt.IsEnabled = true;
                        this.AlternateNo_txt.IsEnabled = true;
                        this.Balance_txt.IsEnabled = true;
                        this.Update_Logic_btn.IsEnabled = true;
                    }
                    if (Delete_radio.IsChecked == true)
                    {
                        this.Delete_Logic_btn.IsEnabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex);
            }
        }

        private void EnableButton(object sender, KeyEventArgs e)
        {
            try
            {
                if (FullName_txt.Text != "" && Address_txt.Text != "" && MobileNo_txt.Text != "" && Balance_txt.Text != "")
                {
                    if (this.Add_radio.IsChecked == true)
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
            catch (Exception ex)
            {
                ErrorLog.Log(ex);
            }
        }
    }
}
