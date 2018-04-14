using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using System.Net;
using System.Threading.Tasks;

namespace Reolin.Web.Api
{
#pragma warning disable CS1591
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                   .UseKestrel(o => o.Listen(IPAddress.Any, 443,
                            op => op.UseHttps("certificate_combined.pfx", "Hassan@1")))
                //.UseUrls("http://*:80")
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseSetting("detailedErrors", "true")
                //.UseIISIntegration()
                .UseStartup<Startup>()
                .CaptureStartupErrors(true)
                .Build();

            host.Run();


        }
    }
#pragma warning restore CS1591
}
