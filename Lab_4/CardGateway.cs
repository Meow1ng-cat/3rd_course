using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Lab_4
{
    internal class CardGateway : PaymentGateway
    {
        protected string m_maskedPan = "**** **** **** ****";

        public CardGateway() { }

        public string maskedPan
        {
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    if (Regex.IsMatch(value, @"^([*0-9]{4} ){3}[*0-9]{4}$"))
                    {
                        m_maskedPan = value;
                    }
                    else
                    {
                        Console.WriteLine("Введеное значение не подходи по формату: **** **** **** ****");
                    }
                }
                else
                {
                    Console.WriteLine("Введено пустое значение");
                }
            }
            get { return m_maskedPan; }
        }
        public override void getInfo()
        {
            Process();
            Console.WriteLine($"Provider name: {m_providerName}, Sand box: {m_sandBox}, Masked pan: {m_maskedPan}"); 
        }
    }
}
