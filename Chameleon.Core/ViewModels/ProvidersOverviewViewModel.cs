using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chameleon.Services.Models;
using Chameleon.Services.Providers;
using MediaManager;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;

namespace Chameleon.Core.ViewModels
{
    public class ProvidersOverviewViewModel : BaseViewModel
    {
        private readonly IMediaManager _mediaManager;

        public ProvidersOverviewViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IMediaManager mediaManager) : base(logProvider, navigationService)
        {
            _mediaManager = mediaManager ?? throw new ArgumentNullException(nameof(mediaManager));
        }

        private ISourceProvider _selectedItem;
        public ISourceProvider SelectedItem
        {
            get => _selectedItem;
            set => SetProperty(ref _selectedItem, value);
        }

        private IList<ISourceProvider> _recommendedProviders;
        public IList<ISourceProvider> RecommendedProviders
        {
            get => _recommendedProviders;
            set => SetProperty(ref _recommendedProviders, value);
        }

        private IList<ISourceProvider> _providers;
        public IList<ISourceProvider> Providers
        {
            get => _providers;
            set => SetProperty(ref _providers, value);
        }

        public override void Prepare()
        {
            base.Prepare();

            var providers = _mediaManager.Library.Providers.Where(x => !(x is PlaylistProvider)).OfType<ISourceProvider>().ToList();
            providers.AddRange(new List<ISourceProvider>() {
                new ProviderBase() { Title = "Podcasts", Soon = true },
                new ProviderBase() { Title = "Youtube", Soon = true },
                new ProviderBase() { Title = "Spotify", Soon = true },
                new ProviderBase() { Title = "Tidal", Soon = true },
                new ProviderBase() { Title = "Soundcloud", Soon = true }
            });
            
            Providers = providers;
            RecommendedProviders = Providers.Where(x => !x.Soon).ToList();
        }

        private IMvxAsyncCommand<ISourceProvider> _sourceCommand;
        public IMvxAsyncCommand<ISourceProvider> SourceCommand => _sourceCommand ?? (_sourceCommand = new MvxAsyncCommand<ISourceProvider>(OpenProvider));

        private async Task OpenProvider(ISourceProvider provider)
        {
            if (provider.Soon)
                return;

            await NavigationService.Navigate<ProviderViewModel, ISourceProvider>(provider);
            SelectedItem = null;
        }
    }
}
