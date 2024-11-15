using System.ComponentModel.DataAnnotations;
using Domain.ValueObjects;

namespace Domain.Entities;
public class Gym
{
    [Key]
    [StringLength(64)]
    [Required]
    public string Id { get; set; } = default!;

    [StringLength(255)]
    [Required]
    public string Title { get; set; } = default!;

    [StringLength(500)]
    public string Description { get; set; } = default!;

    public string Phone { get; set; } = default!;

    public Cordinate Cordinate { get; set; } = default!;

<<<<<<< Updated upstream
    [Required]
    public DateTime CreatedAt { get; set; } = default!;

=======
    public ICollection<CheckIn> CheckIns { get; set; } = default!;

    [Required]
    public DateTime CreatedAt { get; set; } = default!;


    private Gym() { }

>>>>>>> Stashed changes
    private Gym(string? id, Cordinate cordinate, DateTime createdAt, string title, string description, string phone)
    {
        Id = id ?? Guid.NewGuid().ToString();
        Title = title;
        Description = description;
        Phone = phone;
        Cordinate = cordinate;
        CreatedAt = createdAt;
    }

    public static Gym Create(string? id, Cordinate cordinate, DateTime? createdAt, string title, string description = "", string phone = "")
    {
        Gym gym = new(id, cordinate, createdAt ?? DateTime.UtcNow, title, description, phone);

        return gym;
    }
}