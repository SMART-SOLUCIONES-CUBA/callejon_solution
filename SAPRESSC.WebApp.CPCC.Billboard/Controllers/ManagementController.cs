using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.Repositories;
using SmartSolucionesCuba.SAPRESSC.Core.Web.Common;
using SmartSolucionesCuba.SAPRESSC.Core.Web.Common.Controllers;
using SmartSolucionesCuba.SAPRESSC.Core.Web.Security.Filters;
using System.Linq;

namespace SmartSolucionesCuba.SAPRESSC.WebApp.CPCC.Billboard.Controllers
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [RequiresRoleBasePermission(Core.Web.Security.SecurityRoleCodes.ShellManager)]
    [ChangeUserPasswordRequiredAction]
    [CatchChangeUserPasswordRequiredException]
    public class ManagementController : BaseWebController
    {
        private static readonly string[] DAYS_OF_WEEK = new [] { "Domingo", "Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado"};
        private static readonly string[] MONTHS = new[] { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };

        private readonly IHostingEnvironment env;
        private readonly Persistence.Context.ApplicationDbContext applicationDbContext;
        private readonly IApplicationContext applicationContext;
        private readonly IEntityRepository<Event, System.Guid> eventRepository;
        private readonly IEntityRepository<Presentation, long> presentationRepository;

        public ManagementController(IHostingEnvironment env, Persistence.Context.ApplicationDbContext applicationDbContext, IEntityRepository<Event, System.Guid> eventRepository, IEntityRepository<Presentation, long> presentationRepository, Microsoft.Extensions.Localization.IStringLocalizer<ManagementController> localizer, ILogger<BaseWebController> logger, IApplicationContext applicationContext) : base(localizer, logger)
        {
            this.env = env;
            this.applicationDbContext = applicationDbContext;
            this.applicationContext = applicationContext;
            this.eventRepository = eventRepository;
            this.presentationRepository = presentationRepository;
        }

        public IActionResult Index()
        {
            var startEdgeDate = System.DateTime.Today;
            var endEdgeDate = startEdgeDate.AddDays(7);
            var passWeekEdgeDate = startEdgeDate.AddDays(-6);
            var passMonthEdgeDate = startEdgeDate.AddMonths(-1);
            var passSemesterEdgeDate = startEdgeDate.AddMonths(-5);
            var passYearEdgeDate = startEdgeDate.AddMonths(-11);

            var model = new Models.View.DashboardViewModel
            {
                Groups = Helpers.DashboardManagementItemGroupHelper.GetGrouped(applicationContext),
                NextWeekBillboardEventCount = eventRepository.FindBy(entity => entity.Enabled && ((startEdgeDate <= entity.Start.Date) || (startEdgeDate <= entity.End.Date))).Count(),
                NextWeekBillboardPresentationCount = presentationRepository.FindBy(p => p.Enabled && ((p.Date.Date >= startEdgeDate) && (p.Date.Date <= endEdgeDate))).OrderBy(e => e.Date).Count(),
            };

            for (System.DateTime currentDate = passWeekEdgeDate; currentDate <= startEdgeDate; currentDate = currentDate.AddDays(1))
            {
                model.WeekStatisticsColumns.Add(DAYS_OF_WEEK[(int)currentDate.DayOfWeek]);

                var eventCommentCount = applicationDbContext.EventComments.Count(entity => entity.Created.Date == currentDate);
                var personalityCommentCount = applicationDbContext.PersonalityComments.Count(entity => entity.Created.Date == currentDate);
                var productionCommentCount = applicationDbContext.ProductionComments.Count(entity => entity.Created.Date == currentDate);

                model.WeekEventCommentStatisticsValues.Add(eventCommentCount);
                model.WeekPersonalityCommentStatisticsValues.Add(personalityCommentCount);
                model.WeekPresentationCommentStatisticsValues.Add(productionCommentCount);
                model.WeekTotalCommentStatisticsValues.Add(eventCommentCount + personalityCommentCount + productionCommentCount);

                model.WeekMobileDatabaseUpdateStatisticsValues.Add(applicationDbContext.PortableDbDownloads.Count(entity => entity.Created.Date == currentDate));
            }

            for (System.DateTime currentDate = passMonthEdgeDate; currentDate <= startEdgeDate; currentDate = currentDate.AddDays(1))
            {
                model.MonthStatisticsColumns.Add(currentDate.Day.ToString());

                var eventCommentCount = applicationDbContext.EventComments.Count(entity => entity.Created.Date == currentDate);
                var personalityCommentCount = applicationDbContext.PersonalityComments.Count(entity => entity.Created.Date == currentDate);
                var productionCommentCount = applicationDbContext.ProductionComments.Count(entity => entity.Created.Date == currentDate);

                model.MonthEventCommentStatisticsValues.Add(eventCommentCount);
                model.MonthPersonalityCommentStatisticsValues.Add(personalityCommentCount);
                model.MonthPresentationCommentStatisticsValues.Add(productionCommentCount);
                model.MonthTotalCommentStatisticsValues.Add(eventCommentCount + personalityCommentCount + productionCommentCount);

                model.MonthMobileDatabaseUpdateStatisticsValues.Add(applicationDbContext.PortableDbDownloads.Count(entity => entity.Created.Date == currentDate));
            }

            for (System.DateTime currentDate = passSemesterEdgeDate; currentDate <= startEdgeDate; currentDate = currentDate.AddMonths(1))
            {
                model.SemesterStatisticsColumns.Add(MONTHS[currentDate.Month - 1]);

                var eventCommentCount = applicationDbContext.EventComments.Count(entity => (entity.Created.Month == currentDate.Month) && (entity.Created.Year == currentDate.Year));
                var personalityCommentCount = applicationDbContext.PersonalityComments.Count(entity => (entity.Created.Month == currentDate.Month) && (entity.Created.Year == currentDate.Year));
                var productionCommentCount = applicationDbContext.ProductionComments.Count(entity => (entity.Created.Month == currentDate.Month) && (entity.Created.Year == currentDate.Year));

                model.SemesterEventCommentStatisticsValues.Add(eventCommentCount);
                model.SemesterPersonalityCommentStatisticsValues.Add(personalityCommentCount);
                model.SemesterPresentationCommentStatisticsValues.Add(productionCommentCount);
                model.SemesterTotalCommentStatisticsValues.Add(eventCommentCount + personalityCommentCount + productionCommentCount);

                model.SemesterMobileDatabaseUpdateStatisticsValues.Add(applicationDbContext.PortableDbDownloads.Count(entity => (entity.Created.Month == currentDate.Month) && (entity.Created.Year == currentDate.Year)));
            }

            for (System.DateTime currentDate = passYearEdgeDate; currentDate <= startEdgeDate; currentDate = currentDate.AddMonths(1))
            {
                model.YearStatisticsColumns.Add(MONTHS[currentDate.Month - 1]);

                var eventCommentCount = applicationDbContext.EventComments.Count(entity => (entity.Created.Month == currentDate.Month) && (entity.Created.Year == currentDate.Year));
                var personalityCommentCount = applicationDbContext.PersonalityComments.Count(entity => (entity.Created.Month == currentDate.Month) && (entity.Created.Year == currentDate.Year));
                var productionCommentCount = applicationDbContext.ProductionComments.Count(entity => (entity.Created.Month == currentDate.Month) && (entity.Created.Year == currentDate.Year));

                model.YearEventCommentStatisticsValues.Add(eventCommentCount);
                model.YearPersonalityCommentStatisticsValues.Add(personalityCommentCount);
                model.YearPresentationCommentStatisticsValues.Add(productionCommentCount);
                model.YearTotalCommentStatisticsValues.Add(eventCommentCount + personalityCommentCount + productionCommentCount);

                model.YearMobileDatabaseUpdateStatisticsValues.Add(applicationDbContext.PortableDbDownloads.Count(entity => (entity.Created.Month == currentDate.Month) && (entity.Created.Year == currentDate.Year)));
            }

            var workDirectoryInfo = env.WebRootFileProvider.GetFileInfo(DatabaseController.WORK_DIRECTORY);

            var portabledbFiles = System.IO.Directory.GetFiles(workDirectoryInfo.PhysicalPath);

            if (portabledbFiles.Any())
            {
                try
                {
                    var portabledbDates = portabledbFiles.Select(item => new System.DateTime(long.Parse(System.IO.Path.GetFileName(item)))).OrderByDescending(current => current.Ticks).ToArray();

                    var currentPortableDatabaseDateTime = portabledbDates.FirstOrDefault();

                    model.CurrentPortableDatabaseDateTimeFormated = currentPortableDatabaseDateTime.ToString(System.Globalization.CultureInfo.GetCultureInfo("ES-pr"));
                }
                catch { }
            }

            model.PersistenceSecurityUserCount = applicationDbContext.Users.LongCount();
            model.PersistenceSecurityGroupCount = applicationDbContext.Groups.LongCount();
            model.PersistenceSecurityRoleCount = applicationDbContext.Roles.LongCount();
            model.PersistenceSecurityTraceCount = applicationDbContext.Traces.LongCount();

            model.PersistenceBillboardProductionCount = applicationDbContext.Productions.LongCount();
            model.PersistenceBillboardPresentationCount = applicationDbContext.Presentations.LongCount();
            model.PersistenceBillboardHighlightCount = applicationDbContext.Advices.LongCount();
            model.PersistenceBillboardEventCount = applicationDbContext.Events.LongCount();
            model.PersistenceBillboardConfigCount = applicationDbContext.FixScreenClientConfigurations.LongCount();
            model.PersistenceBillboardProductionCommentCount = applicationDbContext.ProductionComments.LongCount();
            model.PersistenceBillboardEventCommentCount = applicationDbContext.EventComments.LongCount();
            model.PersistenceBillboardPersonalityCommentCount = applicationDbContext.PersonalityComments.LongCount();

            model.PersistenceSupportCountryCount = applicationDbContext.Countries.LongCount();
            model.PersistenceSupportCategoryCount = applicationDbContext.Categories.LongCount();
            model.PersistenceSupportPersonalityCount = applicationDbContext.Personalities.LongCount();
            model.PersistenceSupportGenreCount = applicationDbContext.Genres.LongCount();
            model.PersistenceSupportAwardCount = applicationDbContext.Awards.LongCount();
            model.PersistenceSupportLanguageCount = applicationDbContext.Languages.LongCount();
            model.PersistenceSupportProductionHouseCount = applicationDbContext.ProducerHouses.LongCount();
            model.PersistenceSupportCinemaCount = applicationDbContext.Cinemas.LongCount();
            model.PersistenceSupportClasifiersCount = applicationDbContext.CommentClassifiers.LongCount();

            return View(model);
        }
    }
}
