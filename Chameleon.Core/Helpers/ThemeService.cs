using System;
using Chameleon.Core.Resources;
using Xamarin.Forms;

namespace Chameleon.Core.Helpers
{
    public class ThemeService : IThemeService
    {
        public void ThemeDark()
        {
            Application.Current.Resources.Clear();
            var style = new Styles();
            style.MergedDictionaries.Add(new DarkColors());

            Application.Current.Resources = style;
        }

        public void ThemeLight()
        {
            Application.Current.Resources.Clear();
            var style = new Styles();
            style.MergedDictionaries.Add(new LightColors());

            Application.Current.Resources = style;
        }
    }
}
