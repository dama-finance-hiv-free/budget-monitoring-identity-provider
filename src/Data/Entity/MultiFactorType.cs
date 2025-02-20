namespace IdentityProvider.Data.Entity
{
    public class MultiFactorType: IIdentifiableEntity
    {
        public string Code { get; set; }
        public string Locale { get; set; }
        public string Description { get; set; }
        public bool UserEnabled { get; set; }
    }
}