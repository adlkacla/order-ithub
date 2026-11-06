﻿public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string City { get; set; } = "";

    // ЗАДАЧА: добавьте конструктор и ToString()
    public Customer(int id, string name, string city)
    {
        this.Id = id;
        this.Name = name;
        this.City = city;
    }

    public override string ToString() => $"#{Id} {Name} ({City})";
}
