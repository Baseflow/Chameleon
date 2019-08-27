using System;
using System.Threading.Tasks;
using Acr.UserDialogs;
using MediaManager;
using MediaManager.Library;
using MediaManager.Volume;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;

namespace Chameleon.Core.ViewModels
{
    public class SettingsPlaybackViewModel : BaseViewModel
    {
        private readonly IUserDialogs _userDialogs;
        public IMediaManager MediaManager { get; }

        public SettingsPlaybackViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IUserDialogs userDialogs, IMediaManager mediaManager) : base(logProvider, navigationService)
        {
            _userDialogs = userDialogs ?? throw new ArgumentNullException(nameof(userDialogs));
            MediaManager = mediaManager ?? throw new ArgumentNullException(nameof(mediaManager));
        }

        private string _balanceLabel;
        public string BalanceLabel
        {
            get => _balanceLabel;
            set => SetProperty(ref _balanceLabel, value);
        }

        private string _balance;
        public string Balance
        {
            get => _balance;
            set => SetProperty(ref _balance, value);
        }

        private IMvxCommand _balanceChangedCommand;
        public IMvxCommand BalanceChangedCommand => _balanceChangedCommand ?? (_balanceChangedCommand = new MvxCommand(UpdateBalanceLabel));

        private void UpdateBalanceLabel()
        {
            var balance = Convert.ToInt32(MediaManager.VolumeManager.Balance * 10);
            var balanceString = "";

            if (balance == 0)
                balanceString = "0";

            if (balance < 0)
            {
                balance *= -1;
                balanceString = $"+{balance.ToString()} Left";
            }
            else if(balance > 0)
            {
                balanceString = $"+{balance.ToString()} Right";
            }

            Balance = balanceString;
        }

        public override void ViewAppearing()
        {
            base.ViewAppearing();

            UpdateBalanceLabel();
        }
    }

}
