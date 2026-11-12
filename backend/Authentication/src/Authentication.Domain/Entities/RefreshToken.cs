namespace Authentication.Domain.Entities;

public class RefreshToken : BaseEntity
{
    public Guid SessionId { get; set; }
    public UserSession Session { get; set; } = default!;
    public string TokenHash { get; set; } = default!;
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset ExpiresAt { get; set; }
    public DateTimeOffset? UsedAt { get; set; }
    public DateTimeOffset? RevokedAt { get; set; }
}
