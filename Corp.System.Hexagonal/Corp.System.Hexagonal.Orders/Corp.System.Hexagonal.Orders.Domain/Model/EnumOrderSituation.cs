namespace Corp.System.Hexagonal.Orders.Domain.Model
{
    public enum EnumOrderSituation : byte
    {
        Unknown = 0,
        Registered = 1,
        Approved = 2,
        Reproved = 3,
        Canceled = 4,
        Settled = 5
    }
}
