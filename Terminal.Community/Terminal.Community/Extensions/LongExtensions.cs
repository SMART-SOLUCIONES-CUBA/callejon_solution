namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Extensions
{
    internal static class LongExtensions
    {
        internal static string ToFileSizeString(this long value)
        {
            decimal pivote = value;
            var iterationCount = 0;

            while (pivote > 1024)
            {
                pivote /= 1024;
                iterationCount++;
            }

            pivote = System.Math.Round(pivote, 2);

            switch (iterationCount)
            {
                case 1:
                    return pivote + " KB";
                case 2:
                    return pivote + " MB";
                case 3:
                    return pivote + " GB";
                case 4:
                    return pivote + " TB";
                default:
                    return pivote + " B";
            }
        }
    }
}
