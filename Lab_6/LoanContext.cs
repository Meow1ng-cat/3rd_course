using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_8
{
    internal class LoanContext
    {
        public bool HasCollateral { get; set; }
        public bool IsFirstTime { get; set; }
        public bool HasPromo { get; set; }
    }
}
