using System.ComponentModel.DataAnnotations;

namespace Domain.ValueObjects;

// TODO: Fazer a conversão de ValueObject em propriedades
public class Cordinate
{
    [Required]
<<<<<<< Updated upstream
    public float Longitude { get; set; }

    [Required]
    public float Latitude { get; set; }

    public Cordinate(float latitude = 0, float longitude = 0)
=======
    public double Longitude { get; set; }

    [Required]
    public double Latitude { get; set; }

    public Cordinate(double latitude = 0, double longitude = 0)
>>>>>>> Stashed changes
    {
        Longitude = longitude;
        Latitude = latitude;
    }
}
