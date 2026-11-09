namespace Catalog.Domain.Entities;

public class BaseEntity
{
    public Guid Id { get; private set; } = Guid.CreateVersion7(); 
}
