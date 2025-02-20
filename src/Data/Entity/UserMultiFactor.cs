namespace IdentityProvider.Data.Entity
{
    public class UserMultiFactor : IIdentifiableEntity
    {

        public string User { get; set; }
        public string MultiFactorType { get; set; }
    }
}