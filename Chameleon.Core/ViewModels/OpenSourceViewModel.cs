using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Chameleon.Core.Helpers;
using Chameleon.Services.Services;
using MediaManager;
using MediaManager.Media;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using static System.Net.Mime.MediaTypeNames;

namespace Chameleon.Core.ViewModels
{
    public class OpenSourceViewModel : BaseViewModel
    {
        private readonly IUserDialogs _userDialogs;

        public OpenSourceViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IUserDialogs userDialogs) : base(logProvider, navigationService)
        {
            _userDialogs = userDialogs ?? throw new ArgumentNullException(nameof(userDialogs));

        }

        private IList<OpenSourceButton> _openSourceList;
        public IList<OpenSourceButton> OpenSourceList
        {
            get => _openSourceList;
            set => SetProperty(ref _openSourceList, value);
        }

        private IMvxAsyncCommand<OpenSourceButton> _itemClickedCommand;
        public IMvxAsyncCommand<OpenSourceButton> ItemClickedCommand => _itemClickedCommand ?? (_itemClickedCommand = new MvxAsyncCommand<OpenSourceButton>(ItemClicked));

        private async Task ItemClicked(OpenSourceButton arg)
        {
            await Xamarin.Essentials.Browser.OpenAsync(arg.Url);
        }

        public override void Prepare()
        {
            base.Prepare();
            OpenSourceList = new List<OpenSourceButton>()
            {
                new OpenSourceButton(){Title = "MediaManager", Command = ItemClickedCommand, Url = "https://github.com/martijn00/XamarinMediaManager"},
                new OpenSourceButton(){Title = "Xamarin Forms", Command = ItemClickedCommand, Url = "https://docs.microsoft.com/en-us/xamarin/xamarin-forms/"},
                new OpenSourceButton(){Title = "MvvmCross", Command = ItemClickedCommand, Url = "https://github.com/MvvmCross/MvvmCross"},
                new OpenSourceButton(){Title = "Lottie", Command = ItemClickedCommand, Url = "https://github.com/martijn00/LottieXamarin"},
                new OpenSourceButton(){Title = "Xamarin Essentials", Command = ItemClickedCommand, Url = "https://github.com/xamarin/Essentials"},
                new OpenSourceButton(){Title = "FFImageLoading", Command = ItemClickedCommand, Url = "https://github.com/luberda-molinet/FFImageLoading"},
                new OpenSourceButton(){Title = "UserDialogs", Command = ItemClickedCommand, Url = "https://github.com/aritchie/userdialogs"},
                new OpenSourceButton(){Title = "MonkeyCache", Command = ItemClickedCommand, Url = "https://github.com/jamesmontemagno/monkey-cache"},
                new OpenSourceButton(){Title = "Xam Plugin Media", Command = ItemClickedCommand, Url = "https://github.com/jamesmontemagno/MediaPlugin"}
            };
        }
    }
}
