using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_8
{
    public enum CreditBand
    {
        A, B, C, D, GovGuaranteed
    }

    internal class InterestCalculatorEnum
    {
        public static int CalculateRate(CreditBand band, LoanContext context)
        {
            double baseRate = band switch
            {
                CreditBand.A => 8.0,
                CreditBand.B => 12.0,
                CreditBand.C => 18.0,
                CreditBand.D => 25.0,
                CreditBand.GovGuaranteed => 3.0,
                _ => 0.0
            };

            if (band == CreditBand.GovGuaranteed)
                return 3;

            double rate = baseRate;

            if (context.HasCollateral)
                rate = Math.Max(0, rate - 3);
            if (context.IsFirstTime)
                rate += 2;
            if (context.HasPromo)
                rate = Math.Max(0, rate - 2);

            return (int)Math.Round(rate);
        }
    }
}
