using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GymPass.Domain.Entities;

public class Role
{
    [Key]
    [StringLength(64)]
    public string Id { get; set; } = default!;

    [StringLength(50)]
    [Required]
    public string Name { get; set; } = default!;

    public ICollection<UserRole> Users { get; set; } = default!;

    private Role(string? id, string name)
    {
        Id = id ?? Guid.NewGuid().ToString();
        Name = name;
    }

    public static Role Create(string? id, string name)
    {
        Role role = new(id, name);

        return role;
    }
}
