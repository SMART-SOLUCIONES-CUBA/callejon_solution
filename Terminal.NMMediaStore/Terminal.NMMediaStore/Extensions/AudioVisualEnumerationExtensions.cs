namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Extensions
{
    public static class AudioVisualEnumerationExtensions
    {
        public static long ToStorageSpace(this System.Collections.Generic.IEnumerable<SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.AudioVisualModel> target)
        {
            var result = 0L;

            foreach (var item in target)
            {
                result += item.FileSize;
            }

            return result;
        }
    }
}
