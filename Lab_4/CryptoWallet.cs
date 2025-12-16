using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_4
{
    internal class CryptoWallet : WalletGateway
    {
        private string m_network = "noNetwork";
        private List<string> m_networkList = new() { "BTC", "ETH" };

        public CryptoWallet() { }

        public string network
        {
            set {if(m_networkList.Contains(value))
                {
                    m_network = value;
                }
            } 
            get { return m_network; }
        }

        public override void getInfo()
        {
            Process();
            Console.WriteLine($"Provider name: {m_providerName}, Sand box: {m_sandBox}, Wallet id: {m_walletId}, Network: {m_network}");
        }
    }
}
