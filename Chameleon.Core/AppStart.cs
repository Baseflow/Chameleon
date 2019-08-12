using System.Threading.Tasks;
using Chameleon.Core.ViewModels;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Chameleon.Core
{
    public class AppStart : MvxAppStart
    {
        public AppStart(IMvxApplication application, IMvxNavigationService navigationService) : base(application, navigationService)
        {
        }

        protected override async Task NavigateToFirstViewModel(object hint = null)
        {
            await NavigationService.Navigate<RootViewModel>();
        }
    }
}
