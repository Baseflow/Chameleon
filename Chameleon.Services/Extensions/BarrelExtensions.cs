using MonkeyCache;

namespace Chameleon.Services.Extensions
{
    public static class BarrelExtensions
    {
        public static T Get<T>(this IBarrel source, string key, T defaultValue)
        {
            if (string.IsNullOrWhiteSpace(key) || !source.Exists(key))
                return defaultValue;

            return source.Get<T>(key);
        }
    }
}
