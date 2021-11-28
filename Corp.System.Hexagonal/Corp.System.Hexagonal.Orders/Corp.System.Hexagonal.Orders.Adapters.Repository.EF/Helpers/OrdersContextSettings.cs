namespace Corp.System.Hexagonal.Orders.Adapters.Repository.EF.Helpers
{
    public class OrdersContextSettings
    {
        public EnumRepoType RepoType { get; set; }
        public string DBName { get; set; }
    }
}
