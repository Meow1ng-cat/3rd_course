using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Lab_4
{
    internal class PaymentGateway
    {
        protected string m_providerName = "noName";
        protected bool m_sandBox = false;
        decimal loadProc = 0;

        public virtual void Process()
        {
            char[] frames = { '|', '/', '-', '\\' };
            int currentFrame = 0;

            Console.WriteLine("Processing...");

            for (int i = 0; i <= 100; i++)
            {
                Console.SetCursorPosition(0, 1);
                Console.Write($"{frames[currentFrame]} Process: {i}%");

                currentFrame = (currentFrame + 1) % frames.Length;
                Thread.Sleep(10);
            }

            Console.WriteLine("\nDone!");
        }

        public PaymentGateway() { }

        public string providerName
        {
            set 
            {
                if (!string.IsNullOrEmpty(value))
                {
                    m_providerName = value;
                }
                else
                {
                    Console.WriteLine("Введено пустое значение");
                }
            }
            get { return m_providerName; }
        }

        public bool sandBox
        {
            set { m_sandBox = value; }
            get { return m_sandBox; }
        }

        public void switchSandbox()
        {
            m_sandBox = !m_sandBox;
        }

        public virtual void getInfo()
        {
            Process();
            Console.WriteLine($"Provider name: {m_providerName}, Sand box: {m_sandBox}");  //В задаче написано "Возвращает",
                                                                                           //но вроде бы логично было бы в Info выводит такую инфу
        }
    }
}
