using System;
using System.Linq;
using Chameleon.Core.Helpers;
using Chameleon.Core.Resources;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using Xamarin.Forms;

namespace Chameleon.Core.ViewModels
{
    public class ThemingViewModel : BaseViewModel
    {
        private readonly IThemeService _themeService;

        public ThemingViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IThemeService themeService) : base(logProvider, navigationService)
        {
            _themeService = themeService;
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

        private IMvxCommand _themeAutoCommand;
        public IMvxCommand ThemeAutoCommand => _themeAutoCommand ?? (_themeAutoCommand = new MvxCommand(ThemeAuto));

        private IMvxCommand _themeDarkCommand;
        public IMvxCommand ThemeDarkCommand => _themeDarkCommand ?? (_themeDarkCommand = new MvxCommand(ThemeDark));

        private IMvxCommand _themeLightCommand;
        public IMvxCommand ThemeLigthCommand => _themeLightCommand ?? (_themeLightCommand = new MvxCommand(ThemeLight));

        private IMvxCommand _themeCustomCommand;
        public IMvxCommand ThemeCustomCommand => _themeCustomCommand ?? (_themeCustomCommand = new MvxCommand(ThemeCustom));

        private void ThemeAuto()
        {
            _themeService.AppTheme = Models.ThemeMode.Auto;
            _themeService.UpdateTheme();
            UpdateThemeImages();
        }

        private void ThemeDark()
        {
            _themeService.AppTheme = Models.ThemeMode.Dark;
            _themeService.UpdateTheme();
            UpdateThemeImages();
        }

        private void ThemeLight()
        {
            _themeService.AppTheme = Models.ThemeMode.Light;
            _themeService.UpdateTheme();
            UpdateThemeImages();
        }

        private void ThemeCustom()
        {
            _themeService.AppTheme = Models.ThemeMode.Custom;

            var colors = _themeService.CustomColors ?? new DarkColors();
            colors["PrimaryBackgroundColor"] = Color.FromHex("#FF30313C");

            _themeService.CustomColors = colors;
            _themeService.UpdateTheme();
            UpdateThemeImages();
        }

        private void UpdateThemeImages()
        {
            switch (_themeService.AppTheme)
            {
                case Models.ThemeMode.Auto:
                    break;
                case Models.ThemeMode.Dark:
                    ThemeLightImage = ImageSource.FromFile("theme_light");
                    ThemeDarkImage = ImageSource.FromFile("theme_dark_on");
                    break;
                case Models.ThemeMode.Light:
                    ThemeLightImage = ImageSource.FromFile("theme_light_on");
                    ThemeDarkImage = ImageSource.FromFile("theme_dark");
                    break;
                case Models.ThemeMode.Custom:
                    break;
                default:
                    break;
            }
        }

        public override void ViewAppearing()
        {
            base.ViewAppearing();

            UpdateThemeImages();
        }
    }
}
