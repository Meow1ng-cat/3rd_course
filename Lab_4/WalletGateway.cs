using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_4
{
    internal class WalletGateway : PaymentGateway
    {
        protected int m_walletId = -1;

        public WalletGateway() { }

        public int walletId 
        {
            set
            {
                if (value > 0)
                {
                    m_walletId = value;
                }
            }
            get { return m_walletId; }
        }

        public override void getInfo()
        {
            Process();
            Console.WriteLine($"Provider name: {m_providerName}, Sand box: {m_sandBox}, Wallet id: {m_walletId}");
        }
    }
}
