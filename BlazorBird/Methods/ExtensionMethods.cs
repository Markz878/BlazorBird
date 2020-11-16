using System.Globalization;

namespace BlazorBird.Methods
{
    public static class ExtensionMethods
    {
        public static string ToPixelString(this double value) => value.ToString(CultureInfo.InvariantCulture) + "px";
        public static string ToDegString(this double value) => value.ToString(CultureInfo.InvariantCulture) + "deg";
    }
}
