using Domain.ValueObjects;

namespace DDDApplication.Application.Utils;

public class GetDistanceBetweenCordinatesUtil
{
    public static double GetDistance(Cordinate from, Cordinate to)
    {
        if (from.Latitude == to.Latitude && from.Longitude == to.Longitude)
        {
            return 0;
        }

        var fromRadian = Math.PI * from.Latitude / 180;
        var toRadian = Math.PI * to.Latitude / 180;

        var theta = from.Longitude - to.Longitude;
        var radTheta = Math.PI * theta / 180;

        var dist =
            Math.Sin(fromRadian) * Math.Sin(toRadian) +
            Math.Cos(fromRadian) * Math.Cos(toRadian) * Math.Cos(radTheta);

        if (dist > 1)
        {
            dist = 1;
        }

        dist = Math.Acos(dist);
        dist = dist * 180 / Math.PI;
        dist = dist * 60 * 1.1515;
        dist *= 1.609344;

        return dist;
    }
}
