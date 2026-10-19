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
    }
}