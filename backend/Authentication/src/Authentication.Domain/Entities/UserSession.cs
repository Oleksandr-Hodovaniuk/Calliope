namespace Authentication.Domain.Entities;

public class UserSession : BaseEntity
{
    public Guid UserId { get; set; }
    public User User { get; set; } = default!;
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset lastSeenAt { get; set; }
    public DateTimeOffset ExpiresdAt { get; set; }
    public DateTimeOffset? RevokedAt { get; set; }
    public string? IpAddress { get; set; }
    public string? UserAgent { get; set; }
    public ICollection<RefreshToken> RefreshTokens { get; set; } = [];
}
