using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

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

    [StringLength(64)]
    [Required]
    public string PasswordHash { get; set; } = default!;

    [Required]
    public DateTime CreatedAt { get; set; } = default!;

    private User(string? id, string name, string email, string password, DateTime createdAt) {
        Id = id ?? Guid.NewGuid().ToString();
        Name = name;
        Email = email;
        PasswordHash = password;
        CreatedAt = createdAt;
     }

    public static User Create(string? id, string name, string email, string password, DateTime? createdAt)
    {
        User user = new(id, name, email, password, createdAt ?? DateTime.UtcNow);

        return user;
    }
}
