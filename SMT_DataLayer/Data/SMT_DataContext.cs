using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMT_DataLayer;
using System.Text.Json;
using System.IO;
namespace SMT_DataLayer.Data
{
    public class SMT_DataContext
    {
        public List<CreditorOrDebtor> CreditorOrDebtor;
     
        public List<Invoice> Invoices;
        public List<PayIn_Out> Payee;
        private string creditorOrDebtorFilepath;
        private string deptorFilepath;
        private string invoiceFilepath;
        private int payeeLength;
        private int creditorLength;
  
        private int invoiceLength;
        public SMT_DataContext()
        {
            try
            {
                string dir = Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName).FullName + "\\SMT_DataLayer\\Data";
                creditorOrDebtorFilepath = dir+"\\Creditor Details";
                invoiceFilepath = dir+"\\Invoice Details";
                if (!Directory.Exists(creditorOrDebtorFilepath))
                {
                    Directory.CreateDirectory(creditorOrDebtorFilepath);
                }
                if (!Directory.Exists(invoiceFilepath))
                {
                    Directory.CreateDirectory(invoiceFilepath);
                }
                if (!File.Exists(creditorOrDebtorFilepath + "\\CreditorDetails.txt"))
                {
                    FileStream fileStream = new FileStream(creditorOrDebtorFilepath + "\\CreditorOrDebtorDetails.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);
                    fileStream.Close();
                }
                if (!File.Exists(invoiceFilepath + "\\InvoiceDetails.txt"))
                {
                    FileStream fileStream = new FileStream(invoiceFilepath + "\\InvoiceDetails.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);
                    fileStream.Close();
                }
                if (!File.Exists(invoiceFilepath + "\\Payee_Details.txt"))
                {
                    FileStream fileStream = new FileStream(invoiceFilepath + "\\Payee_Details.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);
                    fileStream.Close();
                }
                string creditorDetail = File.ReadAllText(creditorOrDebtorFilepath + "\\CreditorOrDebtorDetails.txt");
              
                string invoicedetail = File.ReadAllText(invoiceFilepath + "\\InvoiceDetails.txt");
                string payeeDetails = File.ReadAllText(invoiceFilepath + "\\Payee_Details.txt");
                CreditorOrDebtor = (creditorDetail.Trim().Length == 0 || creditorDetail == null) ? new List<CreditorOrDebtor>() : JsonSerializer.Deserialize<List<CreditorOrDebtor>>(creditorDetail);
               
                Invoices = (invoicedetail.Trim().Length == 0 || invoicedetail == null) ? new List<Invoice>() : JsonSerializer.Deserialize<List<Invoice>>(invoicedetail);
                Payee = (payeeDetails.Trim().Length == 0 || payeeDetails == null) ? new List<PayIn_Out>() : JsonSerializer.Deserialize<List<PayIn_Out>>(payeeDetails);
                creditorLength = CreditorOrDebtor.Count;
                invoiceLength = Invoices.Count;
                payeeLength = Payee.Count;
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex);
            }
        }
        public bool SaveChange()
        {
            try
            {
                string CreditoModified = JsonSerializer.Serialize(this.CreditorOrDebtor);
                string InvoiceModified = JsonSerializer.Serialize(this.Invoices);
                string PayeeModified = JsonSerializer.Serialize(this.Payee);
                if (this.CreditorOrDebtor.Count != creditorLength )
                {
                    File.WriteAllText(creditorOrDebtorFilepath + "\\CreditorOrDebtorDetails.txt", string.Empty);
                    File.WriteAllText(creditorOrDebtorFilepath + "\\CreditorOrDebtorDetails.txt", CreditoModified);
                }
                if (this.Invoices.Count != invoiceLength)
                {
                    File.WriteAllText(invoiceFilepath + "\\InvoiceDetails.txt", string.Empty);
                    File.WriteAllText(invoiceFilepath + "\\InvoiceDetails.txt", InvoiceModified);
                }
                if (this.Payee.Count != payeeLength)
                {
                    File.WriteAllText(invoiceFilepath + "\\Payee_Details.txt", string.Empty);
                    File.WriteAllText(invoiceFilepath + "\\Payee_Details.txt", PayeeModified);
                }
                return true;
            }
            catch(Exception ex)
            {
                ErrorLog.Log(ex);
                return false;
            }
        }
        public bool SaveChange(int n)
        {
            try
            {
                if (n == 1)
                {
                    string CreditoModified = JsonSerializer.Serialize(this.CreditorOrDebtor);
                    File.WriteAllText(creditorOrDebtorFilepath + "\\CreditorOrDebtorDetails.txt", string.Empty);
                    File.WriteAllText(creditorOrDebtorFilepath + "\\CreditorOrDebtorDetails.txt", CreditoModified);
                }
                else if (n == 2)
                {
                    string InvoiceModified = JsonSerializer.Serialize(this.Invoices);
                    File.WriteAllText(invoiceFilepath + "\\InvoiceDetails.txt", string.Empty);
                    File.WriteAllText(invoiceFilepath + "\\InvoiceDetails.txt", InvoiceModified);
                }
                else if (n == 3)
                {
                    string PayeeModified = JsonSerializer.Serialize(this.Payee);
                    File.WriteAllText(invoiceFilepath + "\\Payee_Details.txt", string.Empty);
                    File.WriteAllText(invoiceFilepath + "\\Payee_Details.txt", PayeeModified);
                }
                return true;
            }
            catch(Exception ex)
            {
                ErrorLog.Log(ex);
                return false;
            }
        }
    }
}
