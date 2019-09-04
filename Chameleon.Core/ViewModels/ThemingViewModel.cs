using MvvmCross.Logging;
using MvvmCross.Navigation;
using MediaManager.Queue;
using MvvmCross.Commands;
using Xamarin.Forms;
using System;
using System.Collections.Generic;
using Chameleon.Core.Resources;

namespace Chameleon.Core.ViewModels
{
    public class ThemingViewModel : BaseViewModel
    {
        public ThemingViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
        }

        private ImageSource _themeDarkImage = ImageSource.FromFile("theme_dark");
        public ImageSource ThemeDarkImage
        {
            get => _themeDarkImage;
            set => SetProperty(ref _themeDarkImage, value);
        }

        private ImageSource _themeLightImage = ImageSource.FromFile("theme_light");
        public ImageSource ThemeLightImage
        {
            get => _themeLightImage;
            set => SetProperty(ref _themeLightImage, value);
        }

        private IMvxCommand _themeDarkCommand;
        public IMvxCommand ThemeDarkCommand => _themeDarkCommand ?? (_themeDarkCommand = new MvxCommand(ThemeDark));

        private IMvxCommand _themeLightCommand;
        public IMvxCommand ThemeLigthCommand => _themeLightCommand ?? (_themeLightCommand = new MvxCommand(ThemeLight));

        private void ThemeDark()
        {
            ICollection<ResourceDictionary> mergedDictionaries = Application.Current.Resources.MergedDictionaries;
            if (mergedDictionaries != null)
            {
                mergedDictionaries.Clear();

                mergedDictionaries.Add(new DarkTheme());
                ThemeDarkImage = ImageSource.FromFile("theme_dark_on");
                RaisePropertyChanged();
            }
        }

        private void ThemeLight()
        {
            ICollection<ResourceDictionary> mergedDictionaries = Application.Current.Resources.MergedDictionaries;
            if (mergedDictionaries != null)
            {
                mergedDictionaries.Clear();

                mergedDictionaries.Add(new LightTheme());
                ThemeLightImage = ImageSource.FromFile("theme_light_on");
                RaiseAllPropertiesChanged();
            }
        }

        //public enum Themes
        //{
        //    Light,
        //    Dark
        //}

        public override void ViewAppearing()
        {
            base.ViewAppearing();
            //Resources.DarkTheme;

            //var AppRequestedTheme = App.Current.RequestedTheme;
            //if (AppRequestedTheme == "Light")
            resources = new Xamarin.Forms.ResourceDictionary .DarkThemeResources();
            if (Resources.GetType() == typeof(DarkThemeResources))
                if (true)
            {
                FavoriteImage = ImageSource.FromFile("playback_controls_favorite_off");
            }
            else
            {
                FavoriteImage = ImageSource.FromFile("playback_controls_favorite_on");
            }
        }



    }
}
