using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.AccessControl;
using Lab_9;

public enum Difficulty
{
    Trivial = 0,
    Easy = 1,
    Normal = 2,
    Hard = 3,
    Nightmare = 4
}

class Program
{
    static void Main()
    {
        // Создаем цели
        var obj1 = new Objective("kill_wolves", "Убить 5 волков", 5);
        var obj2 = new Objective("gather_herbs", "Собрать 10 трав", 10);

        // Создаем квесты
        var quest1 = new Quest("q1", "Охота на волков", Difficulty.Normal, new[] { obj1 });
        var quest2 = new Quest("q2", "Сбор трав", Difficulty.Easy, new[] { obj2 });
        var quest3 = new Quest("q3", "Эпический квест", Difficulty.Nightmare, Array.Empty<Objective>());

        // Журнал квестов
        var log = new QuestLog();
        log.Add(quest1);
        log.Add(quest2);
        log.Add(quest3);

        Console.WriteLine($"Всего квестов: {log.Count}");

        // Доступ по индексу
        Console.WriteLine($"\nПервый квест: {log[0]}");

        // Доступ по Id
        Console.WriteLine($"Квест q2: {log["q2"]}");

        // Ленивая фильтрация по сложности
        Console.WriteLine("\nКвесты сложностью Normal+:");
        foreach (var quest in log.EnumerateByDifficulty(Difficulty.Normal))
        {
            Console.WriteLine($"  - {quest}");
        }

        // Удаление
        Console.WriteLine($"\nУдаляем q2: {log.RemoveById("q2")}");
        Console.WriteLine($"Удаляем по индексу 0: {log.RemoveAt(0)}");
        Console.WriteLine($"Осталось квестов: {log.Count}");
    }
}