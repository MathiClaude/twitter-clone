namespace TwitterClone.Domain.Entities;

public class AuditableEntity
{
    protected AuditableEntity()
    {
        Id =  Guid.NewGuid();
    }
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; } = new DateTime();
    public DateTime? UpdatedAt { get; set; }
}