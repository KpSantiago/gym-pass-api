using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymPass.Domain.Entities;

public class User
{
    [Key]
    [StringLength(64)]
    public string Id { get; set; } = default!;

    [StringLength(255)]
    [Required]
    public string Name { get; set; } = default!;

    [StringLength(255)]
    [EmailAddress]
    [Required]
    public string Email { get; set; } = default!;

    [Required]
    public string PasswordHash { get; set; } = default!;

    [Required]
    public DateTime CreatedAt { get; set; } = default!;
    
    public ICollection<UserRole> Roles { get; set; } = default!;

    public ICollection<CheckIn> CheckIns { get; set; } = default!;

    private User(string? id, string name, string email, string passwordHash, DateTime createdAt)
    {
        Id = id ?? Guid.NewGuid().ToString();
        Name = name;
        Email = email;
        PasswordHash = passwordHash;
        CreatedAt = createdAt;
    }

    public static User Create(string? id, string name, string email, string password, DateTime? createdAt)
    {
        User user = new(id, name, email, password, createdAt ?? DateTime.UtcNow);

        return user;
    }
}
