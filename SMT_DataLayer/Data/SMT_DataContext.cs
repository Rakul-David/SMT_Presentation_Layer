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
        public List<Creditor> Creditors;
        public List<Deptor> Deptors;
        public List<Invoice> Invoices;
        public List<PayIn_Out> Payee;
        private string creditorFilepath;
        private string deptorFilepath;
        private string invoiceFilepath;
        private int payeeLength;
        private int creditorLength;
        private int deptorlength;
        private int invoiceLength;
        public SMT_DataContext()
        {
            try
            {
                string dir = Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName).FullName;
                dir += "\\SMT_DataLayer\\Data";
                creditorFilepath = dir+"\\Creditor Details";
                deptorFilepath = dir+"\\Deptor Details";
                invoiceFilepath = dir+"\\Invoice Details";
                if (!Directory.Exists(creditorFilepath))
                {
                    Directory.CreateDirectory(creditorFilepath);
                }
                if (!Directory.Exists(deptorFilepath))
                {
                    Directory.CreateDirectory(deptorFilepath);
                }
                if (!Directory.Exists(invoiceFilepath))
                {
                    Directory.CreateDirectory(invoiceFilepath);
                }
                if (!File.Exists(creditorFilepath + "\\CreditorDetails.txt"))
                {
                    FileStream fileStream = new FileStream(creditorFilepath + "\\CreditorDetails.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);
                    fileStream.Close();
                }
                if (!File.Exists(deptorFilepath + "\\DeptorDetails.txt"))
                {
                    FileStream fileStream = new FileStream(deptorFilepath + "\\DeptorDetails.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);
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
                string creditorDetail = File.ReadAllText(creditorFilepath + "\\CreditorDetails.txt");
                string deptorDetail = File.ReadAllText(deptorFilepath + "\\DeptorDetails.txt");
                string invoicedetail = File.ReadAllText(invoiceFilepath + "\\InvoiceDetails.txt");
                string payeeDetails = File.ReadAllText(invoiceFilepath + "\\Payee_Details.txt");
                Creditors = (creditorDetail.Trim().Length == 0 || creditorDetail == null) ? new List<Creditor>() : JsonSerializer.Deserialize<List<Creditor>>(creditorDetail);
                Deptors = (deptorDetail.Trim().Length == 0 || deptorDetail == null) ? new List<Deptor>() : JsonSerializer.Deserialize<List<Deptor>>(deptorDetail);
                Invoices = (invoicedetail.Trim().Length == 0 || invoicedetail == null) ? new List<Invoice>() : JsonSerializer.Deserialize<List<Invoice>>(invoicedetail);
                Payee = (payeeDetails.Trim().Length == 0 || payeeDetails == null) ? new List<PayIn_Out>() : JsonSerializer.Deserialize<List<PayIn_Out>>(payeeDetails);
                creditorLength = Creditors.Count;
                deptorlength = Deptors.Count;
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
                string CreditoModified = JsonSerializer.Serialize(this.Creditors);
                string DeptorModified = JsonSerializer.Serialize(this.Deptors);
                string InvoiceModified = JsonSerializer.Serialize(this.Invoices);
                string PayeeModified = JsonSerializer.Serialize(this.Payee);
                if (this.Creditors.Count != creditorLength )
                {

                    File.WriteAllText(creditorFilepath + "\\CreditorDetails.txt", string.Empty);
                    File.WriteAllText(creditorFilepath + "\\CreditorDetails.txt", CreditoModified);
                }
                if(this.Deptors.Count != deptorlength) { 
                    File.WriteAllText(deptorFilepath + "\\DeptotorDetails.txt", string.Empty);
                    File.WriteAllText(deptorFilepath + "\\DeptotorDetails.txt", DeptorModified);
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
                    string CreditoModified = JsonSerializer.Serialize(this.Creditors);
                    File.WriteAllText(creditorFilepath + "\\CreditorDetails.txt", string.Empty);
                    File.WriteAllText(creditorFilepath + "\\CreditorDetails.txt", CreditoModified);
                }
                else if (n == 2)
                {
                    string DeptorModified = JsonSerializer.Serialize(this.Deptors);
                    File.WriteAllText(deptorFilepath + "\\DeptotorDetails.txt", string.Empty);
                    File.WriteAllText(deptorFilepath + "\\DeptotorDetails.txt", DeptorModified);
                }
                else if (n == 3)
                {
                    string InvoiceModified = JsonSerializer.Serialize(this.Invoices);
                    File.WriteAllText(invoiceFilepath + "\\InvoiceDetails.txt", string.Empty);
                    File.WriteAllText(invoiceFilepath + "\\InvoiceDetails.txt", InvoiceModified);
                }
                else if (n == 4)
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
