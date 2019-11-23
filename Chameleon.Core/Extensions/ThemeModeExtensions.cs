using Chameleon.Core.Models;
using Chameleon.Core.Resources;
using Xamarin.Forms;

namespace Chameleon.Core.Extensions
{
    public static class ThemeModeExtensions
    {
        public static ResourceDictionary ToResourceDictionary(this ThemeMode mode, ResourceDictionary customColors = default)
        { 
            switch (mode)
            {
                case ThemeMode.Dark:
                    return new DarkColors();
                case ThemeMode.Light:
                    return new LightColors();
                default:
                    return customColors;
            }
        }
    }
}
