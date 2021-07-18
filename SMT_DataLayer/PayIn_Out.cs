using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMT_DataLayer
{
    public class PayIn_Out
    {
        public string name { get; set; }
        public int invoiceId { get; set; }
        public double amount { get; set; }
        public double standingBalance { get; set; }
        public string payer { get; set; }
        public DateTime Date { get; set; }
    }
}
