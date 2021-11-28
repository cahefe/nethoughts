namespace Corp.System.Hexagonal.Orders.Domain.Model
{
    public class ClientInfo
    {
        public int IDBroker { get; set; }
        public long IDClient { get; set; }
        public bool IsClientDocument { get; set; }
    }
}
