namespace infnet_bl6_daw_at.Domain.Entities
{
    public class Token
    {
        public string BearerToken { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
