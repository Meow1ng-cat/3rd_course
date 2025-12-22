using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_8
{
    internal class InterestCalculatorOop
    {
        public static int CalculateRate(BorrowerBand band, LoanContext context)
        {
            return band.GetRate(context);
        }
    }
}
