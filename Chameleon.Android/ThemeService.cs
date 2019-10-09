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
using MvvmCross;
using MvvmCross.Platforms.Android;

namespace Chameleon.Android
{
    public class ThemeService : ThemeServiceBase
    {
        public ThemeService(MonkeyCache.IBarrel barrel) : base(barrel)
        {
        }

        Activity Activity => Mvx.IoCProvider.Resolve<IMvxAndroidCurrentTopActivity>().Activity;

        public override void UpdateTheme(ThemeMode themeMode = ThemeMode.Auto)
        {
            base.UpdateTheme(themeMode);

            bool changed = false;
            switch (AppTheme)
            {
                case ThemeMode.Auto:
                    if (AppCompatDelegate.DefaultNightMode != AppCompatDelegate.ModeNightFollowSystem)
                    {
                        AppCompatDelegate.DefaultNightMode = AppCompatDelegate.ModeNightFollowSystem;
                        changed = true;
                    }
                    break;
                case ThemeMode.Dark:
                    if (AppCompatDelegate.DefaultNightMode != AppCompatDelegate.ModeNightYes)
                    {
                        AppCompatDelegate.DefaultNightMode = AppCompatDelegate.ModeNightYes;
                        changed = true;
                    }
                    break;
                case ThemeMode.Light:
                    if (AppCompatDelegate.DefaultNightMode != AppCompatDelegate.ModeNightNo)
                    {
                        AppCompatDelegate.DefaultNightMode = AppCompatDelegate.ModeNightNo;
                        changed = true;
                    }
                    break;
                case ThemeMode.Custom:
                    if (AppCompatDelegate.DefaultNightMode != AppCompatDelegate.ModeNightFollowSystem)
                    {
                        AppCompatDelegate.DefaultNightMode = AppCompatDelegate.ModeNightFollowSystem;
                        changed = true;
                    }
                    break;
                default:
                    break;
            }
            if (Activity?.Theme != null && changed)
            {
                (Activity as MainActivity)?.Delegate.ApplyDayNight();
                Activity.Recreate();
            }
        }
    }
}
