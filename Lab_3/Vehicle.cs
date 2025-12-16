using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_3
{
    internal class Vehicle
    {
        protected int m_id = -1;
        protected string m_brand = "noBrand";
        protected int m_maxSpeed = -1;

        public Vehicle() { }

        public Vehicle(int id, string brand, int maxSpeed)
        {
            m_id = id;
            m_brand = brand;
            m_maxSpeed = maxSpeed;
        }

        public virtual void getInfo()
        {
            Console.WriteLine($"Id: {m_id}, Марка: {m_brand}, Максимальная скорость: {m_maxSpeed}");
        }

    }
}
