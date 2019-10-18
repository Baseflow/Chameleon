using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chameleon.Services.Models;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;

namespace Chameleon.Core.ViewModels
{
    public class ProviderViewModel : BaseViewModel<Provider>
    {
        public ProviderViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
        }

        public override void Prepare(Provider parameter)
        {

        }
    }
}
