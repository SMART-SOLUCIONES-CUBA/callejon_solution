namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Factories
{
    public static class FriendDataModelFactory
    {
        public static Models.Data.FriendDataModel CreateSelf()
        {
            var userProfile = Managers.ProfileManager.Instance.GetCurrentProfile();

            var result = new Models.Data.FriendDataModel
            {
                Id = userProfile.Id,
                FirstName = userProfile.FirstName,
                LastName = userProfile.LastName,
                IsMale = userProfile.IsMale,
                DataUpdatedAt = Helpers.DataSyncronizationHelper.Instance.SyncronizationDate,
                Terminal = new Models.Data.TerminalDataModel
                {
                    Manufacturer = Xamarin.Essentials.DeviceInfo.Manufacturer,
                    Model = Xamarin.Essentials.DeviceInfo.Model,
                    Name = Xamarin.Essentials.DeviceInfo.Name,
                    Platform = Xamarin.Essentials.DeviceInfo.Platform.ToString(),
                    VersionString = Xamarin.Essentials.DeviceInfo.VersionString
                }
            };

            var filePath = Helpers.LocalFileStorageHelper.GetImageFilePath(userProfile.Id);

            if (System.IO.File.Exists(filePath))
            {
                using (var imageStream = new System.IO.FileStream(filePath, System.IO.FileMode.Open))
                {
                    using (var imageSkData = SkiaSharp.SKData.Create(imageStream))
                    {
                        using (var imageSkBitmap = SkiaSharp.SKBitmap.Decode(imageSkData))
                        {
                            using (var thumbnailSkBitmap = imageSkBitmap.Resize(new SkiaSharp.SKImageInfo(128, 128), SkiaSharp.SKFilterQuality.High))
                            {
                                using (var thumbnailSkImage = SkiaSharp.SKImage.FromBitmap(thumbnailSkBitmap))
                                {
                                    var thumbnailSkData = thumbnailSkImage.Encode(SkiaSharp.SKEncodedImageFormat.Jpeg, 75);

                                    result.ImageData = thumbnailSkData.ToArray();
                                }
                            }
                        }
                    }
                }
            }

            return result;
        }
    }
}
