using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMT_UI
{
    class Enumeration
    {
        public static Dictionary<int, string> Atributes = new Dictionary<int, string>()
        {
            {(int)Details.Creditor,"00000000-0000-0000-0000-000000000001" },
            {(int)Details.Deptor,"00000000-0000-0000-0000-000000000002" }
        };
    }
    enum Details
    {
        Creditor = 1,
        Deptor = 2,
        Invoice = 3,
        PayIn_Out = 4
    }
}
