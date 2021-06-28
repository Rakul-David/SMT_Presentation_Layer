using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMT_DataLayer
{
    public class Deptor
    {
        static int i = 0;
        public Deptor()
        {
            i++;
        }
        public int id = i; 
        public string name { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public string alternate { get; set; }
        public double standingBalance { get; set; }

    }
}
