using System.Data.Entity.Spatial;

namespace System
{
    public static class GeoHelpers
    {
        public const double MAX_LONGITUDE = 180;
        public const double MIN_LONGITUDE = -180;

        public const double MAX_LATITUDE = 90;
        public const double MIN_LATITUDE = -90;

        public static DbGeography FromLongitudeLatitude(double longitude, double latitude, int srid)
        {
            
            return DbGeography.FromText($"POINT({longitude} {latitude})", srid);
        }

        public static bool IsValid(this DbGeography source)
        {
            return ((double)source.Latitude).IsValideLatitude() && ((double)source.Longitude).IsValidLongitude();
        }
        
        public static bool IsValidLongitude(this double longitude)
        {
            return longitude <= MAX_LONGITUDE && longitude > MIN_LONGITUDE;
        }

        public static bool IsValideLatitude(this double latitude)
        {
            return latitude <= MAX_LATITUDE && latitude > MIN_LATITUDE;
        }
    }
}
