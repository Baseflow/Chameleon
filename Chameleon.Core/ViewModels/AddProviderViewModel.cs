using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;

namespace Chameleon.Core.ViewModels
{
    public class AddProviderViewModel : BaseViewModel
    {
        public AddProviderViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
        }
    }
}
