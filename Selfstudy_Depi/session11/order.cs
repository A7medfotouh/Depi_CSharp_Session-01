namespace assignment11;

public class Order
{
    public int Id { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}