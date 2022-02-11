using Microsoft.EntityFrameworkCore;
using System.IO.Compression;
using System.Linq;

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Helpers
{
    internal class DataSyncronizationHelper
    {
        private const string DATABASE_UPDATE_DATETIME_KEY = "database-update-datetime-key";

        private static DataSyncronizationHelper instance;

        internal static DataSyncronizationHelper Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DataSyncronizationHelper();
                }

                return instance;
            }
        }

        private System.DateTime syncronizationDate;

        private DataSyncronizationHelper()
        {
            syncronizationDate = System.DateTime.MinValue;
        }

        public async System.Threading.Tasks.Task ResetAsync()
        {
            if (Xamarin.Forms.Application.Current.Properties.ContainsKey(DATABASE_UPDATE_DATETIME_KEY))
            {
                Xamarin.Forms.Application.Current.Properties.Remove(DATABASE_UPDATE_DATETIME_KEY);

                await Xamarin.Forms.Application.Current.SavePropertiesAsync();
            }

            syncronizationDate = System.DateTime.MinValue;
        }

        public System.DateTime SyncronizationDate
        {
            private set
            {
                syncronizationDate = value;
            }

            get
            {
                if (syncronizationDate == System.DateTime.MinValue)
                {
                    if (Xamarin.Forms.Application.Current.Properties.ContainsKey(DATABASE_UPDATE_DATETIME_KEY))
                    {
                        syncronizationDate = System.DateTime.Parse(Xamarin.Forms.Application.Current.Properties[DATABASE_UPDATE_DATETIME_KEY].ToString());
                    }
                }

                return syncronizationDate;
            }
        }

        public long GetDataBundleLength()
        {
            return GetDataBundleContent().LongLength;
        }

        public byte[] GetDataBundleContent()
        {
            var bundleFilePaths = GetBundleableDataFilePaths();

            if (bundleFilePaths.Count <= 0) throw new System.InvalidOperationException();

            byte[] result;

            using (var memoryStream = new System.IO.MemoryStream())
            {
                using (var zipArchive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                {
                    foreach (var path in bundleFilePaths)
                    {
                        byte[] fileToZipBytes = System.IO.File.ReadAllBytes(path);

                        var zipFileEntry = zipArchive.CreateEntry(System.IO.Path.GetFileName(path));

                        //add the file contents
                        using (var zipEntryStream = zipFileEntry.Open())
                        using (var zipFileBinary = new System.IO.BinaryWriter(zipEntryStream))
                        {
                            zipFileBinary.Write(fileToZipBytes);
                        }
                    }
                }

                result = memoryStream.ToArray();
            }            

            return result;
        }

        public void StartDataBundleAppling(byte[] dataBundleContent, System.DateTime at, System.Action<System.Threading.Tasks.Task> asyncContinueAction)
        {
            var deviceScreenMinSize = System.Convert.ToInt32(System.Math.Min(Xamarin.Essentials.DeviceDisplay.MainDisplayInfo.Width, Xamarin.Essentials.DeviceDisplay.MainDisplayInfo.Height));

            System.Threading.Tasks.Task.Run(() =>
            {
                using (var dataBundleStream = new System.IO.MemoryStream(dataBundleContent))
                {
                    using (var dataBundle = new ZipArchive(dataBundleStream))
                    {
                        foreach (var entry in dataBundle.Entries)
                        {
                            var filePath = LocalFileStorageHelper.GetFilePath(entry.Name);

                            entry.ExtractToFile(filePath);

                            if (!System.Guid.TryParse(entry.Name, out System.Guid imageId)) continue;

                            using (var imageFileStream = System.IO.File.OpenRead(filePath))
                            {
                                using (var skStream = new SkiaSharp.SKManagedStream(imageFileStream))
                                {
                                    using (var skBitmap = SkiaSharp.SKBitmap.Decode(skStream))
                                    {
                                        if (skBitmap.Width > deviceScreenMinSize)
                                        {
                                            double dif = System.Convert.ToDouble(skBitmap.Height) / System.Convert.ToDouble(skBitmap.Width);

                                            var requiredHeight = System.Convert.ToInt32(System.Math.Round(dif * deviceScreenMinSize));

                                            using (var resizedSkBitmap = skBitmap.Resize(new SkiaSharp.SKImageInfo(deviceScreenMinSize, requiredHeight), SkiaSharp.SKFilterQuality.High))
                                            {
                                                using (var skImage = SkiaSharp.SKImage.FromBitmap(resizedSkBitmap))
                                                {
                                                    using (var output = System.IO.File.OpenWrite(LocalFileStorageHelper.GetResponsiveImageFilePath(imageId, deviceScreenMinSize)))
                                                    {
                                                        skImage.Encode(SkiaSharp.SKEncodedImageFormat.Jpeg, 75).SaveTo(output);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                syncronizationDate = at;

                Xamarin.Forms.Application.Current.Properties[DATABASE_UPDATE_DATETIME_KEY] = syncronizationDate.ToString();

                Xamarin.Forms.Application.Current.SavePropertiesAsync();
            }).ContinueWith(asyncContinueAction);
        }

        public void StartSyncronizeDataFromCloud(System.Action<int, bool, string> reportProgressAction, System.Action<System.Threading.Tasks.Task<bool>> continueAction, bool force = false)
        {
            var protocol = "http";
            //var host = "192.168.1.100";
            var host = "sgc.elcallejon.net";
            //var host = "192.168.200.201";
            var port = 80;

            var url = protocol + "://" + host;

            if (!(("http" == protocol && port == 80) || ("https" == protocol && port == 443)))
            {
                url += ":" + port;
            }

            var baseAddress = new System.Uri(url);

            var apiBaseQuery = "api/cpccbillboard";

            reportProgressAction.Invoke(0, true, "Conectando ...");

            var deviceScreenMinSize = System.Convert.ToInt32(System.Math.Min(Xamarin.Essentials.DeviceDisplay.MainDisplayInfo.Width, Xamarin.Essentials.DeviceDisplay.MainDisplayInfo.Height));

            System.Threading.Tasks.Task.Run(async () =>
            {
                System.Net.Http.HttpResponseMessage response = null;

                var profile = Managers.ProfileManager.Instance.GetCurrentProfile();

                using (var client = new System.Net.Http.HttpClient())
                {
                    client.BaseAddress = baseAddress;
                    client.DefaultRequestHeaders.UserAgent.Add(new System.Net.Http.Headers.ProductInfoHeaderValue(new System.Net.Http.Headers.ProductHeaderValue("CPCCBillboardClient", "1001")));
                    client.DefaultRequestHeaders.Add(Providers.ConstantsProvider.API_KEY_HEADER_PROPERTY, Providers.ConstantsProvider.API_KEY_SECRET);
                    client.Timeout = System.TimeSpan.FromSeconds(30);

                    Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                    {
                        reportProgressAction.Invoke(0, true, "Buscando perfil ...");
                    });

                    #region Profile Handle Lines
                    response = await client.GetAsync(string.Format("{0}/clients/{1}", apiBaseQuery, profile.Id));

                    if (!response.IsSuccessStatusCode)
                    {
                        Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                        {
                            reportProgressAction.Invoke(0, true, "Enviando perfil ...");
                        });

                        using (var content = new System.Net.Http.StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(new SAPRESSC.Business.Billboard.CPCC.Web.Api.Common.Models.ClientModel
                        {
                            Id = profile.Id,
                            FirstName = profile.FirstName,
                            LastName = profile.LastName,
                            PhoneNumber = profile.Phone,
                            EmailAddress = profile.Email,
                            IsMale = profile.IsMale,
                            BornDate = profile.BornDate,
                            About = profile.About
                        }), System.Text.Encoding.UTF8, "application/json"))
                        {
                            response = await client.PostAsync(string.Format("{0}/clients", apiBaseQuery), content);

                            response.EnsureSuccessStatusCode();
                        }
                    }
                    #endregion

                    #region Comments Handle Lines
                    if (!force && SyncronizationDate != System.DateTime.MinValue)
                    {
                        Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                        {
                            reportProgressAction.Invoke(0, true, "Enviando nuevos comentarios ...");
                        });

                        using (var dbContext = Providers.DdContextProvider.GetDbContext())
                        {
                            var newEventComments = dbContext.EventComments.Where(entity => !entity.Syncronized).OrderBy(entity => entity.Created).Include(e => e.Client).Include(e => e.Event).Include(e => e.Reference).ToList();

                            if (newEventComments.Count() > 0)
                            {
                                foreach (var current in newEventComments)
                                {
                                    using (var content = new System.Net.Http.StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(new SAPRESSC.Business.Billboard.CPCC.Web.Api.Common.Models.CommentInputModel
                                    {
                                        Id = current.Id,
                                        ClientId = current.Client.Id,
                                        Message = current.Message,
                                        ReferenceId = current.Reference != null ? current.Reference.Id : System.Guid.Empty,
                                        Created = current.Created
                                    }), System.Text.Encoding.UTF8, "application/json"))
                                    {
                                        response = await client.PutAsync(string.Format("{0}/comments/event/{1}", apiBaseQuery, current.Event.Id), content);

                                        if (response.IsSuccessStatusCode)
                                        {
                                            current.Syncronized = true;

                                            dbContext.EventComments.Update(current);

                                            dbContext.SaveChanges("client");
                                        }

                                        response.EnsureSuccessStatusCode();
                                    }
                                }
                            }

                            var newProductionComments = dbContext.ProductionComments.Where(entity => !entity.Syncronized).OrderBy(entity => entity.Created).Include(e => e.Client).Include(e => e.Production).Include(e => e.Reference).ToList();

                            if (newProductionComments.Count() > 0)
                            {
                                foreach (var current in newProductionComments)
                                {
                                    using (var content = new System.Net.Http.StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(new SAPRESSC.Business.Billboard.CPCC.Web.Api.Common.Models.CommentInputModel
                                    {
                                        Id = current.Id,
                                        ClientId = current.Client.Id,
                                        Message = current.Message,
                                        ReferenceId = current.Reference != null ? current.Reference.Id : System.Guid.Empty,
                                        Created = current.Created
                                    }), System.Text.Encoding.UTF8, "application/json"))
                                    {
                                        response = await client.PutAsync(string.Format("{0}/comments/production/{1}", apiBaseQuery, current.Production.Id), content);

                                        if (response.IsSuccessStatusCode)
                                        {
                                            current.Syncronized = true;

                                            dbContext.ProductionComments.Update(current);

                                            dbContext.SaveChanges("client");
                                        }

                                        response.EnsureSuccessStatusCode();
                                    }
                                }
                            }

                            var newPersonalityComments = dbContext.PersonalityComments.Where(entity => !entity.Syncronized).OrderBy(entity => entity.Created).Include(e => e.Client).Include(e => e.Personality).Include(e => e.Reference).ToList();

                            if (newPersonalityComments.Count() > 0)
                            {
                                foreach (var current in newPersonalityComments)
                                {
                                    using (var content = new System.Net.Http.StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(new SAPRESSC.Business.Billboard.CPCC.Web.Api.Common.Models.CommentInputModel
                                    {
                                        Id = current.Id,
                                        ClientId = current.Client.Id,
                                        Message = current.Message,
                                        ReferenceId = current.Reference != null ? current.Reference.Id : System.Guid.Empty,
                                        Created = current.Created
                                    }), System.Text.Encoding.UTF8, "application/json"))
                                    {
                                        response = await client.PutAsync(string.Format("{0}/comments/personality/{1}", apiBaseQuery, current.Personality.Id), content);

                                        if (response.IsSuccessStatusCode)
                                        {
                                            current.Syncronized = true;

                                            dbContext.PersonalityComments.Update(current);

                                            dbContext.SaveChanges("client");
                                        }

                                        response.EnsureSuccessStatusCode();
                                    }
                                }
                            }

                            try
                            {
                                var eventCommentDelations = dbContext.EventCommentDelations.Include(entity => entity.Client).Include(entity => entity.Comment).ToList();

                                foreach (var delation in eventCommentDelations)
                                {
                                    using (var content = new System.Net.Http.StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(new SAPRESSC.Business.Billboard.CPCC.Web.Api.Common.Models.CommentDelationInputModel
                                    {
                                        Id = delation.Id,
                                        Note = delation.Note,
                                        ClientId = delation.Client.Id,
                                        Created = delation.Created,
                                        Revoked = delation.Revoked
                                    }), System.Text.Encoding.UTF8, "application/json"))
                                    {
                                        response = await client.PutAsync(string.Format("{0}/delations/eventcomment/{1}", apiBaseQuery, delation.Comment.Id), content);

                                        response.EnsureSuccessStatusCode();
                                    }
                                }

                                var productionCommentDelations = dbContext.ProductionCommentDelations.Include(entity => entity.Client).Include(entity => entity.Comment).ToList();

                                foreach (var delation in productionCommentDelations)
                                {
                                    using (var content = new System.Net.Http.StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(new SAPRESSC.Business.Billboard.CPCC.Web.Api.Common.Models.CommentDelationInputModel
                                    {
                                        Id = delation.Id,
                                        Note = delation.Note,
                                        ClientId = delation.Client.Id,
                                        Created = delation.Created,
                                        Revoked = delation.Revoked
                                    }), System.Text.Encoding.UTF8, "application/json"))
                                    {
                                        response = await client.PutAsync(string.Format("{0}/delations/productioncomment/{1}", apiBaseQuery, delation.Comment.Id), content);

                                        response.EnsureSuccessStatusCode();
                                    }
                                }

                                var personalityCommentDelations = dbContext.PersonalityCommentDelations.Include(entity => entity.Client).Include(entity => entity.Comment).ToList();

                                foreach (var delation in personalityCommentDelations)
                                {
                                    using (var content = new System.Net.Http.StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(new SAPRESSC.Business.Billboard.CPCC.Web.Api.Common.Models.CommentDelationInputModel
                                    {
                                        Id = delation.Id,
                                        Note = delation.Note,
                                        ClientId = delation.Client.Id,
                                        Created = delation.Created,
                                        Revoked = delation.Revoked
                                    }), System.Text.Encoding.UTF8, "application/json"))
                                    {
                                        response = await client.PutAsync(string.Format("{0}/delations/personalitycomment/{1}", apiBaseQuery, delation.Comment.Id), content);

                                        response.EnsureSuccessStatusCode();
                                    }
                                }
                            }
                            catch (Microsoft.Data.Sqlite.SqliteException) { }
                            catch (System.Exception exception)
                            {
                                throw exception;
                            }
                        }
                    }
                    #endregion

                    Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                    {
                        reportProgressAction.Invoke(0, true, "Obteniendo datos ...");
                    });

                    response = await client.GetAsync("database/portable");

                    response.EnsureSuccessStatusCode();

                    var responseContentStream = await response.Content.ReadAsStreamAsync();

                    LocalFileStorageHelper.DeleteLocalFiles();

                    var databaseFileStream = System.IO.File.OpenWrite(LocalFileStorageHelper.GetDatabaseFilePath());

                    Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                    {
                        reportProgressAction.Invoke(0, true, "Salvando datos recibidos ...");
                    });

                    responseContentStream.CopyTo(databaseFileStream);

                    responseContentStream.Close();
                    databaseFileStream.Close();
                }

                using (var dbContext = Providers.DdContextProvider.GetDbContext())
                {
                    Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                    {
                        reportProgressAction.Invoke(0, true, "Sincronizando ...");
                    });

                    var self = new SAPRESSC.Business.Billboard.CPCC.Persistence.Entities.Client
                    {
                        Id = profile.Id,
                        FirstName = profile.FirstName,
                        LastName = profile.LastName,
                        Sex = profile.IsMale ? SAPRESSC.Middleware.Identity.Persistence.Enumerations.Sex.Male : SAPRESSC.Middleware.Identity.Persistence.Enumerations.Sex.Famale,
                        PhoneNumber = profile.Phone
                    };
                    if (!dbContext.Clients.Any(entity => entity.Id == profile.Id))
                    {
                        dbContext.Clients.Add(self);

                        dbContext.SaveChanges("system");
                    }

                    Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                    {
                        reportProgressAction.Invoke(0, true, "Obteniendo comentarios ...");
                    });

                    using (var client = new System.Net.Http.HttpClient())
                    {
                        client.BaseAddress = baseAddress;
                        client.DefaultRequestHeaders.Add(Providers.ConstantsProvider.API_KEY_HEADER_PROPERTY, Providers.ConstantsProvider.API_KEY_SECRET);
                        client.Timeout = System.TimeSpan.FromSeconds(30);

                        foreach (var personality in dbContext.Personalities)
                        {
                            response = await client.GetAsync(string.Format("{0}/comments/personality/{1}", apiBaseQuery, personality.Id));

                            response.EnsureSuccessStatusCode();

                            var personalityCommentModels = Newtonsoft.Json.JsonConvert.DeserializeObject<SAPRESSC.Business.Billboard.CPCC.Web.Api.Common.Models.PersonalityCommentModel[]>(await response.Content.ReadAsStringAsync());

                            foreach (var item in personalityCommentModels)
                            {
                                var comment = new SAPRESSC.Business.Billboard.CPCC.Persistence.Entities.PersonalityComment
                                {
                                    Id = item.Id,
                                    Created = item.Created,
                                    Banish = false,
                                    Syncronized = true,
                                    Message = item.Message,
                                    Client = new SAPRESSC.Business.Billboard.CPCC.Persistence.Entities.Client
                                    {
                                        Id = item.Client.Id,
                                        About = item.Client.About,
                                        BornDate = item.Client.BornDate,
                                        EmailAddress = item.Client.EmailAddress,
                                        PhoneNumber = item.Client.PhoneNumber,
                                        FirstName = item.Client.FirstName,
                                        LastName = item.Client.LastName,
                                        Sex = item.Client.IsMale ? SAPRESSC.Middleware.Identity.Persistence.Enumerations.Sex.Male : SAPRESSC.Middleware.Identity.Persistence.Enumerations.Sex.Famale
                                    },
                                    Personality = personality
                                };

                                if (item.ReferenceId != System.Guid.Empty)
                                {
                                    comment.Reference = dbContext.PersonalityComments.First(entity => entity.Id == item.ReferenceId);
                                }

                                dbContext.PersonalityComments.Add(comment);

                                dbContext.SaveChanges("system");
                            }
                        }

                        foreach (var production in dbContext.Productions)
                        {
                            response = await client.GetAsync(string.Format("{0}/comments/production/{1}", apiBaseQuery, production.Id));

                            response.EnsureSuccessStatusCode();

                            var productionCommentModels = Newtonsoft.Json.JsonConvert.DeserializeObject<SAPRESSC.Business.Billboard.CPCC.Web.Api.Common.Models.ProductionCommentModel[]>(await response.Content.ReadAsStringAsync());

                            foreach (var item in productionCommentModels)
                            {
                                var comment = new SAPRESSC.Business.Billboard.CPCC.Persistence.Entities.ProductionComment
                                {
                                    Id = item.Id,
                                    Created = item.Created,
                                    Banish = false,
                                    Syncronized = true,
                                    Message = item.Message,
                                    Client = new SAPRESSC.Business.Billboard.CPCC.Persistence.Entities.Client
                                    {
                                        Id = item.Client.Id,
                                        About = item.Client.About,
                                        BornDate = item.Client.BornDate,
                                        EmailAddress = item.Client.EmailAddress,
                                        PhoneNumber = item.Client.PhoneNumber,
                                        FirstName = item.Client.FirstName,
                                        LastName = item.Client.LastName,
                                        Sex = item.Client.IsMale ? SAPRESSC.Middleware.Identity.Persistence.Enumerations.Sex.Male : SAPRESSC.Middleware.Identity.Persistence.Enumerations.Sex.Famale
                                    },
                                    Production = production
                                };

                                if (item.ReferenceId != System.Guid.Empty)
                                {
                                    comment.Reference = dbContext.ProductionComments.First(entity => entity.Id == item.ReferenceId);
                                }

                                dbContext.ProductionComments.Add(comment);

                                dbContext.SaveChanges("system");
                            }
                        }

                        foreach (var @event in dbContext.Events)
                        {
                            response = await client.GetAsync(string.Format("{0}/comments/event/{1}", apiBaseQuery, @event.Id));

                            response.EnsureSuccessStatusCode();

                            var eventCommentModels = Newtonsoft.Json.JsonConvert.DeserializeObject<SAPRESSC.Business.Billboard.CPCC.Web.Api.Common.Models.EventCommentModel[]>(await response.Content.ReadAsStringAsync());

                            foreach (var item in eventCommentModels)
                            {
                                var comment = new SAPRESSC.Business.Billboard.CPCC.Persistence.Entities.EventComment
                                {
                                    Id = item.Id,
                                    Created = item.Created,
                                    Banish = false,
                                    Syncronized = true,
                                    Message = item.Message,
                                    Client = new SAPRESSC.Business.Billboard.CPCC.Persistence.Entities.Client
                                    {
                                        Id = item.Client.Id,
                                        About = item.Client.About,
                                        BornDate = item.Client.BornDate,
                                        EmailAddress = item.Client.EmailAddress,
                                        PhoneNumber = item.Client.PhoneNumber,
                                        FirstName = item.Client.FirstName,
                                        LastName = item.Client.LastName,
                                        Sex = item.Client.IsMale ? SAPRESSC.Middleware.Identity.Persistence.Enumerations.Sex.Male : SAPRESSC.Middleware.Identity.Persistence.Enumerations.Sex.Famale
                                    },
                                    Event = @event
                                };

                                if (item.ReferenceId != System.Guid.Empty)
                                {
                                    comment.Reference = dbContext.EventComments.First(entity => entity.Id == item.ReferenceId);
                                }

                                dbContext.EventComments.Add(comment);

                                dbContext.SaveChanges("system");
                            }
                        }

                        response = await client.GetAsync(string.Format("{0}/delations/eventcomment/client/{1}", apiBaseQuery, profile.Id));

                        response.EnsureSuccessStatusCode();

                        var eventCommentDelations = Newtonsoft.Json.JsonConvert.DeserializeObject<SAPRESSC.Business.Billboard.CPCC.Web.Api.Common.Models.CommentDelationModel[]>(await response.Content.ReadAsStringAsync());

                        foreach (var item in eventCommentDelations)
                        {
                            dbContext.EventCommentDelations.Add(new SAPRESSC.Business.Billboard.CPCC.Persistence.Entities.EventCommentDelation
                            {
                                Id = item.Id,
                                Created = item.Created,
                                Note = item.Note,
                                Client = self,
                                Comment = dbContext.EventComments.FirstOrDefault(entity => entity.Id == item.CommentId)
                            });
                        }

                        response = await client.GetAsync(string.Format("{0}/delations/productioncomment/client/{1}", apiBaseQuery, profile.Id));

                        response.EnsureSuccessStatusCode();

                        var productionCommentDelations = Newtonsoft.Json.JsonConvert.DeserializeObject<SAPRESSC.Business.Billboard.CPCC.Web.Api.Common.Models.CommentDelationModel[]>(await response.Content.ReadAsStringAsync());

                        foreach (var item in productionCommentDelations)
                        {
                            dbContext.ProductionCommentDelations.Add(new SAPRESSC.Business.Billboard.CPCC.Persistence.Entities.ProductionCommentDelation
                            {
                                Id = item.Id,
                                Created = item.Created,
                                Note = item.Note,
                                Client = self,
                                Comment = dbContext.ProductionComments.FirstOrDefault(entity => entity.Id == item.CommentId)
                            });
                        }

                        response = await client.GetAsync(string.Format("{0}/delations/personalitycomment/client/{1}", apiBaseQuery, profile.Id));

                        response.EnsureSuccessStatusCode();

                        var personalityCommentDelations = Newtonsoft.Json.JsonConvert.DeserializeObject<SAPRESSC.Business.Billboard.CPCC.Web.Api.Common.Models.CommentDelationModel[]>(await response.Content.ReadAsStringAsync());

                        foreach (var item in personalityCommentDelations)
                        {
                            dbContext.PersonalityCommentDelations.Add(new SAPRESSC.Business.Billboard.CPCC.Persistence.Entities.PersonalityCommentDelation
                            {
                                Id = item.Id,
                                Created = item.Created,
                                Note = item.Note,
                                Client = self,
                                Comment = dbContext.PersonalityComments.FirstOrDefault(entity => entity.Id == item.CommentId)
                            });
                        }

                        dbContext.SaveChanges("system");

                        var imageCount = dbContext.Images.Count();

                        Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                        {
                            reportProgressAction.Invoke(0, true, "Obteniendo imágenes ...");
                        });

                        var counter = 0;

                        foreach (var image in dbContext.Images)
                        {
                            Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                            {
                                reportProgressAction.Invoke(0, true, string.Format("Obteniendo imágenes ({0}/{1}) ...", ++counter, imageCount));
                            });

                            var uri = string.Format("media/default/image/{0}", image.Id);

                            response = await client.GetAsync(uri);

                            response.EnsureSuccessStatusCode();

                            using (var imageFileStream = System.IO.File.OpenWrite(LocalFileStorageHelper.GetImageFilePath(image.Id)))
                            {
                                using (var responseContentStream = await response.Content.ReadAsStreamAsync())
                                {
                                    responseContentStream.CopyTo(imageFileStream);
                                }
                            }

                            try
                            {
                                using (var imageFileStream = System.IO.File.OpenRead(LocalFileStorageHelper.GetImageFilePath(image.Id)))
                                {
                                    using (var skStream = new SkiaSharp.SKManagedStream(imageFileStream))
                                    {
                                        using (var skBitmap = SkiaSharp.SKBitmap.Decode(skStream))
                                        {
                                            if (skBitmap.Width > deviceScreenMinSize)
                                            {
                                                double dif = System.Convert.ToDouble(skBitmap.Height) / System.Convert.ToDouble(skBitmap.Width);

                                                var requiredHeight = System.Convert.ToInt32(System.Math.Round(dif * deviceScreenMinSize));

                                                using (var resizedSkBitmap = skBitmap.Resize(new SkiaSharp.SKImageInfo(deviceScreenMinSize, requiredHeight), SkiaSharp.SKFilterQuality.High))
                                                {
                                                    using (var skImage = SkiaSharp.SKImage.FromBitmap(resizedSkBitmap))
                                                    {
                                                        using (var output = System.IO.File.OpenWrite(LocalFileStorageHelper.GetResponsiveImageFilePath(image.Id, deviceScreenMinSize)))
                                                        {
                                                            skImage.Encode(SkiaSharp.SKEncodedImageFormat.Jpeg, 75).SaveTo(output);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            catch
                            {
                            }
                        }
                    }
                }

                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    reportProgressAction.Invoke(0, true, "Terminando ...");
                });

                syncronizationDate = System.DateTime.Now;

                Xamarin.Forms.Application.Current.Properties[DATABASE_UPDATE_DATETIME_KEY] = syncronizationDate.ToString();

                await Xamarin.Forms.Application.Current.SavePropertiesAsync();

                return true;
            }).ContinueWith(continueAction);
        }

        private System.Collections.Generic.List<string> GetBundleableDataFilePaths()
        {
            var result = new System.Collections.Generic.List<string>();

            using (var dbContext = Providers.DdContextProvider.GetDbContext())
            {
                foreach (var image in dbContext.Images)
                {
                    var path = LocalFileStorageHelper.GetImageFilePath(image.Id);

                    if (!System.IO.File.Exists(path)) continue;

                    result.Add(LocalFileStorageHelper.GetImageFilePath(image.Id));
                }
            }

            result.Add(LocalFileStorageHelper.GetDatabaseFilePath());

            var userProfile = Managers.ProfileManager.Instance.GetCurrentProfile();

            var userProfileImagePath = LocalFileStorageHelper.GetImageFilePath(userProfile.Id);

            if (System.IO.File.Exists(userProfileImagePath))
            {
                result.Add(userProfileImagePath);
            }

            return result;
        }
    }
}
