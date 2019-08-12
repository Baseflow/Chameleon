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
                new OpenSourceButton(){Title = "test", Command = ItemClickedCommand, Url = "test.com"}
            };
        }
    }
}
