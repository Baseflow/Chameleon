using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Acr.UserDialogs;
using MediaManager;
using MonkeyCache;
using MonkeyCache.LiteDB;
using MvvmCross.Logging;
using MvvmCross.Navigation;

namespace Chameleon.Core.ViewModels
{
    public class SettingsPlaybackViewModel : BaseViewModel
    {
        private readonly IUserDialogs _userDialogs;
        public IMediaManager MediaManager { get; }
        private readonly IBarrel _barrel;


        public SettingsPlaybackViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IUserDialogs userDialogs, IMediaManager mediaManager, IBarrel barrel) : base(logProvider, navigationService)
        {
            _userDialogs = userDialogs ?? throw new ArgumentNullException(nameof(userDialogs));
            MediaManager = mediaManager ?? throw new ArgumentNullException(nameof(mediaManager));
            _barrel = barrel ?? throw new ArgumentNullException(nameof(barrel));

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

        private int _volume;
        public int Volume
        {
            get => _volume;
            set => SetProperty(ref _volume, value);
        }

        //private int _maxVolume;
        //public int MaxVolume
        //{
        //    get
        //    {
        //        if (!_barrel.Exists("volume"))
        //        {
        //            var volume = MediaManager.Volume.MaxVolume;
        //            return volume;
        //        }
        //        else
        //        {
        //            var savedVolume = _barrel.Get<int>("volume");
        //            return savedVolume;
        //        }
        //    }
        //    set
        //    {
        //        if (SetProperty(ref _maxVolume, value))
        //        {
        //            _barrel.Add("volume", MediaManager.Volume.MaxVolume, TimeSpan.MaxValue);
        //        }
        //    }
        //}

        private TimeSpan _stepSize;
        public TimeSpan StepSize
        {
            get => _stepSize;
            set => SetProperty(ref _stepSize, value);
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
            GetVolume();
            GetBalance();
            GetStepSize();
            GetClearQueueOnPlay();
            GetKeepScreenOn();

            return base.Initialize();
        }

        public override void ViewDisappearing()
        {
            _barrel.Add("volume", Volume, TimeSpan.MaxValue);
            _barrel.Add("balance", Balance, TimeSpan.MaxValue);
            _barrel.Add("stepSize", StepSize, TimeSpan.MaxValue);
            _barrel.Add("clearQueueOnPlay", ClearQueueOnPlay, TimeSpan.MaxValue);
            _barrel.Add("keepScreenOn", KeepScreenOn, TimeSpan.MaxValue);

            base.ViewDisappearing();
        }

        private void GetVolume()
        {
            if (_barrel.Exists("volume"))
                Volume = _barrel.Get<int>("volume");
            else
                Volume = MediaManager.Volume.MaxVolume;
        }

        private void GetBalance()
        {
            if (_barrel.Exists("balance"))
                Balance = _barrel.Get<double>("balance");
            else
                Balance = MediaManager.Volume.Balance;
        }

        private void GetStepSize()
        {
            if (_barrel.Exists("stepSize"))
                StepSize = _barrel.Get<TimeSpan>("stepSize");
            else
                StepSize = MediaManager.StepSize;
        }

        private void GetClearQueueOnPlay()
        {
            if (_barrel.Exists("clearQueueOnPlay"))
                ClearQueueOnPlay = _barrel.Get<bool>("clearQueueOnPlay");
            else
                ClearQueueOnPlay = MediaManager.ClearQueueOnPlay;
        }

        private void GetKeepScreenOn()
        {
            if (_barrel.Exists("keepScreenOn"))
                KeepScreenOn = _barrel.Get<bool>("keepScreenOn");
            else
                KeepScreenOn = MediaManager.KeepScreenOn;
        }
    }

}
