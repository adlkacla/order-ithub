using OrderHub.Collections;
using OrderHub.Services;

public class Program
{
    public static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.InputEncoding = System.Text.Encoding.UTF8;

        var service = new OrderService();
        service.CreateOrder("Иванов", "Клавиатура", 5490m);
        service.CreateOrder("Петрова", "Монитор", 18990m);
        service.CreateOrder("Сидоров", "Мышь", 1290m);
        service.ProcessNext();
        service.ProcessNext();
        service.PrintAllOrders();
        service.PrintPendingQueue();
        service.PrintRecentActions();

        Console.WriteLine("\n\n\n");

        service.InitHistory(
            service.CreateOrder("Иванов", "Клавиатура", 5490m),
            service.CreateOrder("Петрова", "Монитор", 18990m));
        service.PlaceOrderAfterCurrent(service.CreateOrder("Иванов1", "Клавиа1тура", 5490m));
        service.PrintHistory();
        
        service.TryFindOrder("Иванов", out var found);
        Console.WriteLine($"Result: {found}");
        
        if(!service.TryFindOrder("Нет такого", out _))
            Console.WriteLine("PASS");


        Console.WriteLine("\n\n\n");
        var warehouse = new WarehouseService();
        warehouse.Receive("Клавиатура", 10);
        warehouse.Receive("Клавиатура", 5);
        warehouse.Receive("Монитор", 3);
        warehouse.Sell("Монитор", 1);
        warehouse.Sell("Монитор", 99);
        warehouse.Sell("Проектор", 1);
        warehouse.PrintStock();

        Console.WriteLine("\n\n\n");
        var catalog = new CatalogService();
        catalog.AddProduct("Клавиатура", 5490m, "Периферия");
        catalog.AddProduct("Монитор", 18990m, "Мониторы");
        catalog.AddProduct("Мышь", 1290m, "Периферия");
        catalog.PrintAll();
        catalog.PrintSortedByNaturalOrder();
        catalog.PrintSortedByName();
        catalog.PrintSortedByNameLength();

        Console.WriteLine("\n\n\n");
        var reports = new ReportService();
        var products = new List<Product>() { 
            new Product("Клавиатура",5490,"Периферия"),
            new Product("Монитор",18990,"Мониторы"),
            new Product("Мышь",1290,"Периферия")
        };
        const decimal threshold = 15000m;
        var list = reports.ExpensiveProducts(products, threshold);
        foreach (string line in list)
        Console.WriteLine(line);
        Console.WriteLine(reports.CountExpensive(products, threshold));
        Console.WriteLine(reports.HasVeryExpensive(products, 50000m));
        Console.WriteLine(reports.AllInStock(products));
        Console.WriteLine(reports.Cheapest(products));
        // ЗАДАЧА: измените порог в threshold на 15000m и запустите заново,
        // покажите, что изменилось только условие, а весь остальной код остался прежним

        Console.WriteLine("\n\n\n");
        products.Add(new Product("Наушники", 3490, "Аудио"));
        products.Add(new Product("Коврик", 590, "Периферия"));
        products.Add(new Product("Флешка", 990, "Накопители"));
        var summary = reports.SummarizeByCategory(products);
        foreach (var row in summary)
            Console.WriteLine($"{row.Category}: {row.Count} шт., средняя {row.AveragePrice:0.00} ₽");
        Console.WriteLine($"Самая дорогая категория: {reports.MostExpensiveCategory(products).Category}");
        foreach (string name in reports.CategoriesWithAtLeast(products, 2))
            Console.WriteLine(name);

        Console.WriteLine("\n\n\n");
        var customers = new List<Customer>() {
            new Customer(1, "Иванов", "Москва"),
            new Customer(2, "Петрова", "Казань"),
            new Customer(3, "Сидоров", "Тула"),
            new Customer(4, "Смирнов", "Омск")
        };
        var orderList = new List<Order>() {
            service.CreateOrder("Иванов", "Клавиатура", 5490m),
            service.CreateOrder("Иванов", "Мышь", 1290m),
            service.CreateOrder("Петрова", "Монитор", 18990m),
            service.CreateOrder("Сидоров", "Наушники", 3490m),
            service.CreateOrder("Сидоров", "Коврик", 590m),
            service.CreateOrder("Сидоров", "Веб-камера", 4990m),
            service.CreateOrder("Смирнов", "Флешка", 990m)
        };
        foreach (string line in reports.TopCustomers(customers, orderList, 3))
            Console.WriteLine(line);
        Console.WriteLine($"Выручка за 2026: {reports.RevenueByYear(orderList, 2026)}");
        Console.WriteLine($"Больше всех заказов: {reports.MostActiveCustomer(orderList)}");
        reports.PrintAboveAverage(orderList);

        Console.WriteLine("\n\n\n");
        // ЗАДАЧА 1: создайте переменную IEnumerable<Order> orders = OrderStream.Orders();
        // и СРАЗУ выведите строку «После вызова метода»
        // подсказка: строки трассировки из метода-итератора ещё не должны появиться
        IEnumerable<Order> orders = OrderStream.Orders();
        Console.WriteLine("После вызова метода");

        // ЗАДАЧА 2: переберите orders через foreach с Take(5) и выведите каждый заказ
        // подсказка: используйте orders.Take(5), а не полный foreach — иначе цикл не остановится
        foreach (var order in orders.Take(5))
            Console.WriteLine(order);

        // ЗАДАЧА 3: повторите демонстрацию с OrderStream.Expensive(10, 10000m)
        IEnumerable<Order> expensive = OrderStream.Expensive(10, 10000m);
        Console.WriteLine("После вызова метода");
        foreach (var order in expensive)
            Console.WriteLine(order);

        // ЗАДАЧА 4: выведите в консоль объяснение, почему Orders().ToList() без Take зависнет программу
        Console.WriteLine("Orders().ToList() без Take зависнет: в Orders() бесконечный while (true), а ToList() пытается забрать все элементы, поэтому перебор никогда не закончится");

        Console.WriteLine("\n\n\n");
        var buffer = new CircularBuffer<string>(3);

        // ЗАДАЧА 1: добавьте пять элементов подряд: "1", "2", "3", "4", "5"
        // подсказка: ёмкость 3, поэтому первые два будут перезаписаны
        buffer.Add("1");
        buffer.Add("2");
        buffer.Add("3");
        buffer.Add("4");
        buffer.Add("5");

        // ЗАДАЧА 2: выведите буфер через foreach
        // ожидается порядок от старого к новому: 3, 4, 5
        foreach (string item in buffer)
            Console.WriteLine(item);

        // ЗАДАЧА 3: выведите буфер через Reverse()
        // ожидается обратный порядок: 5, 4, 3
        foreach (string item in buffer.Reverse())
            Console.WriteLine(item);

        // ЗАДАЧА 4: выведите Count и IsFull
        Console.WriteLine($"Count: {buffer.Count}, IsFull: {buffer.IsFull}");

        // ЗАДАЧА 5: выведите содержимое через LINQ: buffer.Select(x => $"[{x}]")
        foreach (string item in buffer.Select(x => $"[{x}]"))
            Console.WriteLine(item);
    }
}