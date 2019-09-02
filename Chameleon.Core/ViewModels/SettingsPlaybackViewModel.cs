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

        private double _balance;
        public double Balance
        {
            get => _balance;
            set
            {
                if(SetProperty(ref _balance, value))
                {
                    double balance = value / 10;
                    MediaManager.VolumeManager.Balance = (float)balance;
                    UpdateBalanceLabel();
                }
            }
        }

        private void UpdateBalanceLabel()
        {
            var balance = MediaManager.VolumeManager.Balance;
            var balanceString = "";

            if (balance == 0)
                balanceString = $"0 {GetText("Center")}";

            if (balance < 0)
            {
                balanceString = $"{balance.ToString("0.#")} {GetText("Left")}";
            }
            else if(balance > 0)
            {
                balanceString = $"+{balance.ToString("0.#")} {GetText("Right")}";
            }

            BalanceLabel = balanceString;
        }

        public override void ViewAppearing()
        {
            base.ViewAppearing();

            UpdateBalanceLabel();
        }
    }

}
