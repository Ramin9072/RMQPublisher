namespace RMQPublisher.Core.Domain
{
    // Simple example for bank transaction
    public class Payment
    {
        public string RowNum { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CardNumber { get; set; }
        public double Value { get; set; }
    }
}