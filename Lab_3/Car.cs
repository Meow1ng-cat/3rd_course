using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Lab_3
{
    internal class Car : Vehicle
    {
        protected int m_passengerSeats = -1;
        public Car(int id, string brand, int maxSpeed, int passengerSeats) : base(id, brand, maxSpeed)
        {
            m_passengerSeats = passengerSeats;
        }
        public override void getInfo()
        {
            Console.WriteLine($"Id: {m_id}, Марка: {m_brand}, Максимальная скорость: {m_maxSpeed}, Кол-во пассажирских мест: {m_passengerSeats}");
        }
    }
}
