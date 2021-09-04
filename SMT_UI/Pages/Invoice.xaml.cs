﻿using System;
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
        public Invoice()
        {
            InitializeComponent();
            CreditorList = new List<CreditorOrDebtor>();
            repository = new SMT_DataRepository();
            CreditorOrDebtor = "";
        }

        private void NavServiceOnNavigated(object sender, NavigationEventArgs args)
        {
            this.NavigationService.RemoveBackEntry();
            this.NavigationService.Navigated -= NavServiceOnNavigated;
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
                // MessageBox.Show("Submitted!", "Validation Failed", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            return IsValid;
        }


        List<InvoiceDetails> DetailsList = new List<InvoiceDetails>();
        //private void Add_btn_Click(object sender, RoutedEventArgs e)
        //{
        //    bool IsValid = Fieldvalidations();
        //    int serialno = 1;
        //    String Item_Name = ItemName_txt.Text;
        //    String date = Dates_dtd.Text;
        //    double Quantity = Convert.ToDouble(Qnty_txt.Text);
        //    String Units =Units_Cmbx.Text;
        //    double PricePerUnit = Convert.ToDouble(PricePerUnit_txt.Text);
        //    double subTotal = Quantity * PricePerUnit;

        //    if (IsValid == true)
        //    {
        //        this.DetailsList.Add(new InvoiceDetails() { Serial_no = serialno, Product_Name = Item_Name, Quantity = Quantity, Units = Units, Units_Per_Price = PricePerUnit, Date = date, Sub_Total = subTotal });
        //        this.FullInvoice.ItemsSource = DetailsList;
        //        this.Yes_radio.IsEnabled = true;
        //        this.No_radio.IsEnabled = true;
        //    }
        //}


        private void Add_btn_Click(object sender, RoutedEventArgs e)
        {
            InvoiceDetails invoiced = new InvoiceDetails();
            invoiced.Product_Name = ItemName_txt.Text;
            invoiced.Date = Dates_dtd.Text;
            invoiced.Quantity = Convert.ToDouble(Qnty_txt.Text);
            invoiced.Units = Units_Cmbx.Text;
            invoiced.Units_Per_Price = Convert.ToDouble(PricePerUnit_txt.Text);
            invoiced.Sub_Total = invoiced.Quantity * invoiced.Units_Per_Price;
            FullInvoice.Items.Add(invoiced);

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
            }
            else
            {
                this.Add_btn.IsEnabled = false;
            }
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

        private void filterDropDown(object sender, KeyEventArgs e)
        {
            try
            {
                this.Dropdown_Cmbx.Text = this.Dropdown_txt.Text;
                if (e.Key.ToString() != null)
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
                    this.ItemName_txt.IsEnabled = false;
                    this.Dates_dtd.IsEnabled = false;
                    this.Qnty_txt.IsEnabled = false;
                    this.Units_Cmbx.IsEnabled = false;
                    this.PricePerUnit_txt.IsEnabled = false;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex);
            }
        }

        private void clearAll()
        {
            try
            {
                this.Dropdown_Cmbx.SelectedIndex = -1;
                this.Dropdown_txt.Clear();
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

    }
}
