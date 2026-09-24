namespace assignment11program;

class Program
{
    // القاعدة الكاملة للتحقق من الأوردر (بتجمع 3 شروط)
    static readonly Predicate<Order> IsValidOrder = order =>
        order.Quantity > 0 &&
        order.Price > 0 &&
        !string.IsNullOrWhiteSpace(order.CustomerName);

    static void Main(string[] args)
    {
        Order order = new Order { Id = 1, CustomerName = "Ahmed", Price = 50m, Quantity = 3 };

        // ---------------- Part 1: User-defined delegate ----------------
        Console.WriteLine("===== Part 1: User-defined delegate =====");
        Console.WriteLine("Total              : " +
            OrderPricing.CalculateOrderPrice(order, OrderPricing.CalculateTotal));               // 150
        Console.WriteLine("Total with discount: " +
            OrderPricing.CalculateOrderPrice(order, OrderPricing.CalculateTotalWithDiscount));   // 140

        // ---------------- Part 2: Func<> ----------------
        Console.WriteLine("\n===== Part 2: Func<> =====");
        Console.WriteLine("Total              : " +
            OrderPricingFunc.CalculateOrderPrice(order, x => x.Price * x.Quantity));             // 150
        Console.WriteLine("Total (10% off)    : " +
            OrderPricingFunc.CalculateOrderPrice(order, x => x.Price * x.Quantity * 0.9m));      // 135

        // ---------------- Part 3: Predicate<> ----------------
        Console.WriteLine("\n===== Part 3: Predicate<> =====");
        Console.WriteLine("Quantity > 0      : " + OrderValidator.ValidateOrder(order, o => o.Quantity > 0));
        Console.WriteLine("Price > 0         : " + OrderValidator.ValidateOrder(order, o => o.Price > 0));
        Console.WriteLine("Has customer name : " + OrderValidator.ValidateOrder(order, o => !string.IsNullOrWhiteSpace(o.CustomerName)));

        Order badOrder = new Order { Id = 2, CustomerName = "", Price = 0m, Quantity = 0 };
        Console.WriteLine("Bad order is valid: " + OrderValidator.ValidateOrder(badOrder, IsValidOrder));   // False

        // ---------------- Part 4: Action<> ----------------
        Console.WriteLine("\n===== Part 4: Action<> =====");
        Action<Order> printOrder = o => Console.WriteLine($"Order {o.Id} processed. Customer: {o.CustomerName}, Total: {o.Price * o.Quantity:F2}");
        Action<Order> sendConfirmation = o => Console.WriteLine($"Confirmation message sent to {o.CustomerName}.");
        Action<Order> writeAudit = o => Console.WriteLine($"AUDIT: order {o.Id} was processed.");

        // نفس الدالة ProcessOrder ومن غير أي تعديل فيها، بس بنبعتلها سلوك مختلف كل مرة
        OrderProcessor.ProcessOrder(order, printOrder);
        OrderProcessor.ProcessOrder(order, sendConfirmation);
        OrderProcessor.ProcessOrder(order, writeAudit);

        // Multicast delegate: أكتر من Action في delegate واحد
        Action<Order> allActions = printOrder;
        allActions += sendConfirmation;
        allActions += writeAudit;
        Console.WriteLine("-- multicast --");
        OrderProcessor.ProcessOrder(order, allActions);

        // ---------------- Part 5 + 6: Events, Subscribe / Unsubscribe ----------------
        Console.WriteLine("\n===== Part 5 & 6: Events =====");
        OrderService orderService = new OrderService();
        orderService.OrderProcessed += OrderHandlers.Handler1;
        orderService.OrderProcessed += OrderHandlers.Handler2;

        Console.WriteLine("-- with Handler1 & Handler2 --");
        orderService.ProcessOrder(order);

        orderService.OrderProcessed -= OrderHandlers.Handler1;
        Console.WriteLine("-- after unsubscribing Handler1 --");
        orderService.ProcessOrder(order);   // Handler2 بس اللي هيشتغل

        // ---------------- Part 7: Final Application ----------------
        Console.WriteLine("\n===== Part 7: Final flow =====");
        OrderService finalService = new OrderService();
        finalService.OrderProcessed += OrderHandlers.Handler1;
        finalService.OrderProcessed += OrderHandlers.Handler2;
        finalService.OrderProcessed += OrderHandlers.Handler3;

        Console.WriteLine("--- Valid order ---");
        RunOrderFlow(order, finalService);

        Console.WriteLine("\n--- Invalid order ---");
        RunOrderFlow(badOrder, finalService);
    }

    // Order -> Validate -> Calculate Price -> Process -> OrderProcessed -> Handlers
    static void RunOrderFlow(Order order, OrderService service)
    {
        Console.WriteLine($"Validating order {order.Id}...");
        if (!OrderValidator.ValidateOrder(order, IsValidOrder))
        {
            Console.WriteLine("Order is invalid. Flow stopped.");
            return;
        }

        decimal total = OrderPricingFunc.CalculateOrderPrice(order, o => o.Price * o.Quantity);
        Console.WriteLine($"Total price: {total:F2}");

        service.ProcessOrder(order);   // بيرفع الـ event وبينفذ كل الـ Handlers
    }
}