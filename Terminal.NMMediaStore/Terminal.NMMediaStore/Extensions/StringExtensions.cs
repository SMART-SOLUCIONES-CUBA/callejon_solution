namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Extensions
{
    public static class StringExtensions
    {
        public static string ToImageExtension(this string source)
        {
            switch (source)
            {
                case "image/jpeg":
                    return ".jpg";

                default:
                    return string.Format(".{0}", source.Split('/')[1].ToLowerInvariant());
            }
        }
    }
}
