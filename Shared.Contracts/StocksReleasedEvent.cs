namespace Shared.Contracts
{
    public class StocksReleasedEvent
    {
        public int OrderId { get; set; }
        public string Reason { get; set; }
    }
}