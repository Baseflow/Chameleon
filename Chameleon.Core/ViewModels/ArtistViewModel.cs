
using System.Collections.Generic;
using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;

namespace Chameleon.Core.ViewModels
{
    public class ArtistViewModel : BaseViewModel
    {
        public ArtistViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {

        }
    }
}
