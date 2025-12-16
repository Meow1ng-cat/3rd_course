using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_3
{
    internal class Train : Vehicle
    {
        protected int m_carriagesCount = -1; 
        public Train(int id, string brand, int maxSpeed, int carriagesCount) : base(id, brand, maxSpeed)
        {
            m_carriagesCount = carriagesCount;
        }

        public override void getInfo()
        {
            Console.WriteLine($"Id: {m_id}, Марка: {m_brand}, Максимальная скорость: {m_maxSpeed}, Кол-во пассажирских мест: {m_carriagesCount}");
        }
    }
}
