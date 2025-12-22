using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_8
{
    internal delegate void BatteryEventHandler(BatteryMonitor sender, int level);

    internal class BatteryMonitor
    {
        public event BatteryEventHandler? LevelChanged;

        private EventHandler<int>? _criticalLowHandlers;
        public event EventHandler<int>? CriticalLowReached
        {
            add
            {
                Console.WriteLine($"Подписчик добавлен на CriticalLowReached: {value?.Method.Name}");
                _criticalLowHandlers += value;
            }
            remove
            {
                Console.WriteLine($"Подписчик удалён из CriticalLowReached: {value?.Method.Name}");
                _criticalLowHandlers -= value;
            }
        }

        private readonly Random _random = new Random();
        private const int CriticalThreshold = 15;
        private const int LowThreshold = 30;
        private readonly int _dischargeSteps;

        public BatteryMonitor(int dischargeSteps = 10)
        {
            _dischargeSteps = dischargeSteps;
        }

        public void Start()
        {
            int currentLevel = 100;

            for (int i = 0; i < _dischargeSteps; i++)
            {
                int discharge = _random.Next(5, 15);
                currentLevel = Math.Max(0, currentLevel - discharge);

                LevelChanged?.Invoke(this, currentLevel);

                if (currentLevel < CriticalThreshold)
                {
                    _criticalLowHandlers?.Invoke(this, currentLevel);
                }

                if (i < _dischargeSteps - 1) System.Threading.Thread.Sleep(300);
            }
        }
    }
}
