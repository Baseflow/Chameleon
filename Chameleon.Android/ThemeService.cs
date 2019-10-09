using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Chameleon.Core.Helpers;
using Chameleon.Core.Models;

namespace Chameleon.Android
{
    public class ThemeService : ThemeServiceBase
    {
        public ThemeService(MonkeyCache.IBarrel barrel) : base(barrel)
        {
        }

        public override void UpdateTheme(ThemeMode themeMode = ThemeMode.Auto)
        {
            base.UpdateTheme(themeMode);

            switch (AppTheme)
            {
                case ThemeMode.Auto:
                    AppCompatDelegate.DefaultNightMode = AppCompatDelegate.ModeNightAuto;
                    break;
                case ThemeMode.Dark:
                    AppCompatDelegate.DefaultNightMode = AppCompatDelegate.ModeNightYes;
                    break;
                case ThemeMode.Light:
                    AppCompatDelegate.DefaultNightMode = AppCompatDelegate.ModeNightNo;
                    break;
                case ThemeMode.Custom:
                    AppCompatDelegate.DefaultNightMode = AppCompatDelegate.ModeNightYes;
                    break;
                default:
                    break;
            }
        }
    }
}
