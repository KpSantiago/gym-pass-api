using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymPass.Domain.Entities;

public class Role
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; } = default!;

    [StringLength(50)]
    [Required]
    public string Name { get; set; } = default!;

    public ICollection<UserRole> Users { get; set; } = default!;

    private Role(string name)
    {
        Name = name;
    }

    public static Role Create(string name)
    {
        Role role = new(name);

        return role;
    }
}
