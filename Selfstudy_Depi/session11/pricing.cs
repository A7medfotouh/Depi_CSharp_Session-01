using assignment11;

namespace Section02;

// Part 1: User-defined delegate
public delegate decimal PriceCalculator(Order order);


public static class OrderPricing
{
    private const decimal DiscountAmount = 10m;

    // Price × Quantity
    public static decimal CalculateTotal(Order order)
    {
        return order.Price * order.Quantity;
    }

    // Price × Quantity - discount 
    public static decimal CalculateTotalWithDiscount(Order order)
    {
        return Math.Max(0m, order.Price * order.Quantity - DiscountAmount);
    }

    public static decimal CalculateOrderPrice(Order order, PriceCalculator calculator)
    {
        return calculator(order);
    }
}


public static class OrderPricingFunc
{
    public static decimal CalculateOrderPrice(Order order, Func<Order, decimal> calculator)
    {
        return calculator(order);
    }
}