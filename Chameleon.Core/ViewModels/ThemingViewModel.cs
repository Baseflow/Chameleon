using System;
using Chameleon.Core.Helpers;
using Chameleon.Core.Resources;
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
            _themeService = themeService ?? throw new ArgumentNullException(nameof(themeService));
        }

        private ImageSource _themeDarkImage = ImageSource.FromFile("theme_dark.png");
        public ImageSource ThemeDarkImage
        {
            get => _themeDarkImage;
            set => SetProperty(ref _themeDarkImage, value);
        }

        private ImageSource _themeLightImage = ImageSource.FromFile("theme_light.png");
        public ImageSource ThemeLightImage
        {
            get => _themeLightImage;
            set => SetProperty(ref _themeLightImage, value);
        }

        private ImageSource _themeAutoImage = ImageSource.FromFile("theme_auto.png");
        public ImageSource ThemeAutoImage
        {
            get => _themeAutoImage;
            set => SetProperty(ref _themeAutoImage, value);
        }

        private ImageSource _themeCustomImage = ImageSource.FromFile("radio_button_off.png");
        public ImageSource ThemeCustomImage
        {
            get => _themeCustomImage;
            set => SetProperty(ref _themeCustomImage, value);
        }

        private IMvxCommand _themeAutoCommand;
        public IMvxCommand ThemeAutoCommand => _themeAutoCommand ?? (_themeAutoCommand = new MvxCommand(ThemeAuto));

        private IMvxCommand _themeDarkCommand;
        public IMvxCommand ThemeDarkCommand => _themeDarkCommand ?? (_themeDarkCommand = new MvxCommand(ThemeDark));

        private IMvxCommand _themeLightCommand;
        public IMvxCommand ThemeLigthCommand => _themeLightCommand ?? (_themeLightCommand = new MvxCommand(ThemeLight));

        private IMvxCommand _themeCustomCommand;
        public IMvxCommand ThemeCustomCommand => _themeCustomCommand ?? (_themeCustomCommand = new MvxCommand(ThemeCustom));

        private IMvxAsyncCommand _customPickerCommand;
        public IMvxAsyncCommand CustomPickerCommand => _customPickerCommand ?? (_customPickerCommand = new MvxAsyncCommand(() => NavigationService.Navigate<ThemeCustomPickerViewModel>()));

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
            _themeService.UpdateTheme();
            UpdateThemeImages();
        }

        private void UpdateThemeImages()
        {
            switch (_themeService.AppTheme)
            {
                case Models.ThemeMode.Auto:
                    ThemeAutoImage = ImageSource.FromFile("theme_auto_on.png");
                    ThemeLightImage = ImageSource.FromFile("theme_light.png");
                    ThemeDarkImage = ImageSource.FromFile("theme_dark.png");
                    ThemeCustomImage = ImageSource.FromFile("radio_button_off.png");
                    break;
                case Models.ThemeMode.Dark:
                    ThemeLightImage = ImageSource.FromFile("theme_light.png");
                    ThemeDarkImage = ImageSource.FromFile("theme_dark_on.png");
                    ThemeAutoImage = ImageSource.FromFile("theme_auto.png");
                    ThemeCustomImage = ImageSource.FromFile("radio_button_off.png");
                    break;
                case Models.ThemeMode.Light:
                    ThemeLightImage = ImageSource.FromFile("theme_light_on.png");
                    ThemeDarkImage = ImageSource.FromFile("theme_dark.png");
                    ThemeAutoImage = ImageSource.FromFile("theme_auto.png");
                    ThemeCustomImage = ImageSource.FromFile("radio_button_off.png");
                    break;
                case Models.ThemeMode.Custom:
                    ThemeLightImage = ImageSource.FromFile("theme_light.png");
                    ThemeDarkImage = ImageSource.FromFile("theme_dark.png");
                    ThemeAutoImage = ImageSource.FromFile("theme_auto.png");
                    ThemeCustomImage = ImageSource.FromFile("radio_button_on.png");
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
