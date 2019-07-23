using System;
using System.Collections.Generic;
using System.Text;
using MvvmCross.Logging;
using MvvmCross.Navigation;

namespace Chameleon.Core.ViewModels
{
    public class PlayerViewModel : BaseViewModel
    {
        public PlayerViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
        }
    }
}
