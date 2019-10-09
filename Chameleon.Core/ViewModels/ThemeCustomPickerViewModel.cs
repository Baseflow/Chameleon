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
