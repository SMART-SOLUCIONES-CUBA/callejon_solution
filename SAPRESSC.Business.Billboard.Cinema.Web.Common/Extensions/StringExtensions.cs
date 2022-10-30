namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Common.Extensions
{
    public static class StringExtensions
    {
        public static string ContentTypeToExtension(this string source)
        {
            switch (source)
            {
                case "image/jpeg":
                    return "jpg";

                default:
                    return source.Split('/')[1].ToLowerInvariant();
            }
        }

        public static string ToHashedPassword(this string plainPassword)
        {
            var hasher = System.Security.Cryptography.SHA256.Create();

            var originalBytes = System.Text.Encoding.ASCII.GetBytes(plainPassword);

            var encodedBytes = hasher.ComputeHash(originalBytes);

            return System.BitConverter.ToString(encodedBytes).Replace("-", "").ToLower();
        }
    }
}
