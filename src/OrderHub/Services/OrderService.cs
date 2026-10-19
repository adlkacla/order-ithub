using System.ComponentModel.DataAnnotations;

public class OrderService
{
    List<Order> allOrders = new();
    Queue<Order> pendingQueue = new();
    Stack<string> undoHistory = new();

    public Order CreateOrder(string customerName, string productName, decimal price)
    {
        int id = 0;
        if (allOrders.Count >= 1)
        {
            id = allOrders.Last<Order>().Id+1;
        }
        Order newOrder = new(id,customerName,productName,price,DateTime.Now);

        allOrders.Add(newOrder);
        pendingQueue.Enqueue(newOrder);
        undoHistory.Push($"Создан заказ {newOrder.Id} для {newOrder.CustomerName}");
        
        return newOrder;
    }

    public Order? ProcessNext()
    {
        if(pendingQueue.Count == 0 ) return null;

        Order order = pendingQueue.Dequeue();
        string process = $"Обработан заказ {order.Id}";
        undoHistory.Push(process);
        Console.WriteLine(process);
        
        return order;
    }

    public bool UndoLastAction()
    {
        if(undoHistory==null) return false;
        
        string action = undoHistory.Pop();
        Console.WriteLine($"Отменено действие: \"{action}\"");
        
        return true;
    }

    public void PrintAllOrders()
    {
        foreach(Order order in allOrders)
        {
            Console.WriteLine(order.ToString());
        }
    }

    public void PrintPendingQueue()
    {
        Console.Write($"Count: {pendingQueue.Count}");
        if(pendingQueue.Count > 0 )
            Console.WriteLine($", first: {pendingQueue.Peek().Id}");
    }

    public void PrintRecentActions()
    {
        foreach(string act in undoHistory)
        {
            Console.WriteLine(act);
        }
    }

}