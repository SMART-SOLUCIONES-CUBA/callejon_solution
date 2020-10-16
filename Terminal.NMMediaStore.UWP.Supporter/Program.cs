using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.WindowsServices;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.UWP.Supporter
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var isService = !(System.Diagnostics.Debugger.IsAttached || args.Contains("--console"));

            if (isService)
            {
                var pathToExe = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
                var pathToContentRoot = System.IO.Path.GetDirectoryName(pathToExe);

                System.IO.Directory.SetCurrentDirectory(pathToContentRoot);
            }

            var builder = CreateWebHostBuilder(args.Where(arg => arg != "--console").ToArray());

            var host = builder.Build();

            if (isService)
            {
                // To run the app without the CustomWebHostService change the
                // next line to host.RunAsService();
                host.RunAsService();
            }
            else
            {
                host.Run();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureLogging((hostingContext, logging) =>
                {
                    logging.AddEventLog();
                })
                .ConfigureAppConfiguration((context, config) =>
                {
                    // Configure the app here.
                })
                .ConfigureKestrel((context, serverOptions) =>
                {
                    serverOptions.Listen(System.Net.IPAddress.Loopback, 5666);
                })
                .UseStartup<Startup>();
    }
}