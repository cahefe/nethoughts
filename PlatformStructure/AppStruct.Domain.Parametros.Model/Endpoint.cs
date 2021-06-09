namespace AppStruct.Domain.Parametros.Model
{
    public class Endpoint
    {
        public long ID { get; set; }
        public string servidor { get; set; }
        public int port { get; set; }
        public string url { get; set; }
        public bool Fornecedor { get; set; }
    }
}
