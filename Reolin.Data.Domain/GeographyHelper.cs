using Reolin.Domain;
using System.Data.Entity.Spatial;

namespace System
{
    public static class GeographyHelper
    {
        public static DbGeography FromLongitudeLatitude(double longitude, double latitude, int srid = Address.Geo_SRID)
        {
            return DbGeography.FromText($"POINT({longitude} {latitude})", srid);
        }
    }
}