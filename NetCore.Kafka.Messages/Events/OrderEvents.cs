namespace NetCore.Kafka.Messages.Events
{
    public static class OrderEvents
    {
        public static string CreateOrder => "OrderCreated";
        public static string UpdateOrder => "OrderUpdated";
    }
}