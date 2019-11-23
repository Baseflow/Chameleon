using System;
using Chameleon.Core.Extensions;
using Chameleon.Core.Models;
using Chameleon.Core.Resources;
using Chameleon.Services;
using MonkeyCache;
using Xamarin.Forms;

namespace Chameleon.Core.Helpers
{
    public class ThemeServiceBase : IThemeService
    {
        private readonly IBarrel _barrel;

        public ThemeServiceBase(IBarrel barrel)
        {
            _barrel = barrel ?? throw new ArgumentNullException(nameof(barrel));
        }

        public ThemeMode AppTheme
        {
            get => _barrel.Get<ThemeMode>(AppSettings.ThemeKey);
            set => _barrel.Add(AppSettings.ThemeKey, value, TimeSpan.MaxValue);
        }

        public ResourceDictionary CustomColors
        {
            get => _barrel.Get<ResourceDictionary>(AppSettings.CustomColorsKey) ?? new LightColors();
            set => _barrel.Add(AppSettings.CustomColorsKey, value, TimeSpan.MaxValue);
        }

        public ThemeMode CurrentRuntimeTheme { get; private set; }

        public virtual void UpdateTheme(ThemeMode themeMode = ThemeMode.Auto)
        {
            switch (AppTheme)
            {
                case ThemeMode.Auto:
                    if (themeMode == ThemeMode.Dark)
                        goto case ThemeMode.Dark;
                    else
                        goto case ThemeMode.Light;
                case ThemeMode.Dark:
                    SetTheme(ThemeMode.Dark);
                    break;
                case ThemeMode.Light:
                    SetTheme(ThemeMode.Light);
                    break;
                case ThemeMode.Custom:
                    SetTheme(ThemeMode.Custom);
                    break;
                default:
                    break;
            }
        }

        private void SetTheme(ThemeMode themeMode)
        {
            if (CurrentRuntimeTheme == themeMode)
                return;

            SetColors(themeMode);

            CurrentRuntimeTheme = themeMode;
        }

        private void SetColors(ThemeMode themeMode)
        {
            var colors = themeMode.ToResourceDictionary(CustomColors);

            Application.Current.Resources.Clear();
            var style = new Styles();
            style.MergedDictionaries.Add(colors);

            Application.Current.Resources = style;
        }
    }
}
