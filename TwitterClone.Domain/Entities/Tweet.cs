using System.ComponentModel.DataAnnotations;

namespace TwitterClone.Domain.Entities;

public class Tweet: AuditableEntity
{
    [MaxLength(280)]
    public string Content { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
}