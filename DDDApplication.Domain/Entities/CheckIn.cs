using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class CheckIn
{
    public string Id { get; set; } = default!;

    [ForeignKey("User")]
    [Required]
    public string UserId { get; set; } = default!;
    public User User { get; set; } = default!;

    [ForeignKey("Gym")]
    [Required]
    public string GymId { get; set; } = default!;
    public Gym Gym { get; set; } = default!;

    public DateTime? ValidatedAt { get; set; } = default!;

    public DateTime CreatedAt { get; set; } = default!;

    private CheckIn(string? id, string userId, string gymId, DateTime? validatedAt, DateTime createdAt)
    {
        Id = id ?? Guid.NewGuid().ToString();
        UserId = userId;
        GymId = gymId;
        ValidatedAt = validatedAt;
        CreatedAt = createdAt;
    }

    public static CheckIn Create(string? id, string userId, string gymId, DateTime? validatedAt, DateTime? createdAt)
    {
        CheckIn checkIn = new(id, userId, gymId, validatedAt, createdAt ?? DateTime.UtcNow);

        return checkIn;
    }
}
