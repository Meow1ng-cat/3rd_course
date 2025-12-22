using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_8
{
    internal class BorrowerBand
    {
        private readonly double _baseRate;
        public double BaseRate => _baseRate;

        protected BorrowerBand(double baseRate)
        {
            _baseRate = baseRate;
        }

        public virtual int GetRate(LoanContext context)
        {
            double rate = _baseRate;

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
