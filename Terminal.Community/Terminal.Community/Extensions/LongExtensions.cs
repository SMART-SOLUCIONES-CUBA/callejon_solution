namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Extensions
{
    public static class LongExtensions
    {
        public static int ToSelfPercent(this long target, double percent)
        {
            double selfPercent = target * percent;

            var intPercent = System.Convert.ToInt32(selfPercent);

            return selfPercent > intPercent ? intPercent + 1 : intPercent;
        }

        public static string ToFileSizeString(this long target)
        {
            decimal pivote = target;

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
