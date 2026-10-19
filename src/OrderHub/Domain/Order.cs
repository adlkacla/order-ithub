public class Order
{
    // ЗАДАЧА: добавьте конструктор с параметрами Id, CustomerName, ProductName, Price, CreatedAt
    // ЗАДАЧА: объявите свойства только для чтения: Id, CustomerName, ProductName, Price, CreatedAt
    // подсказка: DateTime создаётся вызовом DateTime.Now, тип decimal — для денежных сумм
    // ЗАДАЧА: переопределите ToString() так, чтобы вывод был вида
    // "#12 Иванов — Клавиатура 5 490,00 ₽ (12.03.2026)"
    // подсказка: используйте интерполяцию $"...", форматирование даты — d.ToString("dd.MM.yyyy")
	public int Id {get;}
    public string CustomerName {get;}
    public string ProductName {get;}
    public decimal Price {get;}
    public DateTime CreatedAt {get;}
	
	public Order(int Id, string CustomerName, string ProductName, decimal Price, DateTime CreatedAt){
		this.Id = Id;
		this.CustomerName = CustomerName;
		this.ProductName = ProductName;
		this.Price = Price;
		this.CreatedAt = CreatedAt;
	}

	public override string ToString() =>
		$"#{Id} {CustomerName} - {ProductName} {Price} ({CreatedAt.ToString("dd.MM.yyyy")})";
	
	
}