using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_8
{
    internal class ABand : BorrowerBand
    {
        public ABand() : base(8.0) { }
    }

    internal class BBand : BorrowerBand
    {
        public BBand() : base(12.0) { }
    }

    internal class CBand : BorrowerBand
    {
        public CBand() : base(18.0) { }
    }

    internal class DBand : BorrowerBand
    {
        public DBand() : base(25.0) { }
    }

    internal class GovGuaranteedBand : BorrowerBand
    {
        public GovGuaranteedBand() : base(3.0) { }

        public override int GetRate(LoanContext context) => 3;
    }
}
