using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using Acr.UserDialogs;
using Chameleon.Services.Services;
using FFImageLoading.Config;
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

            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<IPlaylistService, PlaylistService>();

            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            /*var language = Mvx.IoCProvider.Resolve<ILanguageService>()?.GetLanguage()?.TwoLetterISOLanguageName;
            var textProviderBuilder = ((TextProviderBuilder)Mvx.IoCProvider.GetSingleton<IMvxTextProviderBuilder>());
            if (textProviderBuilder.CurrentLocalization != language)
                textProviderBuilder.LoadResources(language);*/

            FFImageLoading.ImageService.Instance.Initialize();

            RegisterCustomAppStart<AppStart>();
        }
    }
}
