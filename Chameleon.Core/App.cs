using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using FFImageLoading.Config;
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
            // Register Connectivity
            //Mvx.IoCProvider.RegisterSingleton<IConnectivity>(CrossConnectivity.Current);
            //Mvx.IoCProvider.RegisterSingleton<IUserDialogs>(UserDialogs.Instance);

            // Register Text provider
            var textProviderBuilder = new TextProviderBuilder();
            Mvx.IoCProvider.RegisterSingleton<IMvxTextProviderBuilder>(textProviderBuilder);
            Mvx.IoCProvider.RegisterSingleton<IMvxTextProvider>(textProviderBuilder.TextProvider);

            /*var language = Mvx.IoCProvider.Resolve<ILanguageService>()?.GetLanguage()?.TwoLetterISOLanguageName;
            var textProviderBuilder = ((TextProviderBuilder)Mvx.IoCProvider.GetSingleton<IMvxTextProviderBuilder>());
            if (textProviderBuilder.CurrentLocalization != language)
                textProviderBuilder.LoadResources(language);*/

            FFImageLoading.ImageService.Instance.Initialize();

            RegisterCustomAppStart<AppStart>();
        }
    }
}
