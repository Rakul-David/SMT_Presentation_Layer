using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMT_DataLayer
{
    public class CreditorOrDebitor
    {
        public Guid id = Guid.NewGuid();
        public Guid UserIdentity { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public string alternate { get; set; }
        public double standingBalance { get; set; }
    }
}
