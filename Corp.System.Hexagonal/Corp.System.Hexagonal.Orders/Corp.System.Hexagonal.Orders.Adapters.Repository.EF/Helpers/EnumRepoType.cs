namespace Corp.System.Hexagonal.Orders.Adapters.Repository.EF.Helpers
{
    public enum EnumRepoType : byte
    {
        Unknown = 0,
        InMemory = 1,
        SQLite = 2,
        SQLDatabase = 3
    }
}
