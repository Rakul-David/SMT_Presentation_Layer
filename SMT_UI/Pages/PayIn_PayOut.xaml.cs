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
using SMT_DataLayer.Repository;

namespace SMT_UI.Pages
{
    /// <summary>
    /// Interaction logic for PayIn_PayOut.xaml
    /// </summary>
    public partial class PayIn_PayOut : Page
    {
        String CreditorOrDebtor;
        List<String> AllNames;
        List<CreditorOrDebtor> CreditorList;
        SMT_DataRepository repository;
        public PayIn_PayOut()
        {
            InitializeComponent();
            CreditorList = new List<CreditorOrDebtor>();
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
                this.Title_lbl.Content = "PAY " + type;
                //if (type == "IN")
                //{
                //    this.CreditorList = repository.GetAllCreditor();
                //}
                //else if (type == "OUT")
                //{
                //    this.DebtorList = repository.GetAllDeptor();
                //}
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex);
            }
        }

    }
}
