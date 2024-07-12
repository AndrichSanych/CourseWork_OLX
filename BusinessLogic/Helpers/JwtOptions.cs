
namespace BusinessLogic.Helpers
{
    public class JwtOptions
    {
        public string Issuer { get; set; } = string.Empty;
        public string Key { get; set; } = string.Empty;
        public int AccessTokenLifetimeInMinutes { get; set; }
    }
}
