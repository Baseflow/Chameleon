using System;
using Chameleon.Core.Helpers;
using Chameleon.Core.Resources;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using Xamarin.Forms;

namespace Chameleon.Core.ViewModels
{
    public class ThemeCustomPickerViewModel : BaseViewModel
    {
        private readonly IThemeService _themeService;

        public ThemeCustomPickerViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IThemeService themeService) : base(logProvider, navigationService)
        {
            _themeService = themeService ?? throw new ArgumentNullException(nameof(themeService));
        }

        public Color BackgroundColorButton
        {
            get
            {
                var backgroundColor = _themeService.CustomColors["PrimaryBackgroundColor"];
                if (backgroundColor == null)
                    return Color.Default;

                return (Color)backgroundColor;
            }
            set
            {
                _themeService.CustomColors["PrimaryBackgroundColor"] = value;
                RaisePropertyChanged(nameof(BackgroundColorButton));
            }
        }

        public Color PrimaryColorButton
        {
            get
            {
                var primaryColor = _themeService.CustomColors["PrimaryColor"];
                if (primaryColor == null)
                {
                    return Color.Default;
                }
                return (Color)primaryColor;
            }
            set
            {
                _themeService.CustomColors["PrimaryColor"] = value;
                RaisePropertyChanged(nameof(PrimaryColorButton));
            }
        }

        public Color TextColorButton
        {
            get
            { 
                var textColor = _themeService.CustomColors["PrimaryTextColor"];
                if (textColor == null)
                {
                    return Color.Default;
                }
                return (Color)textColor;
            }
            set
            {
                _themeService.CustomColors["PrimaryTextColor"] = value; 
                RaisePropertyChanged(nameof(TextColorButton));
            }
        }

        private IMvxCommand _themeCustomCommand;
        public IMvxCommand ThemeCustomCommand => _themeCustomCommand ?? (_themeCustomCommand = new MvxCommand(ThemeCustom));

        private void ThemeCustom()
        {
            _themeService.AppTheme = Models.ThemeMode.Custom;

            var colors = _themeService.CustomColors ?? new DarkColors();
            colors["PrimaryBackgroundColor"] = Color.FromHex("#FF30313C");
            colors["SecondaryBackgroundColor"] = Color.FromHex("#FF393A47");
            colors["PrimaryColor"] = Color.FromHex("#FF252525");
            colors["SecondaryColor"] = Color.FromHex("#FFDA3434");
            colors["TertiaryColor"] = Color.FromHex("#FFE3E5F6");
            colors["PrimaryTextColor"] = Color.FromHex("#FFFFFFFF");
            colors["SecondaryTextColor"] = Color.FromHex("#FF6C6E81");
            colors["TertiaryTextColor"] = Color.FromHex("#FFE3E5F6");

            _themeService.CustomColors = colors;
            _themeService.UpdateTheme();
        }
    }
}
