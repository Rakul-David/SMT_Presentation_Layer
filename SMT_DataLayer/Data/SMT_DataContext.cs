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
        private string creditorFilepath;
        private string deptorFilepath;
        private int creditorLength;
        private int deptorlength;
        public SMT_DataContext()
        {
            creditorFilepath = "C:\\Users\\rakul\\source\\repos\\SMT_Presentation_Layer\\SMT_DataLayer\\Data\\Creditor Details";
            deptorFilepath = "C:\\Users\\rakul\\source\\repos\\SMT_Presentation_Layer\\SMT_DataLayer\\Data\\Deptor Details";
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
            string creditorDetail = File.ReadAllText(creditorFilepath + "\\CreditorDetails.txt");
            string deptorDetail = File.ReadAllText(deptorFilepath + "\\DeptorDetails.txt");

            Creditors = (creditorDetail.Length==0||creditorDetail==null)?new List<Creditor>():JsonSerializer.Deserialize<List<Creditor>>(creditorDetail);
            Deptors = (deptorDetail.Length == 0 || deptorDetail == null) ? new List<Deptor>() : JsonSerializer.Deserialize<List<Deptor>>(deptorDetail);
            creditorLength = Creditors.Count;
            deptorlength = Deptors.Count;
        }
        public bool SaveChange()
        {
            try
            {
                if (this.Creditors.Count != creditorLength || this.Deptors.Count != deptorlength)
                {
                    string CreditoModified = JsonSerializer.Serialize(this.Creditors);
                    string DeptorModified = JsonSerializer.Serialize(this.Deptors);
                    File.WriteAllText(creditorFilepath + "\\CreditorDetails.txt", string.Empty);
                    File.WriteAllText(creditorFilepath + "\\CreditorDetails.txt", CreditoModified);
                    File.WriteAllText(deptorFilepath + "\\DeptotorDetails.txt", string.Empty);
                    File.WriteAllText(deptorFilepath + "\\DeptotorDetails.txt", DeptorModified);
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
