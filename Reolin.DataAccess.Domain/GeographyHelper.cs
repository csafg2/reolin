using System.Data.Entity.Spatial;

namespace Reolin.DataAccess.Domain
{
    public static class GeographyHelper
    {
        public static DbGeography FromLongitudeLatitude(double longitude, double latitude, int srid = Address.LocationSRID)
        {
            return DbGeography.FromText($"POINT({longitude} {latitude})", srid);
        }
    }
}