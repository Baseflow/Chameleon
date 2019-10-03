using Chameleon.Core.Models;

namespace Chameleon.Core.Helpers
{
    public interface IThemeService
    {
        Xamarin.Forms.ResourceDictionary CustomColors { get; set; }
        ThemeMode AppTheme { get; set; }

        void ThemeCustom();
        void ThemeDark();
        void ThemeLight();
        void UpdateTheme(ThemeMode themeMode = ThemeMode.Auto);
    }
}
