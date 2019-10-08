using System;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Chameleon.Services.Services;
using MediaManager;
using MvvmCross.Logging;
using MvvmCross.Navigation;

namespace Chameleon.Core.ViewModels
{
    public class SettingsPlaybackViewModel : BaseViewModel
    {
        private readonly IUserDialogs _userDialogs;
        public IMediaManager MediaManager { get; }
        private readonly ISettingsService _settingsService;


        public SettingsPlaybackViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IUserDialogs userDialogs, IMediaManager mediaManager, ISettingsService settingsService) : base(logProvider, navigationService)
        {
            _userDialogs = userDialogs ?? throw new ArgumentNullException(nameof(userDialogs));
            MediaManager = mediaManager ?? throw new ArgumentNullException(nameof(mediaManager));
            _settingsService = settingsService ?? throw new ArgumentNullException(nameof(settingsService));

        }

        private TimeSpan _stepSize;
        public TimeSpan StepSize
        {
            get => _stepSize;
            set => SetProperty(ref _stepSize, value);
        }

        private int _volume;
        public int Volume
        {
            get => _volume;
            set => SetProperty(ref _volume, value);
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
                if (SetProperty(ref _balance, value))
                {
                    double balance = value / 10;
                    MediaManager.Volume.Balance = (float)balance;
                    UpdateBalanceLabel();
                }
            }
        }

        private bool _clearQueueOnPlay;
        public bool ClearQueueOnPlay
        {
            get => _clearQueueOnPlay;
            set => SetProperty(ref _clearQueueOnPlay, value);
        }

        private bool _keepScreenOn;
        public bool KeepScreenOn
        {
            get => _keepScreenOn;
            set => SetProperty(ref _keepScreenOn, value);
        }

        private void UpdateBalanceLabel()
        {
            var balance = MediaManager.Volume.Balance;
            var balanceString = "";

            if (balance == 0)
                balanceString = $"0 {GetText("Center")}";

            if (balance < 0)
            {
                balanceString = $"{balance.ToString("0.#")} {GetText("Left")}";
            }
            else if (balance > 0)
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

        public override Task Initialize()
        {
            StepSize = _settingsService.StepSizes;
            Volume = _settingsService.Volume;
            Balance = _settingsService.Balance;
            ClearQueueOnPlay = _settingsService.ClearQueueOnPlay;
            KeepScreenOn = _settingsService.KeepScreenOn;

            return base.Initialize();
        }

        public override void ViewDisappearing()
        {
            _settingsService.StepSizes = StepSize;
            _settingsService.Volume = Volume;
            _settingsService.Balance = Balance;
            _settingsService.ClearQueueOnPlay = ClearQueueOnPlay;
            _settingsService.KeepScreenOn = KeepScreenOn;

            base.ViewDisappearing();
        }
    }
}
