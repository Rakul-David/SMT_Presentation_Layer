using SMT_DataLayer;
using SMT_DataLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SMT_UI.Pages
{
    /// <summary>
    /// Interaction logic for Creditor_Debtor.xaml
    /// </summary>
    public partial class Creditor_Debtor : Page
    {
        String CreditorOrDebtor;
        List<String> AllNames;
        List<CreditorOrDebtor> CreditorOrDebtorList;
        SMT_DataRepository repository;
        public Creditor_Debtor()
        {
            InitializeComponent();
            this.CreditorOrDebtorList = new List<CreditorOrDebtor>();
            this.repository = new SMT_DataRepository();
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
                this.CreditorOrDebtorList = repository.GetAllCreditorOrDebtor(type);
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
                this.Dropdown_txt.Visibility = Visibility.Hidden;
                this.Add_Logic_btn.IsEnabled = false;
                this.FullName_txt.IsEnabled = true;
                this.Address_txt.IsEnabled = true;
                this.MobileNo_txt.IsEnabled = true;
                this.AlternateNo_txt.IsEnabled = true;
                this.Balance_txt.IsEnabled = true;
                this.ComboBox_lbl.Content = "";
                this.Dropdown_Cmbx.SelectedIndex = -1;
                this.clearAll();
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
                if (this.CreditorOrDebtorList == null || this.CreditorOrDebtorList.Count() == 0)
                {
                    this.nullList(radioChecked);
                    return;
                }
                else
                {
                    this.AllNames = this.CreditorOrDebtorList.Select(x => x.name).ToList();
                }
                this.Dropdown_Cmbx.ItemsSource = this.AllNames;
                this.Add_Logic_btn.Visibility = Visibility.Hidden;
                this.Dropdown_Cmbx.Visibility = Visibility.Visible;
                this.Dropdown_txt.Visibility = Visibility.Visible;
                this.Dropdown_Cmbx.Text = "";
                this.Dropdown_txt.Text = "";
                this.Update_Logic_btn.IsEnabled = false;
                this.Dropdown_Cmbx.IsEnabled = true;
                this.Dropdown_txt.IsEnabled = true;
                this.FullName_txt.IsEnabled = false;
                this.Address_txt.IsEnabled = false;
                this.MobileNo_txt.IsEnabled = false;
                this.AlternateNo_txt.IsEnabled = false;
                this.Balance_txt.IsEnabled = false;
                this.ComboBox_lbl.Visibility = Visibility.Visible;
                this.ComboBox_lbl.Content = "Select the " + this.CreditorOrDebtor + " to " + radioChecked;
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
                this.Dropdown_txt.IsEnabled = false;
                this.Dropdown_Cmbx.Visibility = Visibility.Hidden;
                this.Dropdown_txt.Visibility = Visibility.Hidden;
                this.ComboBox_lbl.Visibility = Visibility.Hidden;
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
                if (FieldValidations() == true)
                {
                    CreditorOrDebtor CredOrDeptObj = new CreditorOrDebtor();
                    CredOrDeptObj.UserIdentity = this.CreditorOrDebtor.Equals("CREDITOR") ? Guid.Parse(Enumeration.Atributes[(int)Details.Creditor]) : Guid.Parse(Enumeration.Atributes[(int)Details.Deptor]);
                    CredOrDeptObj.name = this.FullName_txt.Text.ToUpper();
                    CredOrDeptObj.address = this.Address_txt.Text.ToUpper();
                    CredOrDeptObj.phone = this.MobileNo_txt.Text;
                    CredOrDeptObj.alternate = string.IsNullOrEmpty(this.AlternateNo_txt.Text) ? String.Empty : AlternateNo_txt.Text;
                    CredOrDeptObj.standingBalance = Double.Parse(this.Balance_txt.Text);
                    if (this.repository.AddCreditorOrDebtor(CredOrDeptObj))
                    {
                        this.CreditorOrDebtorList = this.repository.GetAllCreditorOrDebtor(this.CreditorOrDebtor);
                        this.AllNames = this.CreditorOrDebtorList.Select(x => x.name).ToList();
                        this.Add_Logic_btn.IsEnabled = false;
                        MessageBox.Show("Added "+this.FullName_txt.Text.ToUpper()+" as "+this.CreditorOrDebtor+ " successfully!", "Submitted", MessageBoxButton.OK, MessageBoxImage.Information);
                        this.clearAll();
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
                bool isValid = this.FieldValidations();
                if (isValid == true)
                {
                    CreditorOrDebtor CredOrDeptObj = new CreditorOrDebtor();
                    CreditorOrDebtor ObjId = new CreditorOrDebtor();
                    ObjId = (CreditorOrDebtor)this.CreditorOrDebtorList.FirstOrDefault(x => x.name == this.Dropdown_Cmbx.SelectedItem.ToString());
                    CredOrDeptObj.id = ObjId.id;
                    CredOrDeptObj.UserIdentity = this.CreditorOrDebtor.Equals("CREDITOR") ? Guid.Parse(Enumeration.Atributes[(int)Details.Creditor]) : Guid.Parse(Enumeration.Atributes[(int)Details.Deptor]);
                    CredOrDeptObj.name = this.FullName_txt.Text.ToUpper();
                    CredOrDeptObj.address = this.Address_txt.Text.ToUpper();
                    CredOrDeptObj.phone = this.MobileNo_txt.Text;
                    CredOrDeptObj.alternate = string.IsNullOrEmpty(this.AlternateNo_txt.Text) ? String.Empty : this.AlternateNo_txt.Text;
                    CredOrDeptObj.standingBalance = Double.Parse(this.Balance_txt.Text);
                    if (repository.EditCreditorOrDebtor(CredOrDeptObj))
                    {
                        if (MessageBox.Show("Updated " + this.CreditorOrDebtor +" details successfully!", "Updated", MessageBoxButton.OK, MessageBoxImage.Information) == MessageBoxResult.OK)
                        {
                            this.CreditorOrDebtorList = this.repository.GetAllCreditorOrDebtor(this.CreditorOrDebtor);
                            this.EditDeleteCommon("Edit");
                            this.clearAll();
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
                Guid credOrDeptId = this.CreditorOrDebtorList.Where(x => x.name == this.FullName_txt.Text).Select(x => x.id).FirstOrDefault();
                if (this.repository.DeleteCreditorOrDebtor(credOrDeptId))
                {
                    if (MessageBox.Show("Deleted " + this.FullName_txt.Text.ToUpper() + " successfully!", "Deleted", MessageBoxButton.OK, MessageBoxImage.Information) == MessageBoxResult.OK)
                    {
                        this.CreditorOrDebtorList = this.repository.GetAllCreditorOrDebtor(this.CreditorOrDebtor);
                        this.EditDeleteCommon("Delete");
                        this.clearAll();
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
                string name = this.FullName_txt.Text.ToUpper();
                string address = this.Address_txt.Text.Replace(" ", "").Replace("\r\n", "");
                string mob = this.MobileNo_txt.Text;
                string altno = this.AlternateNo_txt.Text;
                string bal = this.Balance_txt.Text;
                bool isValid = false;
                if (this.AllNames != null && this.AllNames.Count() > 0 && this.AllNames.Where(x => x == this.FullName_txt.Text.ToUpper()).FirstOrDefault() != null)
                {
                    string duplicateName = this.AllNames.FirstOrDefault(x => x == this.FullName_txt.Text.ToUpper()).ToString();
                    if (this.Add_radio.IsChecked == true && duplicateName != null)
                    {
                        MessageBox.Show("Name " + name + " already exists!", "Validation Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                        return false;
                    }
                    else if (duplicateName != null && duplicateName != this.Dropdown_Cmbx.Text)
                    {
                        MessageBox.Show("Name " + name + " already exists!", "Validation Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                        return false;
                    }
                }
                if (name.Length < 2)
                {
                    MessageBox.Show("Enter a valid Name!", "Validation Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (address.Length < 2)
                {
                    MessageBox.Show("Enter a valid Address!", "Validation Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (mob.Length != 10)
                {
                    MessageBox.Show("Enter valid Mobile Number!", "Validation Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (altno != "" && altno.Length < 10 && altno.Length > 0)
                {
                    MessageBox.Show("Enter valid Alternate Mobile Number!", "Validation Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (bal == "")
                {
                    MessageBox.Show("Enter a valid Standing Balance!", "Validation Failed", MessageBoxButton.OK, MessageBoxImage.Error);
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

        private void clearAll()
        {
            try
            {
                this.Dropdown_txt.Clear();
                this.FullName_txt.Clear();
                this.Address_txt.Clear();
                this.MobileNo_txt.Clear();
                this.AlternateNo_txt.Clear();
                this.Balance_txt.Clear();
                this.Delete_Logic_btn.IsEnabled = false;
                this.Update_Logic_btn.IsEnabled = false;
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex);
            }
        }

        private void Dropdown_Cmbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (this.Dropdown_Cmbx.SelectedIndex != -1)
                {
                    this.Dropdown_txt.Text = this.Dropdown_Cmbx.SelectedItem.ToString();
                    CreditorOrDebtor creditorList = (CreditorOrDebtor)this.CreditorOrDebtorList.FirstOrDefault(x => x.name == this.Dropdown_Cmbx.SelectedItem.ToString());
                    if (creditorList != null)
                    {
                        this.FullName_txt.Text = creditorList.name;
                        this.Address_txt.Text = creditorList.address;
                        this.MobileNo_txt.Text = creditorList.phone;
                        this.AlternateNo_txt.Text = creditorList.alternate;
                        this.Balance_txt.Text = creditorList.standingBalance.ToString();
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
                if (this.FullName_txt.Text != "" && this.Address_txt.Text != "" && this.MobileNo_txt.Text != "" && this.Balance_txt.Text != "")
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

        private void filterDropDown(object sender, KeyEventArgs e)
        {
            try
            {
                this.Dropdown_Cmbx.Text = this.Dropdown_txt.Text;
                if (this.FullName_txt.Text == "" && e.Key.ToString() != null)
                {
                    if (this.Dropdown_Cmbx.Text != "")
                    {
                        List<String> filteredList = this.AllNames.FindAll(x => x.Contains(this.Dropdown_Cmbx.Text.ToUpper()));
                        if (filteredList != null && filteredList.Count() > 0)
                        {
                            this.Dropdown_Cmbx.ItemsSource = filteredList;
                            this.Dropdown_Cmbx.IsDropDownOpen = true;
                        }
                        else
                        {
                            this.Dropdown_Cmbx.IsDropDownOpen = false;
                        }
                    }
                    else
                    {
                        this.Dropdown_Cmbx.IsDropDownOpen = false;
                    }
                }
                else
                {
                    this.Dropdown_Cmbx.IsDropDownOpen = false;
                    this.clearAll();
                    this.FullName_txt.IsEnabled = false;
                    this.Address_txt.IsEnabled = false;
                    this.MobileNo_txt.IsEnabled = false;
                    this.AlternateNo_txt.IsEnabled = false;
                    this.Balance_txt.IsEnabled = false;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex);
            }
        }

    }
}
