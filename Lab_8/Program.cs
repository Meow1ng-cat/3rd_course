using Lab_8;

var monitor = new BatteryMonitor(dischargeSteps: 12);

var hud = new ConsoleHud();
var stats = new LowLevelStats();

monitor.LevelChanged += hud.OnLevelChanged;
monitor.LevelChanged += stats.OnLevelChanged;
monitor.CriticalLowReached += hud.OnCriticalLowReached;

Console.WriteLine("Запуск мониторинга батареи...\n");

monitor.Start();

monitor.CriticalLowReached -= hud.OnCriticalLowReached;

stats.Report();

Console.WriteLine("\nМониторинг завершен.");