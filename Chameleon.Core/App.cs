using Acr.UserDialogs;
using Chameleon.Core.Helpers;
using Chameleon.Services;
using Chameleon.Services.Providers;
using Chameleon.Services.Services;
using MediaManager;
using MonkeyCache;
using MonkeyCache.LiteDB;
using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.Localization;
using MvvmCross.Plugin.JsonLocalization;
using MvvmCross.ViewModels;

namespace Chameleon.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            Barrel.ApplicationId = "Chameleon";

            Mvx.IoCProvider.RegisterSingleton<IBarrel>(Barrel.Current);
            Mvx.IoCProvider.RegisterSingleton<IUserDialogs>(UserDialogs.Instance);

            // Register Text provider
            var textProviderBuilder = new TextProviderBuilder();
            Mvx.IoCProvider.RegisterSingleton<IMvxTextProviderBuilder>(textProviderBuilder);
            var textProvider = textProviderBuilder.TextProvider;
            Mvx.IoCProvider.RegisterSingleton<IMvxTextProvider>(textProvider);

            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            var mediaManager = CrossMediaManager.Current;
            Mvx.IoCProvider.RegisterSingleton<IMediaManager>(mediaManager);

            var playlists = Mvx.IoCProvider.IoCConstruct<PlaylistProvider>();
            
            var exoplayer = Mvx.IoCProvider.IoCConstruct<ExoplayerProvider>();
            exoplayer.Title = textProvider.GetText(AppSettings.TextProviderNamespace, "ProvidersOverviewViewModel", "Exoplayer");

            var urlSources = Mvx.IoCProvider.IoCConstruct<UrlSourcesProvider>();
            urlSources.Title = textProvider.GetText(AppSettings.TextProviderNamespace, "ProvidersOverviewViewModel", "UrlSource");

            var radioStations = Mvx.IoCProvider.IoCConstruct<RadioStationProvider>();
            radioStations.Title = textProvider.GetText(AppSettings.TextProviderNamespace, "ProvidersOverviewViewModel", "RadioStations");

            mediaManager.Library.Providers.Add(playlists);
            mediaManager.Library.Providers.Add(exoplayer);
            mediaManager.Library.Providers.Add(urlSources);
            mediaManager.Library.Providers.Add(radioStations);

            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<IBrowseService, BrowseService>();
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<ISettingsService, SettingsService>();
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<IThemeService, ThemeServiceBase>();

            /*var language = Mvx.IoCProvider.Resolve<ILanguageService>()?.GetLanguage()?.TwoLetterISOLanguageName;
            var textProviderBuilder = ((TextProviderBuilder)Mvx.IoCProvider.GetSingleton<IMvxTextProviderBuilder>());
            if (textProviderBuilder.CurrentLocalization != language)
                textProviderBuilder.LoadResources(language);*/

            FFImageLoading.ImageService.Instance.Initialize();
            Mvx.IoCProvider.Resolve<IThemeService>().UpdateTheme();

            RegisterCustomAppStart<AppStart>();
        }
    }
}
