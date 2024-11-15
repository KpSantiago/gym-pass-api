using System.ComponentModel.DataAnnotations;

namespace GymPass.Domain.ValueObjects;

// TODO: Fazer a conversão de ValueObject em propriedades
public class Cordinate
{
    [Required]
    public double Longitude { get; set; }

    [Required]
    public double Latitude { get; set; }

    public Cordinate(double latitude = 0, double longitude = 0)
    {
        Longitude = longitude;
        Latitude = latitude;
    }
}
