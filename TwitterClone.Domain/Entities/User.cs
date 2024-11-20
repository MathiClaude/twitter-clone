using System.ComponentModel.DataAnnotations;

namespace TwitterClone.Domain.Entities;

public class User: AuditableEntity
{
    
    public string Name { get; set; }
    public string UserName { get; set; } 
    public string Email { get; set; }
    public string Password { get; set; }
    public List<User> Followers { get; set; } = [];
    public List<User> Following { get; set; } = [];
}