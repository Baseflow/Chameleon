using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Chameleon.Core.Helpers;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;

namespace Chameleon.Core.ViewModels
{
    public class OpenSourceViewModel : BaseViewModel
    {
        private readonly IUserDialogs _userDialogs;

        public OpenSourceViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IUserDialogs userDialogs) : base(logProvider, navigationService)
        {
            _userDialogs = userDialogs ?? throw new ArgumentNullException(nameof(userDialogs));

        }

        private IList<OpenSource> _openSourceList;
        public IList<OpenSource> OpenSourceList
        {
            get => _openSourceList;
            set => SetProperty(ref _openSourceList, value);
        }

        private IMvxAsyncCommand<OpenSource> _itemClickedCommand;
        public IMvxAsyncCommand<OpenSource> ItemClickedCommand => _itemClickedCommand ?? (_itemClickedCommand = new MvxAsyncCommand<OpenSource>(ItemClicked));

        private async Task ItemClicked(OpenSource arg)
        {
            await Xamarin.Essentials.Browser.OpenAsync(arg.Url);
        }

        public override void Prepare()
        {
            base.Prepare();
            OpenSourceList = new List<OpenSource>()
            {
                new OpenSource(){Title = "MediaManager", Command = ItemClickedCommand, Url = "https://github.com/martijn00/XamarinMediaManager"},
                new OpenSource(){Title = "Xamarin Forms", Command = ItemClickedCommand, Url = "https://docs.microsoft.com/en-us/xamarin/xamarin-forms/"},
                new OpenSource(){Title = "MvvmCross", Command = ItemClickedCommand, Url = "https://github.com/MvvmCross/MvvmCross"},
                new OpenSource(){Title = "Lottie", Command = ItemClickedCommand, Url = "https://github.com/martijn00/LottieXamarin"},
                new OpenSource(){Title = "Xamarin Essentials", Command = ItemClickedCommand, Url = "https://github.com/xamarin/Essentials"},
                new OpenSource(){Title = "FFImageLoading", Command = ItemClickedCommand, Url = "https://github.com/luberda-molinet/FFImageLoading"},
                new OpenSource(){Title = "UserDialogs", Command = ItemClickedCommand, Url = "https://github.com/aritchie/userdialogs"},
                new OpenSource(){Title = "MonkeyCache", Command = ItemClickedCommand, Url = "https://github.com/jamesmontemagno/monkey-cache"},
                new OpenSource(){Title = "Xam Plugin Media", Command = ItemClickedCommand, Url = "https://github.com/jamesmontemagno/MediaPlugin"}
            };
        }
    }
}
