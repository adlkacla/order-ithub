﻿using System.Collections.Generic;
namespace OrderHub.Services;

public class OrderStream
{
    static string[] customers = { "Иванов", "Петрова", "Сидоров" };
    static string[] products = { "Клавиатура", "Монитор", "Мышь" };

    // ЗАДАЧА 1: реализуйте бесконечный генератор заказов
    // public static IEnumerable<Order> Orders()
    // подсказка по алгоритму: счётчик Id растёт на 1, клиенты берутся по кругу из массива,
    // цена тоже увеличивается; обязательно while (true)
    // ЗАДАЧА 2: перед каждым yield return добавьте Console.WriteLine с текстом
    // $"Готовлю заказ №{n}" — так вы увидите момент фактического выполнения
    public static IEnumerable<Order> Orders()
    {
        int n = 1;
        decimal price = 1000m;
        while (true)
        {
            Console.WriteLine($"Готовлю заказ №{n}");
            yield return new Order(n, customers[(n - 1) % customers.Length], products[(n - 1) % products.Length], price, DateTime.Now);
            n++;
            price += 1000m;
        }
    }

    // ЗАДАЧА 3: реализуйте конечный итератор с фильтром
    // public static IEnumerable<Order> Expensive(int limit, decimal threshold)
    // подсказка: обычный for, внутри проверка условия и continue
    // при недостижении порога пропускайте значение — yield return не вызывается
    public static IEnumerable<Order> Expensive(int limit, decimal threshold)
    {
        for (int n = 1; n <= limit; n++)
        {
            decimal price = n * 2500m;
            if (price < threshold) continue;

            Console.WriteLine($"Готовлю заказ №{n}");
            yield return new Order(n, customers[(n - 1) % customers.Length], products[(n - 1) % products.Length], price, DateTime.Now);
        }
    }
}
