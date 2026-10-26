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
        var reports = new ReportService();
        var products = new List<Product>() { 
            new Product("a",11,"none"),
            new Product("b",12,"none"),
            new Product("c",13,"none")
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
    }
}