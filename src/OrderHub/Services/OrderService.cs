using System.ComponentModel.DataAnnotations;

public class OrderService
{
    // ЗАДАЧА: объявите три коллекции с точной сигнатурой:
    // List<Order> allOrders — все заказы
    // Queue<Order> pendingQueue — заказы, ожидающие обработки,в порядке поступления
    // Stack<string> undoHistory — история последних действий для отмены
    // подсказка: для каждой коллекции создайте экземпляр прямо в поле,
    // например: private readonly Queue<Order> pendingQueue = new();

    List<Order> allOrders = new();
    Queue<Order> pendingQueue = new();
    Stack<string> undoHistory = new();

    LinkedList<Order> orderHistory = new();
    LinkedListNode<Order>? current = null;

    public Order CreateOrder(string customerName, string productName, decimal price)
    {
        // ЗАДАЧА 1: создайте новый заказ (Id счётчик увеличивается на 1)
        // ЗАДАЧА 2: добавьте заказ в allOrders
        // ЗАДАЧА 3: добавьте тот же заказ в pendingQueue методом Enqueue
        // ЗАДАЧА 4: запишите в undoHistory описание действия для возможной отмены
        // подсказка: описание удобно собрать так:
        // undoHistory.Push($"Создан заказ {order.Id} для{ customerName}");
        // ЗАДАЧА 5: верните созданный заказ
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
        // ЗАДАЧА 1: если pendingQueue пуст — верните null
        // подсказка: проверка emptiness — pendingQueue.Count == 0
        // ЗАДАЧА 2: извлеките заказ из очереди методом Dequeue (именно он, а не Peek)
        // ЗАДАЧА 3: запишите в undoHistory, какой заказ обработан
        // ЗАДАЧА 4: выведите в консоль $"Обработан заказ: {order}"
        // ЗАДАЧА 5: верните обработанный заказ

        if (pendingQueue.Count == 0 ) return null;

        Order order = pendingQueue.Dequeue();
        string process = $"Обработан заказ {order.Id}";
        undoHistory.Push(process);
        Console.WriteLine(process);
        
        return order;
    }

    public bool UndoLastAction()
    {
        // ЗАДАЧА 1: если undoHistory пуст — верните false
        // ЗАДАЧА 2: извлеките последнее действие методом Pop
        // ЗАДАЧА 3: выведите $"Отменено действие: {action}" и верните true
        if (undoHistory==null) return false;
        
        string action = undoHistory.Pop();
        Console.WriteLine($"Отменено действие: \"{action}\"");
        
        return true;
    }

    #region PRINT
    // ЗАДАЧА: допишите три метода-отчёта, используя foreach:
    // PrintAllOrders() — все заказы из allOrders по порядку добавления
    // PrintPendingQueue() — сколько заказов ждёт и какой первый в очереди(Peek, без извлечения)
    // PrintRecentActions() — содержимое undoHistory (обход идёт от последнего к первому)
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
        // ЗАДАЧА: переберите историю через foreach и выведите каждый заказ
        // ЗАДАЧА: отдельно выведите текущий заказ через current?.Value

        foreach (Order val in orderHistory)
        {
            Console.WriteLine(val.ToString());
        }
    }
    #endregion

    // ... поля и методы этапа 1 без изменений ...
    // ЗАДАЧА: объявите поле истории и узел текущего заказа:
    // LinkedList<Order> orderHistory
    // LinkedListNode<Order>? current
    // подсказка: узел nullable, поэтому тип поля с вопросительным

    public void InitHistory(params Order[] orders)
    {
        // ЗАДАЧА 1: очистите историю (orderHistory.Clear())
        // ЗАДАЧА 2: добавьте заказы по очереди методом AddLast
        // ЗАДАЧА 3: сохраните в current узел последнего добавленного заказа
        // подсказка: AddLast возвращает узел — сохраняйте возвращённое значение,
        // тогда не придётся искать его через Find
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
        // ЗАДАЧА 1: добавьте новый заказ в историю через AddAfter(current, order)
        // подсказка: если current == null, сначала вызовите InitHistory или
        // сделайте AddLast и запомните узел — иначе AddAfter бросит ArgumentNullException
        // ЗАДАЧА 2: переставьте current на новый узел
        // ЗАДАЧА 3: верните true

        if (current==null)
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
        // ЗАДАЧА: найдите узел через Find, проверьте его на null
        // подсказка: LINQ-расширение FirstOrDefault тоже подойдёт,но тема требует именно Find
        // ЗАДАЧА: если узел найден — запишите order = node.Value и верните true, иначе false


        order = allOrders.Find(ord => ord.CustomerName==customerName);
        return order == null ? false:true ;
    }
}