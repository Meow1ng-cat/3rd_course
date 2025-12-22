using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_8
{
    internal class LowLevelStats
    {
        private int _lowLevelCount = 0;
        private const int LowThreshold = 30;

        public void OnLevelChanged(BatteryMonitor sender, int level)
        {
            if (level < LowThreshold)
            {
                _lowLevelCount++;
            }
        }

        public void Report()
        {
            Console.WriteLine($"\nСтатистика: Ниже {LowThreshold}% было {_lowLevelCount} раз(а)");
        }
    }
}
