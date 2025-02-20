namespace IdentityProvider.Data.Entity
{
    public class SecurityQuestion : IIdentifiableEntity
    {
        public string Code { get; set; }
        public string Locale { get; set; }
        public string Description { get; set; }
    }
}