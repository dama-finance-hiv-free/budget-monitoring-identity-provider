namespace IdentityProvider.Data.Entity
{
    public class UserSecurityQuestion : IIdentifiableEntity
    {
        public string User { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
    }
}