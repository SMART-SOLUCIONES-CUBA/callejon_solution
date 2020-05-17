using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace SmartSolucionesCuba.SAPRESSC.WebApp.CPCC.Billboard
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var appContext = services.GetService<Core.Web.Common.IApplicationContext>();

                    var dbContext = services.GetService<Persistence.Context.ApplicationDbContext>();

                    var env = services.GetService<IHostingEnvironment>();

                    appContext.InitializeDatabase(env, dbContext);
                }
                catch (System.Exception)
                {
                }
            }

            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            Microsoft.AspNetCore.WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
