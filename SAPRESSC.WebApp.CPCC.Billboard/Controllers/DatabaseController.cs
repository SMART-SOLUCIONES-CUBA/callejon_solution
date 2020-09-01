using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Middleware.Media.Web.Common.Facades.Manager;
using SmartSolucionesCuba.SAPRESSC.WebApp.CPCC.Billboard.Persistence.Context;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;

namespace SmartSolucionesCuba.SAPRESSC.WebApp.CPCC.Billboard.Controllers
{
    public class DatabaseController : Core.Web.Common.Controllers.BaseWebController
    {
        public const string WORK_DIRECTORY = "portabledb";
        private const string TEMP_DIRECTORY = "temp";

        private readonly ApplicationDbContext dbContext;
        private readonly IHostingEnvironment env;
        private readonly Core.Web.Common.IApplicationContext appContext;
        private readonly IMediaFileManagerFacade mediaFileManagerFacade;

        public DatabaseController(IMediaFileManagerFacade mediaFileManagerFacade, ApplicationDbContext dbContext, IHostingEnvironment env, Core.Web.Common.IApplicationContext appContext, Microsoft.Extensions.Localization.IStringLocalizer<DatabaseController> localizer, Microsoft.Extensions.Logging.ILogger<DatabaseController> logger)
            : base(localizer, logger)
        {
            this.env = env;
            this.dbContext = dbContext;
            this.appContext = appContext;
            this.mediaFileManagerFacade = mediaFileManagerFacade;
        }

        private void SavePortableDbCopy(System.DateTime targetDate, Microsoft.Extensions.FileProviders.IFileInfo workDirectoryInfo)
        {
            var fileName = targetDate.Ticks.ToString();

            var tempDirectoryInfo = env.WebRootFileProvider.GetFileInfo(TEMP_DIRECTORY);

            if (!Directory.Exists(tempDirectoryInfo.PhysicalPath))
            {
                Directory.CreateDirectory(tempDirectoryInfo.PhysicalPath);
            }

            var filePath = Path.Combine(tempDirectoryInfo.PhysicalPath, fileName);

            if (System.IO.File.Exists(filePath))
            {
                fileName = System.Guid.NewGuid().ToString();

                filePath = Path.Combine(tempDirectoryInfo.PhysicalPath, fileName);
            }

            var dbContextOptionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

            dbContextOptionsBuilder.UseSqlite(string.Format("Filename={0}", filePath));

            using (var portableDbContext = new ApplicationDbContext(dbContextOptionsBuilder.Options, appContext))
            {
                portableDbContext.Database.EnsureCreated();

                var startDate = System.DateTime.Today;
                var endDate = System.DateTime.Today.AddDays(7);

                var pivotDate = startDate;

                while (pivotDate <= endDate)
                {
                    var planedPresentations = dbContext.Presentations
                        .Include(p => p.Room).ThenInclude(r => r.Cinema).ThenInclude(c => c.Image)
                        .Include(p => p.Room).ThenInclude(r => r.Cinema).ThenInclude(c => c.OrganizationPostalContactItemBridges).ThenInclude(bridge => bridge.ContactItem)
                        .Include(p => p.Room).ThenInclude(r => r.Cinema).ThenInclude(c => c.OrganizationEmailContactItemBridges).ThenInclude(bridge => bridge.ContactItem)
                        .Include(p => p.Room).ThenInclude(r => r.Cinema).ThenInclude(c => c.OrganizationPhoneContactItemBridges).ThenInclude(bridge => bridge.ContactItem)
                        .Include(p => p.Production).ThenInclude(p => p.Clasification)
                        .Include(p => p.Production).ThenInclude(p => p.Country)
                        .Include(p => p.Production).ThenInclude(p => p.Language)
                        .Include(p => p.Production).ThenInclude(p => p.PosterImage)
                        .Where(e => e.Date.Date == pivotDate && e.Enabled).OrderBy(e => e.Date);

                    if (planedPresentations.Count() > 0)
                    {
                        foreach (var presentation in planedPresentations)
                        {
                            portableDbContext.Presentations.Add(presentation);

                            var relatedParticipations = dbContext.Participations
                                .Include(p => p.Personality).ThenInclude(p => p.Image)
                                .Where(p => p.Production.Id == presentation.Production.Id);

                            portableDbContext.Participations.AddRange(relatedParticipations);

                            //foreach (var participation in relatedParticipations)
                            //{
                            //    var personalityComments = dbContext.PersonalityComments.Where(entity => !entity.Banish && entity.Personality.Id == participation.Personality.Id);

                            //    portableDbContext.PersonalityComments.AddRange(personalityComments);
                            //}

                            var relatedAwardWinnings = dbContext.AwardWinnings
                                .Include(aw => aw.Award)
                                .Where(aw => aw.Production.Id == presentation.Production.Id);

                            portableDbContext.AwardWinnings.AddRange(relatedAwardWinnings);

                            var relatedProductionGenreBridges = dbContext.ProductionGenreBridges
                                .Include(bridge => bridge.Genre)
                                .Where(bridge => bridge.ProductionId == presentation.Production.Id);

                            portableDbContext.ProductionGenreBridges.AddRange(relatedProductionGenreBridges);

                            var relatedProducerHouseProductionBridges = dbContext.ProducerHouseProductionBridges
                                .Include(bridge => bridge.ProducerHouse).ThenInclude(ph => ph.Image)
                                .Where(bridge => bridge.ProductionId == presentation.Production.Id);

                            portableDbContext.ProducerHouseProductionBridges.AddRange(relatedProducerHouseProductionBridges);

                            var relatedCoCountries = dbContext.CountryCoProductionBridges
                                .Include(bridge => bridge.Country)
                                .Where(bridge => bridge.ProductionId == presentation.Production.Id);

                            portableDbContext.CountryCoProductionBridges.AddRange(relatedCoCountries);

                            //var relatedProductionComments = dbContext.ProductionComments.Where(entity => !entity.Banish && entity.Production.Id == presentation.Production.Id)
                            //    .Include(entity => entity.Client).ThenInclude(entity => entity.Image)
                            //    .Include(entity => entity.Delations).ThenInclude(entity => entity.Client).ThenInclude(entity => entity.Image)
                            //    .Include(entity => entity.Reference);

                            //portableDbContext.ProductionComments.AddRange(relatedProductionComments);
                        }
                    }

                    pivotDate = pivotDate.AddDays(1);
                }

                var planedEvents = dbContext.Events
                    .Include(e => e.PosterImage)
                    .Where(e => e.Enabled && ((startDate <= e.Start.Date) || (startDate <= e.End.Date)))
                    .OrderBy(entity => entity.Start);

                foreach (var @event in planedEvents)
                {
                    portableDbContext.Events.Add(@event);

                    var relatedPersonalityBridges = dbContext.EventPersonalityBridges
                        .Include(bridge => bridge.Personality).ThenInclude(p => p.Image)
                        .Where(bridge => bridge.EventId == @event.Id);

                    portableDbContext.EventPersonalityBridges.AddRange(relatedPersonalityBridges);

                    //foreach (var personalityBridge in relatedPersonalityBridges)
                    //{
                    //    var personalityComments = dbContext.PersonalityComments.Where(entity => !entity.Banish && entity.Personality.Id == personalityBridge.PersonalityId);

                    //    portableDbContext.PersonalityComments.AddRange(personalityComments);
                    //}

                    //var relatedEventComments = dbContext.EventComments.Where(entity => !entity.Banish && entity.Event.Id == @event.Id)
                    //            .Include(entity => entity.Client).ThenInclude(entity => entity.Image)
                    //            .Include(entity => entity.Delations).ThenInclude(entity => entity.Client).ThenInclude(entity => entity.Image)
                    //            .Include(entity => entity.Reference);

                    //portableDbContext.EventComments.AddRange(relatedEventComments);
                }

                portableDbContext.Advices.AddRange(dbContext.Advices);

                portableDbContext.SaveChanges("system");
            }

            System.IO.File.Move(filePath, Path.Combine(workDirectoryInfo.PhysicalPath, fileName));
        }

        public IActionResult Build()
        {
            var today = System.DateTime.Today;

            var targetDate = System.DateTime.MinValue;

            var workDirectoryInfo = env.WebRootFileProvider.GetFileInfo(WORK_DIRECTORY);

            if (!Directory.Exists(workDirectoryInfo.PhysicalPath))
            {
                Directory.CreateDirectory(workDirectoryInfo.PhysicalPath);
            }

            targetDate = System.DateTime.Now;

            SavePortableDbCopy(targetDate, workDirectoryInfo);

            var notification = new Core.Common.Models.View.NotificationViewModel
            {
                Type = Core.Common.Models.View.NotificationType.Success,
                Message = "Construcción de base de datos para aplicaciones móviles en proceso."
            };

            TempData[Core.Common.Models.View.NotificationViewModel.NOTIFICATION_MAP_KEY] = Newtonsoft.Json.JsonConvert.SerializeObject(notification);

            return new RedirectToActionResult("index", "management", new { Area = string.Empty });
        }

        public IActionResult Portable()
        {
            var today = System.DateTime.Today;

            var tomorrow = today.AddDays(1);

            var targetDate = System.DateTime.MinValue;

            var workDirectoryInfo = env.WebRootFileProvider.GetFileInfo(WORK_DIRECTORY);

            if (!Directory.Exists(workDirectoryInfo.PhysicalPath))
            {
                Directory.CreateDirectory(workDirectoryInfo.PhysicalPath);
            }
            else
            {
                var portabledbFiles = Directory.GetFiles(workDirectoryInfo.PhysicalPath);

                if (portabledbFiles.Any())
                {
                    try
                    {
                        var portabledbDates = portabledbFiles.Select(item => new System.DateTime(long.Parse(Path.GetFileName(item)))).ToArray();

                        var selectablesPortabledbDates = portabledbDates.Where(current => today <= current && current <= tomorrow).OrderByDescending(current => current.Ticks).ToArray();

                        targetDate = selectablesPortabledbDates.FirstOrDefault();
                    }
                    catch { }
                }
            }

            if (targetDate == System.DateTime.MinValue)
            {
                targetDate = System.DateTime.Now;

                SavePortableDbCopy(targetDate, workDirectoryInfo);
            }

            var targetFilePath = Path.Combine(workDirectoryInfo.PhysicalPath, targetDate.Ticks.ToString());

            try
            {
                var fileReadStream = System.IO.File.OpenRead(targetFilePath);

                var fileContent = new byte[fileReadStream.Length];

                fileReadStream.Read(fileContent, 0, (int)fileReadStream.Length);

                fileReadStream.Close();

                var result = new FileContentResult(fileContent, "application/octet-stream")
                {
                    FileDownloadName = "cpcc.db"
                };

                dbContext.PortableDbDownloads.Add(new PortableDbDownload { UserAgent = Request.Headers.First(h => h.Key == "User-Agent").Value });

                dbContext.SaveChanges("system");

                return result;
            }
            catch { }

            return new StatusCodeResult(500);
        }

        public IActionResult Pack()
        {
            var startEdgeDate = System.DateTime.Today;

            var eventRepository = new Business.Billboard.CPCC.Persistence.Repositories.EventRepository(dbContext);

            System.Linq.Expressions.Expression<System.Func<Event, bool>> criteria = entity => entity.Enabled && ((startEdgeDate <= entity.Start.Date) || (startEdgeDate <= entity.End.Date));

            var events = eventRepository.FindBy(criteria).OrderBy(entity => entity.Start);

            var eventModels = new System.Collections.Generic.List<Business.Billboard.CPCC.Web.Api.Common.Models.EventModel>();

            foreach (var entity in events)
            {
                eventModels.Add(Business.Billboard.CPCC.Web.FrontEnd.Factories.Api.Models.EventModelFactory.BuildFor(entity, mediaFileManagerFacade, false));
            }

            var eventsJson = Newtonsoft.Json.JsonConvert.SerializeObject(eventModels);

            var eventJsonData = System.Text.Encoding.ASCII.GetBytes(eventsJson);

            var cinemaRepository = new Business.Billboard.Cinema.Persistence.Repositories.CinemaRepository(dbContext);

            var cinemas = cinemaRepository.GetAll();

            var cinemaBillboards = new System.Collections.Generic.List<Business.Billboard.Cinema.Web.Api.Common.Models.CinemaBillboardModel>();

            var endEdgeDate = System.DateTime.Today.AddDays(7);

            foreach (var item in cinemas)
            {
                var model = Business.Billboard.Cinema.Web.FrontEnd.Factories.Api.Models.CinemaBillboardModelFactory.BuildFor(item, mediaFileManagerFacade, startEdgeDate, endEdgeDate, false, true);

                if (model.RoomBillboards.Count <= 0) continue;

                cinemaBillboards.Add(model);
            }

            var cinemaBillboardsJson = Newtonsoft.Json.JsonConvert.SerializeObject(cinemaBillboards);

            var cinemaBillboardsJsonData = System.Text.Encoding.ASCII.GetBytes(cinemaBillboardsJson);

            var zippedImages = new System.Guid[] { };

            var resultMemoryStream = new MemoryStream();

            using (var archive = new System.IO.Compression.ZipArchive(resultMemoryStream, System.IO.Compression.ZipArchiveMode.Create, true))
            {
                var zipArchiveEntry = archive.CreateEntry("events.json", System.IO.Compression.CompressionLevel.Fastest);

                using (var zipStream = zipArchiveEntry.Open()) zipStream.Write(eventJsonData, 0, eventJsonData.Length);

                foreach (var @event in events)
                {
                    if (zippedImages.Contains(@event.PosterImage.Id)) continue;

                    zipArchiveEntry = archive.CreateEntry(@event.PosterImage.Id.ToString(), System.IO.Compression.CompressionLevel.NoCompression);

                    var imageContent = mediaFileManagerFacade.GetImageFileContent(@event.PosterImage.Id);

                    using (var zipStream = zipArchiveEntry.Open()) zipStream.Write(imageContent, 0, imageContent.Length);
                }

                zipArchiveEntry = archive.CreateEntry("cinemabillboards.json", System.IO.Compression.CompressionLevel.Fastest);

                using (var zipStream = zipArchiveEntry.Open()) zipStream.Write(cinemaBillboardsJsonData, 0, cinemaBillboardsJsonData.Length);

                foreach (var cinemaBillboard in cinemaBillboards)
                {
                    foreach (var roomBillboard in cinemaBillboard.RoomBillboards)
                    {
                        foreach (var roomDayBillboard in roomBillboard.RoomDayBillboards)
                        {
                            foreach (var presentation in roomDayBillboard.Presentations)
                            {
                                if (zippedImages.Contains(presentation.Production.PosterImage.Id)) continue;

                                zipArchiveEntry = archive.CreateEntry(presentation.Production.PosterImage.Id.ToString(), System.IO.Compression.CompressionLevel.NoCompression);

                                var imageContent = mediaFileManagerFacade.GetImageFileContent(presentation.Production.PosterImage.Id);

                                using (var zipStream = zipArchiveEntry.Open()) zipStream.Write(imageContent, 0, imageContent.Length);
                            }
                        }
                    }
                }
            }

            return File(resultMemoryStream.ToArray(), "application/zip", string.Format("portable-update-{0}-{1}.zip", System.DateTime.Now.ToShortDateString().Replace('/', '_'), System.DateTime.Now.ToShortTimeString()));
        }
    }
}
