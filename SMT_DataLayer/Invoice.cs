using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMT_DataLayer
{
    public class Invoice
    {
        private static int i = 0;
        public int generateId()
        {
            return ++i;
        }
        public int invoiceId { get; set; }
        public int forId { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public string productName { get; set; }
        public int quantity { get; set; }
        public string units { get; set; }
        public double price { get; set; }
        public double total { get; set; }
    }
}
