using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMT_DataLayer
{
    public class Invoice
    {
      
        public Guid forid { get; set; }
        public Guid forUid { get; set; }

        public Guid id = Guid.NewGuid();
        public double total { get; set; }
        public bool gst { get; set; }
        public DateTime invoiceDate { get; set; }
        public List<InvoiceDetails> invoiceDetails { get; set; }
    }
    public class InvoiceDetails
    {
        public string productName { get; set; }
        public double quantity { get; set; }
        public string units { get; set; }
        public double price { get; set; }
        public double subtotal { get; set; }
        public string date { get; set; }
    }
}