namespace Authentication.Domain.Entities;

public class User : BaseEntity
{
    public string Email { get; set; } = default!;
    public string NormalizedEmail { get; set; } = default!;
    public string PasswordHash { get; set; } = default!;
    public Guid RoleId { get; set; }
    public Role Role { get; set; } = default!;
    public bool EmailConfirmed { get; set; } = false;
    public bool IsActive { get; set; } = true;
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? UpdatedAt { get; set; }
    public ICollection<UserSession> Sessions { get; set; } = [];
}
