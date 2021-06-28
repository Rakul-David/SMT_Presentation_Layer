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
        private string creditorFilepath;
        private string deptorFilepath;
        private string invoiceFilepath;
        private int creditorLength;
        private int deptorlength;
        private int invoiceLength;
        public SMT_DataContext()
        {
            creditorFilepath = "C:\\Users\\rakul\\source\\repos\\SMT_Presentation_Layer\\SMT_DataLayer\\Data\\Creditor Details";
            deptorFilepath = "C:\\Users\\rakul\\source\\repos\\SMT_Presentation_Layer\\SMT_DataLayer\\Data\\Deptor Details";
            invoiceFilepath = "C:\\Users\\rakul\\source\\repos\\SMT_Presentation_Layer\\SMT_DataLayer\\Data\\Invoice Details";
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
            if (!File.Exists(invoiceFilepath + "\\InvoiceDetails.txt")){
                FileStream fileStream = new FileStream(invoiceFilepath + "\\InvoiceDetails.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);
                fileStream.Close();
            }
            string creditorDetail = File.ReadAllText(creditorFilepath + "\\CreditorDetails.txt");
            string deptorDetail = File.ReadAllText(deptorFilepath + "\\DeptorDetails.txt");
            string invoicedetail= File.ReadAllText(invoiceFilepath + "\\InvoiceDetails.txt");
            Creditors = (creditorDetail.Length==0||creditorDetail==null)?new List<Creditor>():JsonSerializer.Deserialize<List<Creditor>>(creditorDetail);
            Deptors = (deptorDetail.Length == 0 || deptorDetail == null) ? new List<Deptor>() : JsonSerializer.Deserialize<List<Deptor>>(deptorDetail);
            Invoices= (invoicedetail.Length == 0 || invoicedetail == null) ? new List<Invoice>() : JsonSerializer.Deserialize<List<Invoice>>(invoicedetail);
            creditorLength = Creditors.Count;
            deptorlength = Deptors.Count;
            invoiceLength = Invoices.Count;
        }
        public bool SaveChange()
        {
            try
            {
                string CreditoModified = JsonSerializer.Serialize(this.Creditors);
                string DeptorModified = JsonSerializer.Serialize(this.Deptors);
                string InvoiceModified = JsonSerializer.Serialize(this.Invoices);
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
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}
