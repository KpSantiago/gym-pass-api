using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymPass.Domain.Entities;

public class UserRole
{
    [Key]
    [StringLength(64)]
    public string Id { get; set; } = default!;

    [StringLength(64)]
    [Required]
    public string UserId { get; set; } = default!;

    [ForeignKey("UserId")]
    public User User { get; set; } = default!;

    [StringLength(64)]
    [Required]
    public string RoleId { get; set; } = default!;

    [ForeignKey("RoleId")]
    public Role Role { get; set; } = default!;

    private UserRole(string? id, string userId, string roleId)
    {
        Id = id ?? Guid.NewGuid().ToString();
        UserId = userId;
        RoleId = roleId;
    }

    public static UserRole Create(string? id, string userId, string roleId)
    {
        UserRole userRole = new(id, userId, roleId);

        return userRole;
    }
}
