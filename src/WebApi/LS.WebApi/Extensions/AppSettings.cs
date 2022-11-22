namespace LS.WebApi.Extensions
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public int Expiration { get; set; }
        public string Issuer { get; set; }
        public string ValidAt { get; set; }
    }
}
