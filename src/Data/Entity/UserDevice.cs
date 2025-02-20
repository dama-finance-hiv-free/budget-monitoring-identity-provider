using System;

namespace IdentityProvider.Data.Entity;

public class UserDevice : IIdentifiableEntity
{
    public string User { get; set; }
    public string Device { get; set; }
    public string LastIp { get; set; }
    public DateTime LastUsed { get; set; }
    public DateTime Expiration { get; set; }
}

public class UserLogin : IIdentifiableEntity
{
    public string Id { get; set; }
    public string User { get; set; }
    public string Device { get; set; }
    public string Address { get; set; }
    public DateTime Timestamp { get; set; }
}