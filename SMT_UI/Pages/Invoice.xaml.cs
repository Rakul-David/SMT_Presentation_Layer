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
        String CreditorOrDebtor;
        List<String> AllNames;
        List<CreditorOrDebtor> CreditorList;
        SMT_DataRepository repository;
        List<InvoiceDetails> DetailsList;

        public Invoice()
        {
            InitializeComponent();
            CreditorList = new List<CreditorOrDebtor>();
            repository = new SMT_DataRepository();
            CreditorOrDebtor = "";
            DetailsList = new List<InvoiceDetails>();
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            MainPage mainPage = new MainPage();
            this.NavigationService.Navigate(mainPage);
        }

        public void staticDetails(String type)
        {
            this.CreditorOrDebtor = type;
            this.Title_lbl.Content = type + " INVOICE";
            this.CreditorList = repository.GetAllCreditorOrDebtor(type);
            if (this.CreditorList.Count == 0)
            {
                if (MessageBox.Show("No Records", "Error!", MessageBoxButton.OK, MessageBoxImage.Information) == MessageBoxResult.OK)
                {
                    MainPage mainPage = new MainPage();
                    this.NavigationService.Navigate(mainPage);
                }
            }
            else
            {
                this.AllNames = this.CreditorList.Select(x => x.name).ToList();
            }

            this.Dropdown_Cmbx.ItemsSource = this.AllNames;
        }

        private void filterDropDown(object sender, KeyEventArgs e)
        {
            try
            {
                this.Dropdown_Cmbx.Text = this.Dropdown_txt.Text;
                if (this.FullInvoice.Items.Count == 0 && e.Key.ToString() != null)
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
                    this.Dropdown_txt.Clear();
                    this.ItemName_txt.IsEnabled = false;
                    this.Dates_dtd.IsEnabled = false;
                    this.Qnty_txt.IsEnabled = false;
                    this.Units_Cmbx.IsEnabled = false;
                    this.PricePerUnit_txt.IsEnabled = false;
                    this.Save_btn.IsEnabled = false;
                    this.No_radio.IsChecked = false;
                    this.Yes_radio.IsChecked = false;
                    this.No_radio.IsEnabled = false;
                    this.Yes_radio.IsEnabled = false;
                    this.TotalAmount_lbl.Content = "";
                    this.SGST_lbl.Content = "";
                    this.CGST_lbl.Content = "";
                    this.GrandTotal_lbl.Content = "";
                    this.FullInvoice.Items.Clear();
                    this.clearAll();
                    this.EnableButtonValidation();
                }
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
                    this.ItemName_txt.IsEnabled = true;
                    this.Dates_dtd.IsEnabled = true;
                    this.Qnty_txt.IsEnabled = true;
                    this.Units_Cmbx.IsEnabled = true;
                    this.PricePerUnit_txt.IsEnabled = true;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex);
            }
        }

        private void NumberValidationQnty(object sender, KeyEventArgs e)
        {
            try
            {
                var keyString = e.Key.ToString();
                if (keyString.Length == 2)
                {
                    keyString = keyString.Substring(1);
                }
                else if (keyString.Length == 7 && keyString.Contains("NumPad"))
                {
                    keyString = keyString.Substring(6);
                }
                if ((keyString.Length == 1 && char.IsDigit(keyString[0])) || ((keyString == "OemPeriod" || keyString == "Decimal") && (!this.Qnty_txt.Text.Contains(".")) && this.Qnty_txt.Text.Length > 0) || keyString == "Back" || keyString == "Delete" || keyString == "Tab")
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }

            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex);
            }
        }

        private void NumberValidationPrice(object sender, KeyEventArgs e)
        {
            try
            {
                var keyString = e.Key.ToString();
                if (keyString.Length == 2)
                {
                    keyString = keyString.Substring(1);
                }
                else if (keyString.Length == 7 && keyString.Contains("NumPad"))
                {
                    keyString = keyString.Substring(6);
                }
                if ((keyString.Length == 1 && char.IsDigit(keyString[0])) || ((keyString == "OemPeriod" || keyString == "Decimal") && (!this.PricePerUnit_txt.Text.Contains(".")) && this.PricePerUnit_txt.Text.Length > 0) || keyString == "Back" || keyString == "Delete" || keyString == "Tab")
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }

            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex);
            }
        }

        private void EnableButton(object sender, KeyEventArgs e)
        {
            this.EnableButtonValidation();
        }

        private void Units_Cmbx_Selection(object sender, SelectionChangedEventArgs e)
        {
            this.EnableButtonValidation();
        }

        private void Date_Filled(object sender, SelectionChangedEventArgs e)
        {
            this.EnableButtonValidation();
        }

        private void EnableButtonValidation()
        {
            if (this.ItemName_txt.Text != "" && this.Dates_dtd.Text != "" && this.Qnty_txt.Text != "" && this.PricePerUnit_txt.Text != "" && this.Units_Cmbx.SelectedIndex != -1)
            {
                this.Add_btn.IsEnabled = true;
                if (this.FullInvoice.SelectedIndex > 0)
                {
                    this.Update_btn.IsEnabled = true;
                    this.Delete_btn.IsEnabled = true;
                }
                else
                {
                    this.Update_btn.IsEnabled = false;
                    this.Delete_btn.IsEnabled = false;
                }
            }
            else
            {
                this.Add_btn.IsEnabled = false;
                this.Update_btn.IsEnabled = false;
                this.Delete_btn.IsEnabled = false;
            }
        }

        public void Add_btn_Click(object sender, RoutedEventArgs e)
        {
            InvoiceDetails invoiced = new InvoiceDetails();
            invoiced.Product_Name = ItemName_txt.Text;
            invoiced.Date = Dates_dtd.Text;
            invoiced.Quantity = Math.Round(Convert.ToDouble(Qnty_txt.Text), 2);
            invoiced.Units = Units_Cmbx.Text;
            invoiced.Units_Per_Price = Math.Round(Convert.ToDouble(PricePerUnit_txt.Text), 2);
            invoiced.Sub_Total = Math.Round(invoiced.Quantity * invoiced.Units_Per_Price, 2);
            this.FullInvoice.Items.Add(invoiced);
            this.DetailsList.Add(invoiced);
            this.Yes_radio.IsEnabled = true;
            this.No_radio.IsEnabled = true;
            if (this.Yes_radio.IsChecked == false && this.No_radio.IsChecked == false)
            {
                this.No_radio.IsChecked = true;
            }
            this.Save_btn.IsEnabled = true;
            this.FullInvoice.SelectedIndex = -1;
            this.totalCalculation();
            this.clearAll();
            this.EnableButtonValidation();
        }

        private void Update_btn_Click(object sender, RoutedEventArgs e)
        {
            InvoiceDetails invoiced = new InvoiceDetails();
            invoiced.Product_Name = ItemName_txt.Text;
            invoiced.Date = Dates_dtd.Text;
            invoiced.Quantity = Math.Round(Convert.ToDouble(Qnty_txt.Text), 2);
            invoiced.Units = Units_Cmbx.Text;
            invoiced.Units_Per_Price = Math.Round(Convert.ToDouble(PricePerUnit_txt.Text), 2);
            invoiced.Sub_Total = Math.Round(invoiced.Quantity * invoiced.Units_Per_Price, 2);
            this.FullInvoice.Items.Add(invoiced);
            this.DetailsList.Add(invoiced);
            int indexno = FullInvoice.SelectedIndex;
            this.DetailsList.RemoveAt(indexno);
            this.FullInvoice.Items.RemoveAt(indexno);
            this.FullInvoice.Items.Refresh();
            this.totalCalculation();
            this.clearAll();
            this.EnableButtonValidation();
        }

        private void Delete_btn_Click(object sender, RoutedEventArgs e)
        {
            this.ItemName_txt.IsEnabled = false;
            this.Dates_dtd.IsEnabled = false;
            this.Qnty_txt.IsEnabled = false;
            this.Units_Cmbx.IsEnabled = false;
            this.PricePerUnit_txt.IsEnabled = false;
            int indexno = FullInvoice.SelectedIndex;
            if (MessageBox.Show("Are you sure you want to delete selected item?", "Confirm Deletion", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
            {
                this.FullInvoice.Items.RemoveAt(indexno);
                this.DetailsList.RemoveAt(indexno);
            }
            if (FullInvoice.Items.Count == 0)
            {
                this.Yes_radio.IsEnabled = false;
                this.No_radio.IsEnabled = false;
                this.Save_btn.IsEnabled = false;
            }
            this.FullInvoice.Items.Refresh();
            this.totalCalculation();
            this.clearAll();
            this.EnableButtonValidation();
        }

        private bool Fieldvalidations()
        {
            bool IsValid = false;
            String Item_Name = ItemName_txt.Text;
            String date = Dates_dtd.Text;
            String Quantity = Qnty_txt.Text;
            String Units = Units_Cmbx.SelectedItem.ToString();
            String PricePerUnit = PricePerUnit_txt.Text;
            if (Item_Name.Length < 2)
            {
                MessageBox.Show("Enter a valid item name!", "Validation Failed", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (date == "")
            {
                MessageBox.Show("Enter a valid date!", "Validation Failed", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (Quantity == "")
            {
                MessageBox.Show("Enter a quantity!", "Validation Failed", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (Units == "")
            {
                MessageBox.Show("Select a unit!", "Validation Failed", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (PricePerUnit == "")
            {
                MessageBox.Show("Enter valid price per unit!", "Validation Failed", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                IsValid = true;
            }
            return IsValid;
        }

        public void Row_no(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }

        private void clearAll()
        {
            try
            {
                this.ItemName_txt.Clear();
                this.Dates_dtd.Text = null;
                this.Qnty_txt.Clear();
                this.Units_Cmbx.SelectedIndex = -1;
                this.PricePerUnit_txt.Clear();
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex);
            }
        }

        private void FullInvoice_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int indexno = this.FullInvoice.SelectedIndex;
            this.ItemName_txt.IsEnabled = true;
            this.Dates_dtd.IsEnabled = true;
            this.Qnty_txt.IsEnabled = true;
            this.Units_Cmbx.IsEnabled = true;
            this.PricePerUnit_txt.IsEnabled = true;
            if (indexno >= 0)
            {
                this.ItemName_txt.Text = DetailsList[indexno].Product_Name;
                this.Dates_dtd.Text = DetailsList[indexno].Date;
                this.Qnty_txt.Text = DetailsList[indexno].Quantity.ToString();
                this.Units_Cmbx.Text = DetailsList[indexno].Units;
                this.PricePerUnit_txt.Text = DetailsList[indexno].Units_Per_Price.ToString();
                this.Delete_btn.IsEnabled = true;
                this.Update_btn.IsEnabled = true;
                this.Add_btn.IsEnabled = true;
            }
        }

        private void No_radio_Checked(object sender, RoutedEventArgs e)
        {
            this.totalCalculation();
        }

        private void Yes_radio_Checked(object sender, RoutedEventArgs e)
        {
            this.totalCalculation();
        }

        private void totalCalculation()
        {
            var total = 0.0;
            var gst = 0.0;
            foreach (var item in this.DetailsList)
            {
                total += item.Sub_Total;
            }
            this.TotalAmount_lbl.Content = Math.Round(total, 2);
            if (this.Yes_radio.IsChecked == true)
            {
                gst = Math.Round(0.09 * total, 2);
            }
            else if (this.No_radio.IsChecked == true)
            {
                gst = 0.0;
            }
            this.SGST_lbl.Content = gst;
            this.CGST_lbl.Content = gst;
            this.GrandTotal_lbl.Content = Math.Round(total + 2 * gst, 2);
        }

        private void Save_btn_Click(object sender, RoutedEventArgs e)
        {

        }
        private class InvoiceDetails
        {
            public int Serial_no { get; set; }
            public String Product_Name { get; set; }
            public String Date { get; set; }
            public double Quantity { get; set; }
            public string Units { get; set; }
            public double Units_Per_Price { get; set; }
            public double Sub_Total { get; set; }
        }

    }
}
