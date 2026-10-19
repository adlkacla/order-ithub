using System.ComponentModel.DataAnnotations;

public class OrderService
{
    List<Order> allOrders = new();
    Queue<Order> pendingQueue = new();
    Stack<string> undoHistory = new();

    LinkedList<Order> orderHistory = new();
    LinkedListNode<Order>? current = null;

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

    #region PRINT
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

    public void PrintHistory()
    {
        foreach(Order val in orderHistory)
        {
            Console.WriteLine(val.ToString());
        }
    }
    #endregion

    public void InitHistory(params Order[] orders)
    {
        orderHistory.Clear();
        foreach(var order in orders)
        {
            orderHistory.AddLast(order); 
            //не сохранять, слишком много операций записи в память может быть
        }
        current = orderHistory.Last;
    }

    public bool PlaceOrderAfterCurrent(Order order)
    {
        if(current==null)
        {
            orderHistory.AddLast(order);
            current = orderHistory.Last;
            return true;
        }
        current = orderHistory.AddAfter(current, order);
        return true;
    }

    public bool TryFindOrder(string customerName, out Order? order)
    {
        order = allOrders.Find(ord => ord.CustomerName==customerName);
        return order == null ? false:true ;
    }
}