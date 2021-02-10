using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Web.Api.Common.Models;
using SmartSolucionesCuba.SAPRESSC.WpfApp.CPCC.Billboard.FixScreenClient.Controls.Slide;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System;
using SmartSolucionesCuba.SAPRESSC.WpfApp.CPCC.Billboard.FixScreenClient.Models;

namespace SmartSolucionesCuba.SAPRESSC.WpfApp.CPCC.Billboard.FixScreenClient.Providers
{
    public class PlaylistProvider
    {
        private static PlaylistProvider singleInstance;

        public static PlaylistProvider GetSingleInstance()
        {
            if (singleInstance == null) singleInstance = new PlaylistProvider();

            return singleInstance;
        }

        private List<PlaylistItemMetadataModel> metadataList;
        private Queue<UserControl> controls;

        public PlaylistProvider()
        {
            controls = new Queue<UserControl>();
            metadataList = new List<PlaylistItemMetadataModel>();
        }

        public void ReloadData()
        {
            metadataList.Clear();

            var repeatingEvents = false;

            var spanichCultureInfo = System.Globalization.CultureInfo.GetCultureInfo("es");

            var dateFormat = "dddd d";

            var datetimeFormat = "dddd d / h:mm tt";

            var eventQueue = new Queue<EventModel>(CachedDataProvider.GetSingleInstance().Events);
            var cinemaBillboardQueue = new Queue<CinemaBillboardModel>(CachedDataProvider.GetSingleInstance().CinemaBillboards);

            var host = System.Configuration.ConfigurationManager.AppSettings.Get("ApiSeverName");

            if (CachedDataProvider.GetSingleInstance().Configuration.ShowEvents && eventQueue.Count > 0)
            {
                var eventModel = eventQueue.Dequeue();

                metadataList.Add(new PlaylistItemMetadataModel { Title = eventModel.Title, Tag = "Evento", Subjects = new[] { eventModel.Start.ToString(datetimeFormat, spanichCultureInfo), (eventModel.Start == eventModel.End) ? "Todo el día" : eventModel.End.ToString(datetimeFormat, spanichCultureInfo) } });

                controls.Enqueue(new EventDetailSlideControl(eventModel, false));

                if (eventModel.PromoVideo != null)
                {
                    metadataList.Add(new PlaylistItemMetadataModel { Title = eventModel.Title, Tag = "Video Promocional", Subjects = new[] { eventModel.Start.ToString(datetimeFormat, spanichCultureInfo), (eventModel.Start == eventModel.End) ? "Todo el día" : eventModel.End.ToString(datetimeFormat, spanichCultureInfo) } });

                    controls.Enqueue(new TrailerSlideControl(new Uri(string.Format("rtmp://{0}:1935/cpccbillboard_vod/{1}{2}", host, eventModel.PromoVideo.Id, eventModel.PromoVideo.Extension))));
                }
            }

            if (CachedDataProvider.GetSingleInstance().Configuration.ShowPresentations)
            {
                while (cinemaBillboardQueue.Count > 0)
                {
                    var targetCinemaBillboard = cinemaBillboardQueue.Dequeue();

                    metadataList.Add(new PlaylistItemMetadataModel { Title = targetCinemaBillboard.Cinema.Name, Tag = "Cartelera", Subjects = new[] { DateTime.Now.ToString(dateFormat, spanichCultureInfo), DateTime.Now.AddDays(6).ToString(dateFormat, spanichCultureInfo) } });

                    controls.Enqueue(new CinemaOverviewSlideControl(targetCinemaBillboard.Cinema, controls.Count > 0));

                    foreach (var targetRoomBillboard in targetCinemaBillboard.RoomBillboards)
                    {
                        foreach (var targetRoomDayBillboard in targetRoomBillboard.RoomDayBillboards)
                        {
                            metadataList.Add(new PlaylistItemMetadataModel { Title = "Sala " + targetRoomBillboard.Room.Name, Tag = "Programación", Subjects = new[] { targetRoomDayBillboard.Date.ToString(dateFormat, spanichCultureInfo) }, About = targetCinemaBillboard.Cinema.Name });

                            controls.Enqueue(new RoomDayBillboardOverviewSlideControl(targetRoomDayBillboard, targetCinemaBillboard.Cinema.Name, targetRoomBillboard.Room.Name));

                            foreach (var targetPresentation in targetRoomDayBillboard.Presentations)
                            {
                                metadataList.Add(new PlaylistItemMetadataModel { Title = targetPresentation.Production.Title, Tag = "Presentación", Subjects = new[] { targetPresentation.Date.ToString(datetimeFormat, spanichCultureInfo) }, About = "Sala " + targetRoomBillboard.Room.Name + ", " + targetCinemaBillboard.Cinema.Name });

                                controls.Enqueue(new PresentationDetailSlideControl(controls.Last() is PresentationDetailSlideControl, targetPresentation, targetCinemaBillboard.Cinema.Name, targetRoomBillboard.Room.Name));

                                if (targetPresentation.Production.TrailerVideo == null) continue;

                                metadataList.Add(new PlaylistItemMetadataModel { Title = targetPresentation.Production.Title, Tag = "Trailer", Subjects = new[] { targetPresentation.Date.ToString(datetimeFormat, spanichCultureInfo) }, About = "Sala " + targetRoomBillboard.Room.Name + ", " + targetCinemaBillboard.Cinema.Name });

                                controls.Enqueue(new TrailerSlideControl(new Uri(string.Format("rtmp://{0}:1935/cpccbillboard_vod/{1}{2}", host, targetPresentation.Production.TrailerVideo.Id, targetPresentation.Production.TrailerVideo.Extension))));
                            }

                            if (CachedDataProvider.GetSingleInstance().Configuration.ShowEvents)
                            {
                                if (eventQueue.Count <= 0)
                                {
                                    eventQueue = new Queue<EventModel>(CachedDataProvider.GetSingleInstance().Events);
                                }

                                if (eventQueue.Count > 0)
                                {
                                    var eventModel = eventQueue.Dequeue();

                                    metadataList.Add(new PlaylistItemMetadataModel { Title = eventModel.Title, Tag = "Evento", Subjects = new[] { eventModel.Start.ToString(datetimeFormat, spanichCultureInfo), (eventModel.Start == eventModel.End) ? "Todo el día" : eventModel.End.ToString(datetimeFormat, spanichCultureInfo) } });

                                    controls.Enqueue(new EventDetailSlideControl(eventModel, true));

                                    if (eventModel.PromoVideo != null)
                                    {
                                        metadataList.Add(new PlaylistItemMetadataModel { Title = eventModel.Title, Tag = "Video Promocional", Subjects = new[] { eventModel.Start.ToString(datetimeFormat, spanichCultureInfo), (eventModel.Start == eventModel.End) ? "Todo el día" : eventModel.End.ToString(datetimeFormat, spanichCultureInfo) } });

                                        controls.Enqueue(new TrailerSlideControl(new Uri(string.Format("rtmp://{0}:1935/cpccbillboard_vod/{1}{2}", host, eventModel.PromoVideo.Id, eventModel.PromoVideo.Extension))));
                                    }
                                }
                            }
                        }
                    }
                }
            }

            if (CachedDataProvider.GetSingleInstance().Configuration.ShowEvents && !repeatingEvents)
            {
                while (eventQueue.Count > 0)
                {
                    var eventModel = eventQueue.Dequeue();

                    metadataList.Add(new PlaylistItemMetadataModel { Title = eventModel.Title, Tag = "Evento", Subjects = new[] { eventModel.Start.ToString(datetimeFormat, spanichCultureInfo), (eventModel.Start == eventModel.End) ? "Todo el día" : eventModel.End.ToString(datetimeFormat, spanichCultureInfo) } });

                    controls.Enqueue(new EventDetailSlideControl(eventModel, true));

                    if (eventModel.PromoVideo != null)
                    {
                        metadataList.Add(new PlaylistItemMetadataModel { Title = eventModel.Title, Tag = "Video Promocional", Subjects = new[] { eventModel.Start.ToString(datetimeFormat, spanichCultureInfo), (eventModel.Start == eventModel.End) ? "Todo el día" : eventModel.End.ToString(datetimeFormat, spanichCultureInfo) } });

                        controls.Enqueue(new TrailerSlideControl(new Uri(string.Format("rtmp://{0}:1935/cpccbillboard_vod/{1}{2}", host, eventModel.PromoVideo.Id, eventModel.PromoVideo.Extension))));
                    }
                }
            }
        }

        public UserControl DequeueForPresentation()
        {
            return controls.Dequeue();
        }

        public bool IsEmpty { get => controls.Count <= 0; }

        public PlaylistStatusModel Status
        {
            get
            {
                var limit = CachedDataProvider.GetSingleInstance().Configuration.PlaylistSidebarItemsCount;

                var maxCount = metadataList.Count;

                var absoluteIndex = metadataList.Count - controls.Count - 1;

                int relativeIndex = limit / 2;

                var offset = absoluteIndex - 2;

                if (offset < 0)
                {
                    offset = 0;
                }

                if (offset + limit >= maxCount)
                {
                    offset = maxCount - limit;
                }

                if (absoluteIndex < relativeIndex)
                {
                    relativeIndex = absoluteIndex;
                }
                else if (maxCount - 1 - absoluteIndex < relativeIndex)
                {
                    relativeIndex = limit - 1 - (maxCount - 1 - absoluteIndex);
                }

                return new PlaylistStatusModel(metadataList.Skip(offset).Take(limit).ToArray(), relativeIndex);
            }
        }
    }
}
