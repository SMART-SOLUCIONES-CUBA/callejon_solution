namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.UWP.Supporter.Extensions
{
    internal static class DriveInfoExtensions
    {
        internal static Api.Common.Models.DriveInfoModel ToApiModel(this System.IO.DriveInfo target)
        {
            return new Api.Common.Models.DriveInfoModel
            {
                AvailableFreeSpace = target.AvailableFreeSpace,
                DriveFormat = target.DriveFormat,
                DriveType = target.DriveType,
                IsReady = target.IsReady,
                Name = target.Name,
                TotalFreeSpace = target.TotalFreeSpace,
                TotalSize = target.TotalSize,
                VolumeLabel = target.VolumeLabel
            };
        }
    }
}
