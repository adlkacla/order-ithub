public class Order
{
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