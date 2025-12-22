using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_8
{
    internal class ConsoleHud
    {
        public void OnLevelChanged(BatteryMonitor sender, int level)
        {
            Console.WriteLine($"Уровень: {level}%");
        }

        public void OnCriticalLowReached(object? sender, int level)
        {
            Console.WriteLine($"Низкий заряд: {level}% — включите энергосбережение!");
        }
    }
}
