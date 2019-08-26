using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chameleon.Services.Models;
using MediaManager.Library;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Chameleon.Core.ViewModels
{
    public class ProvidersViewModel : BaseViewModel
    {
        public ProvidersViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
        }

        private Provider _selectedItem;
        public Provider SelectedItem
        {
            get => _selectedItem;
            set => SetProperty(ref _selectedItem, value);
        }

        public string SoonText
        {
            get
            {
                var text = GetText("Soon");
                return text;
            }
        }

        private IList<Provider> _recommendedProviders;
        public IList<Provider> RecommendedProviders
        {
            get => _recommendedProviders;
            set => SetProperty(ref _recommendedProviders, value);
        }

        private IList<Provider> _providers;
        public IList<Provider> Providers
        {
            get => _providers;
            set => SetProperty(ref _providers, value);
        }

        public override void Prepare()
        {
            base.Prepare();
            Providers = new List<Provider>() {
                new Provider(){ Title = "URL Source" },
                new Provider(){ Title = "ExoPlayer samples" },
                new Provider(){ Title = "Internet Radio", Soon = true  },
                new Provider(){ Title = "Podcasts", Soon = true  },
                new Provider(){ Title = "Youtube", Soon = true },
                new Provider(){ Title = "Spotify", Soon = true  },
                new Provider(){ Title = "Tidal", Soon = true  },
                new Provider(){ Title = "Soundcloud", Soon = true  }
            };

            RecommendedProviders = Providers.Where(x => !x.Soon).ToList();
        }

        private IMvxAsyncCommand<Provider> _sourceCommand;
        public IMvxAsyncCommand<Provider> SourceCommand => _sourceCommand ?? (_sourceCommand = new MvxAsyncCommand<Provider>(OpenProvider));

        private async Task OpenProvider(Provider provider)
        {
            //await NavigationService.Navigate<SourceViewModel, Provider>(SelectedItem);
            SelectedItem = null;
        }
    }
}
