using Acr.UserDialogs;
using Chameleon.Core.Helpers;
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
            Mvx.IoCProvider.RegisterSingleton<IMediaManager>(CrossMediaManager.Current);

            // Register Text provider
            var textProviderBuilder = new TextProviderBuilder();
            Mvx.IoCProvider.RegisterSingleton<IMvxTextProviderBuilder>(textProviderBuilder);
            Mvx.IoCProvider.RegisterSingleton<IMvxTextProvider>(textProviderBuilder.TextProvider);

            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            CrossMediaManager.Current.Library.Providers.Add(Mvx.IoCProvider.IoCConstruct<PlaylistProvider>());

            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<IBrowseService, BrowseService>();
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<ISettingsService, SettingsService>();
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<IThemeService, ThemeService>();

            /*var language = Mvx.IoCProvider.Resolve<ILanguageService>()?.GetLanguage()?.TwoLetterISOLanguageName;
            var textProviderBuilder = ((TextProviderBuilder)Mvx.IoCProvider.GetSingleton<IMvxTextProviderBuilder>());
            if (textProviderBuilder.CurrentLocalization != language)
                textProviderBuilder.LoadResources(language);*/

            FFImageLoading.ImageService.Instance.Initialize();

            RegisterCustomAppStart<AppStart>();
        }
    }
}
