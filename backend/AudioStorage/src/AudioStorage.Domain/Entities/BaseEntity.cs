namespace AudioStorage.Domain.Entities;

public class BaseEntity
{
    public Guid Id { get; set; } = Guid.CreateVersion7();
}
