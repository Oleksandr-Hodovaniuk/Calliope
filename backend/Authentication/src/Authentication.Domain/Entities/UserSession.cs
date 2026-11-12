using System.Net;

namespace Authentication.Domain.Entities;

public class UserSession : BaseEntity
{
    public Guid UserId { get; set; }
    public User User { get; set; } = default!;
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset LastSeenAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset ExpiresAt { get; set; }
    public DateTimeOffset? RevokedAt { get; set; }
    public IPAddress? IpAddress { get; set; }
    public string? UserAgent { get; set; }
    public ICollection<RefreshToken> RefreshTokens { get; set; } = [];
}
