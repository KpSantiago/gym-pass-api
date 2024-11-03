using System.ComponentModel.DataAnnotations;

namespace Domain.ValueObjects;

// TODO: Fazer a conversão de ValueObject em propriedades
public class Cordinate
{
    [Required]
    public float Longitude { get; set; }

    [Required]
    public float Latitude { get; set; }

    public Cordinate(float latitude = 0, float longitude = 0)
    {
        Longitude = longitude;
        Latitude = latitude;
    }
}
