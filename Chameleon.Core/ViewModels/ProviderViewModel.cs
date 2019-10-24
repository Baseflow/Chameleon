using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Chameleon.Services.Extensions;
using Chameleon.Services.Models;
using MediaManager;
using MediaManager.Library;
using MediaManager.Media;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Chameleon.Core.ViewModels
{
    public class ProviderViewModel : BaseViewModel<ISourceProvider>
    {
        private readonly IUserDialogs _userDialogs;
        private readonly IMediaManager _mediaManager;

        public ProviderViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IUserDialogs userDialogs, IMediaManager mediaManager) : base(logProvider, navigationService)
        {
            _userDialogs = userDialogs ?? throw new ArgumentNullException(nameof(userDialogs));
            _mediaManager = mediaManager ?? throw new ArgumentNullException(nameof(mediaManager));
        }

        public override void Prepare(ISourceProvider parameter)
        {
            Provider = parameter;
        }

        private MvxObservableCollection<IMediaItem> _mediaItems = new MvxObservableCollection<IMediaItem>();
        public MvxObservableCollection<IMediaItem> MediaItems
        {
            get => _mediaItems;
            set => SetProperty(ref _mediaItems, value);
        }

        private IMvxAsyncCommand _addCommand;
        public IMvxAsyncCommand AddCommand => _addCommand ?? (_addCommand = new MvxAsyncCommand(AddProviderSource));

        private async Task AddProviderSource()
        {
            var config = new PromptConfig();
            config.Message = GetText("EnterUrl");
            var result = await _userDialogs.PromptAsync(config);
            if (result.Ok && !string.IsNullOrEmpty(result.Value))
            {
                if(Provider is ILibraryProvider<IMediaItem> mediaItemProvider)
                {
                    if (result.Value.IsValidUrl())
                    {
                        var item = await _mediaManager.Extractor.CreateMediaItem(result.Value);
                        await mediaItemProvider.AddOrUpdate(item);
                        await ReloadData();
                    }
                    else
                        await _userDialogs.AlertAsync(GetText("NotValidUrl"));
                }
            }
        }

        private ISourceProvider _provider;
        public ISourceProvider Provider
        {
            get => _provider;
            set => SetProperty(ref _provider, value);
        }

        public override async Task ReloadData(bool forceReload = false)
        {
            if (Provider is ILibraryProvider<IMediaItem> mediaItemProvider)
            {
                var items = await mediaItemProvider.GetAll();
                MediaItems.ReplaceWith(items);
            }
        }
    }
}
