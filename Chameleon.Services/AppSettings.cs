using System;

namespace Chameleon.Services
{
    public static class AppSettings
    {
        public static readonly string AppName = "Chameleon";
        public static readonly string AppIdentifier = "com.baseflow.chameleon";

        public static readonly string AndroidAppcenterSecret = "APP_SECRET_ANDROID";
        public static readonly string IosAppcenterSecret = "APP_SECRET_IOS";
        public static readonly string UwpAppcenterSecret = "APP_SECRET_UWP";

        public static readonly string[] AppLanguages = { "en" /*, "nl"*/ };
        public static readonly string DefaultAppLanguage = "en";

        // Cache specific settings
        public static readonly TimeSpan DefaultCacheExpiryTimeSpan = TimeSpan.FromDays(7);
        public static readonly TimeSpan DefaultShortCacheExpiryTimeSpan = TimeSpan.FromMinutes(15);
        public static readonly TimeSpan DefaultDayCacheExpiryTimeSpan = TimeSpan.FromDays(1);
        public static readonly TimeSpan DefaultLongCacheExpiryTimeSpan = TimeSpan.FromDays(30);
        public static readonly TimeSpan DefaultInfiniteCacheExpiryTimeSpan = TimeSpan.FromDays(365);

        // Text Provider settings
        public static readonly string TextProviderNamespace = "Chameleon.Core";
        public static readonly string TextProviderSharedTypeKey = "Shared";

        public static readonly string ThemeKey = "Theme";
        public static readonly string CustomColorsKey = "CustomColors";
    }
}
