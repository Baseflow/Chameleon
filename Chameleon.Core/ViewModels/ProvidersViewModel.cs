using System.Linq;
using System.Threading.Tasks;
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

        private IMediaItem _selectedMediaItem;
        public IMediaItem SelectedMediaItem
        {
            get => _selectedMediaItem;
            set => SetProperty(ref _selectedMediaItem, value);
        }

                private MvxObservableCollection<IMediaItem> _recommendedItems = new MvxObservableCollection<IMediaItem>();
        public MvxObservableCollection<IMediaItem> RecommendedItems
        {
            get => _recommendedItems;
            set => SetProperty(ref _recommendedItems, value);
        }

        private IMvxAsyncCommand _sourceCommand;
        public IMvxAsyncCommand SourceCommand => _sourceCommand ?? (_sourceCommand = new MvxAsyncCommand(PlayWhenSourceSelected));

        private async Task PlayWhenSourceSelected()
        {
            await NavigationService.Navigate<PlayerViewModel, IMediaItem>(SelectedMediaItem);
            SelectedMediaItem = null;
        }
    }
}
