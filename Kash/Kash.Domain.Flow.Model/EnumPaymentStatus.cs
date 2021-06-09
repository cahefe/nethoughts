namespace Kash.Domain.Flow.Model
{
    public enum EnumPaymentStatus : byte
    {
        Undefined = 0,
        Pending = 1,
        Scheduled = 2,
        Payed = 3,
        NotPayed = 4
    }
}
