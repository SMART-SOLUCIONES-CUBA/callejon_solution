using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Serilog;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Manager.Controllers.Management;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Web.BackEnd.Controllers.Management;
using SmartSolucionesCuba.SAPRESSC.Core.Web.Common;

namespace SmartSolucionesCuba.SAPRESSC.WebApp.CPCC.Billboard
{
    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();

            this.configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<Options.ApiSecurityOptions>(configuration.GetSection("ApiSecurity"));
            services.Configure<Business.Billboard.CPCC.Web.FrontEnd.Options.MediaStoreOptions>(configuration.GetSection("MediaStore"));
            services.Configure<Business.Billboard.Cinema.Web.Common.Options.FfmpegOptions>(configuration.GetSection("FFMPEG"));

            services.AddDbContext<Persistence.Context.ApplicationDbContext>(options => options.UseMySql(configuration.GetConnectionString("DefaultConnection"), mySqlOptions =>
            {
                mySqlOptions.ServerVersion(new System.Version(5, 5, 24), Pomelo.EntityFrameworkCore.MySql.Infrastructure.ServerType.MySql);
            }));

            services.AddScoped<DbContext, Persistence.Context.ApplicationDbContext>();

            services.AddLogging();

            var appContext = new BaseApplicationContext(
                "CPCCBillboardCookies",
                new IApplicationModule[]
                {
                    new Core.Web.Security.SecurityWebModule(),
                    new Core.Web.SecurityLocal.LocalSecurityWebModule(),
                    new Middleware.Region.Web.Common.RegionMiddlewareCommonApplicationModule(),
                    new Middleware.Region.Web.Manager.RegionMiddlewareManagementApplicationModule(),
                    new Middleware.Region.Web.SecurityLocal.RegionMiddlewareSecurityLocalApplicationModule(),
                    new Middleware.Media.Web.Common.MediaMiddlewareCommonApplicationModule(),
                    new Middleware.Identity.Web.Common.IdentityMiddlewareCommonApplicationModule(),
                    new Business.Billboard.Cinema.Web.Common.CinemaBillboardBusinessCommonApplicationModule(),
                    new Business.Billboard.Cinema.Web.Manager.CinemaBillboardBusinessManagementApplicationModule(),
                    new Business.Billboard.Cinema.Web.SecurityLocal.CinemaBillboardBusinessSecurityLocalApplicationModule(),
                    new Business.Billboard.CPCC.Web.Common.CPCCBillboardBusinessCommonApplicationModule(),
                    new Business.Billboard.CPCC.Web.BackEnd.CPCCBillboardBusinessManagementApplicationModule(),
                    new Business.Billboard.CPCC.Web.SecurityLocal.CPCCBillboardBusinessSecurityLocalApplicationModule()
                }
            );

            appContext.RegisterServices(services);

            services.AddSingleton<Helpers.Localization.GlobalViewLocalizationHelper>();
            services.AddSingleton<IApplicationContext>(appContext);
            services.AddSingleton<Core.Web.Security.Licensing.ILicenseValidator, Licensing.LicenseValidator>();
            services.AddTransient<Core.Web.Security.Models.View.IUserProfileViewModel, Models.View.UserProfileViewModel>();
            services.AddScoped<Core.Web.Security.Facades.Manager.IUserProfileManagerFacade, Managers.UserProfileManager>();
            services.AddSingleton<Middleware.Media.Web.Common.Facades.Manager.IMediaFileManagerFacade, Managers.MediaFileManager>();
            services.AddSingleton<Core.Web.Common.ModelBinding.IConcretModelBindHelper, ModelBinding.ConcretModelBindHelper>();

            services.AddSingleton<IStringLocalizer<PresentationsController>, Helpers.Localization.GlobalViewLocalizationHelper>();
            services.AddSingleton<IStringLocalizer<ProductionsController>, Helpers.Localization.GlobalViewLocalizationHelper>();
            services.AddSingleton<IStringLocalizer<PersonalitiesController>, Helpers.Localization.GlobalViewLocalizationHelper>();
            services.AddSingleton<IStringLocalizer<EventsController>, Helpers.Localization.GlobalViewLocalizationHelper>();

            services.Configure<Microsoft.AspNetCore.Routing.RouteOptions>(options => options.LowercaseUrls = true);

            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.AddMvc(mvc =>
                mvc.ModelBinderProviders.Insert(0, new Core.Web.Common.ModelBinding.AbstractsModelBinderProvider())
            )
            .AddViewLocalization(Microsoft.AspNetCore.Mvc.Razor.LanguageViewLocationExpanderFormat.Suffix)
            .AddDataAnnotationsLocalization(options =>
                options.DataAnnotationLocalizerProvider = (type, factory) =>
                {
                    //return new LocalizerSpy();
                    return factory.Create(typeof(SharedResources));
                }
            );

            var defaultCulture = new System.Globalization.CultureInfo("es");

            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture(defaultCulture);
                options.SupportedCultures = new[] { defaultCulture };
                options.SupportedUICultures = new[] { defaultCulture };

                options.RequestCultureProviders.Insert(0, new Core.Web.Common.Localization.UrlRequestCultureProvider());
            });

            services.AddMvc();

            services.AddDistributedMemoryCache();
            services.AddSession();

            services.AddSingleton<ITempDataProvider, CookieTempDataProvider>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, Microsoft.Extensions.Options.IOptions<Business.Billboard.Cinema.Web.Common.Options.FfmpegOptions> ffmpegOptions)
        {
            FFMediaToolkit.FFmpegLoader.FFmpegPath = System.IO.Path.GetDirectoryName(ffmpegOptions.Value.Path);

            loggerFactory.AddSerilog(Log.Logger);

            var localizaionOptions = app.ApplicationServices.GetService<Microsoft.Extensions.Options.IOptions<RequestLocalizationOptions>>();

            app.UseRequestLocalization(localizaionOptions.Value);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseStatusCodePages();
            }

            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var appContext = serviceScope.ServiceProvider.GetService<IApplicationContext>();

                var dbContext = serviceScope.ServiceProvider.GetService<Persistence.Context.ApplicationDbContext>();

                appContext.Configure(app, env, loggerFactory, dbContext);
            }

            app.UseStaticFiles();

            app.UseSession();

            app.UseMiddleware<Middlewares.ApiKeyValidationMiddleware>();

            app.UseMvc(routes =>
            {
                routes.MapRoute("areaRoutes",
                    template: "{area:exists}/{controller=home}/{action=index}/{id?}");

                routes.MapRoute(
                    name: "defaultRoutes",
                    template: "{controller=start}/{action=index}/{id?}");
            });
        }
    }
}
