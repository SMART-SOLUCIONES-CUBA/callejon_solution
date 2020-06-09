namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Extensions
{
    internal static class StringExtensions
    {
        internal static string ToFriendlyDeviceModel(this string target)
        {
            // TODO Complete implementation!

            if (target.StartsWith("iPhone"))
            {
                switch (target)
                {
                    case "iPhone8,1":
                        return "iPhone 6 S";
                }
            }

            return target;
        }
    }
}
