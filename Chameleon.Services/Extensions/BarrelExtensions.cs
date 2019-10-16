using System;
using MonkeyCache;

namespace Chameleon.Services.Extensions
{
    public static class BarrelExtensions
    {
        public static T GetOrCreate<T>(this IBarrel barrel, string key, T defaultValue, TimeSpan timeSpan = default)
        {
            if (string.IsNullOrWhiteSpace(key) || !barrel.Exists(key))
            {
                if (timeSpan == default)
                    timeSpan = TimeSpan.MaxValue;
                barrel.Add(key, defaultValue, timeSpan);
                return defaultValue;
            }

            return barrel.Get<T>(key);
        }
    }
}
