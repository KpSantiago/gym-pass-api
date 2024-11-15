using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

<<<<<<< Updated upstream
    [StringLength(64)]
=======
>>>>>>> Stashed changes
    [Required]
    public string PasswordHash { get; set; } = default!;

    [Required]
    public DateTime CreatedAt { get; set; } = default!;

<<<<<<< Updated upstream
    public ICollection<Role> Role { get; set; } = default!;

    private User(string? id, string name, string email, string password, DateTime createdAt)
=======
    public ICollection<UserRole> Roles { get; set; } = default!;

    public ICollection<CheckIn> CheckIns { get; set; } = default!;

    private User(string? id, string name, string email, string passwordHash, DateTime createdAt)
>>>>>>> Stashed changes
    {
        Id = id ?? Guid.NewGuid().ToString();
        Name = name;
        Email = email;
<<<<<<< Updated upstream
        PasswordHash = password;
=======
        PasswordHash = passwordHash;
>>>>>>> Stashed changes
        CreatedAt = createdAt;
    }

    public static User Create(string? id, string name, string email, string password, DateTime? createdAt)
    {
        User user = new(id, name, email, password, createdAt ?? DateTime.UtcNow);

        return user;
    }
}
