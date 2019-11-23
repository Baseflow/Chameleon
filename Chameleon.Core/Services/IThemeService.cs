using Chameleon.Core.Models;
using Xamarin.Forms;

namespace Chameleon.Core.Helpers
{
    public interface IThemeService
    {
        ResourceDictionary CustomColors { get; set; }
        ThemeMode AppTheme { get; set; }
        void UpdateTheme(ThemeMode themeMode = ThemeMode.Auto);
    }
}
