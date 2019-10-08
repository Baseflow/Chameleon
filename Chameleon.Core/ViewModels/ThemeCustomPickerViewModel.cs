using System;
using Chameleon.Core.Helpers;
using MvvmCross.Logging;
using MvvmCross.Navigation;

namespace Chameleon.Core.ViewModels
{
    public class ThemeCustomPickerViewModel : BaseViewModel
    {
        public ThemeCustomPickerViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
        }
    }
}
