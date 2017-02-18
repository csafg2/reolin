using Microsoft.AspNetCore.Mvc;

namespace Reolin.Web.Controllers
{
    public class Point
    {
        public double Latitiude { get; set; }
        public double Longitude { get; set; }
    }

    public class RouteModel
    {
        public Point Source { get; set; }
        public Point Destination { get; set; }
    }

    public class RouteController : Controller
    {
        [Route("/{sourceLat}/and/{sourceLong}/to/{destinationLat}/and/{DestinationLong}")]
        public ActionResult Route(double sourceLat, double sourceLong, double destinationLat, double destinationLong)
        {
            var source = new Point() { Latitiude = sourceLat, Longitude = sourceLong };
            var destination = new Point() { Latitiude = destinationLat, Longitude = destinationLong };

            return View(new RouteModel() { Destination = destination, Source = source });
        }
    }
}
