namespace IdentityProvider.Protectimus.Enums
{
    public enum TokenBlockType
    {
        NONE_BLOCKED,
        BLOCKED_BY_ADMIN,
        TOO_MANY_OTP_FAILED_ATTEMPTS_BLOCKED,
        TOO_MANY_OTP_FAILED_SYNCHRONIZATION_ATTEMPTS_BLOCKED
    }
}