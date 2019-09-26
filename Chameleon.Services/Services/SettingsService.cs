using System;
using MediaManager;
using MonkeyCache;
using Chameleon.Services.Extensions;

namespace Chameleon.Services.Services
{
    public class SettingsService : ISettingsService
    {
        private readonly IMediaManager _mediaManager;
        private readonly IBarrel _barrel;

        private const string VOLUME_KEY = "volume";
        private const string STEP_SIZE_KEY = "stepSize";
        private const string BALANCE_KEY = "balance";
        private const string CLEAR_QUEUE_ON_PLAY_KEY = "clearQueueOnPlay";
        private const string KEEP_SCREEN_ON_KEY = "keepScreenOn";

        public SettingsService (IBarrel barrel, IMediaManager mediaManager)
        {
            _mediaManager = mediaManager ?? throw new ArgumentNullException(nameof(mediaManager));
            _barrel = barrel ?? throw new ArgumentNullException(nameof(barrel));
        }

        public TimeSpan StepSizes
        {
            get => _barrel.Get(STEP_SIZE_KEY, _mediaManager.StepSize);
            set => _barrel.Add(STEP_SIZE_KEY, StepSizes, TimeSpan.MaxValue);
        }

        public int Volume
        {
            get =>  _barrel.Get(VOLUME_KEY, _mediaManager.Volume.MaxVolume);
            set => _barrel.Add(VOLUME_KEY, Volume, TimeSpan.MaxValue);
        }

        public double Balance
        {
            get => _barrel.Get(BALANCE_KEY, _mediaManager.Volume.Balance);
            set => _barrel.Add(BALANCE_KEY, Balance, TimeSpan.MaxValue);
        }

        public bool ClearQueueOnPlay
        {
            get => _barrel.Get(CLEAR_QUEUE_ON_PLAY_KEY, _mediaManager.ClearQueueOnPlay);
            set => _barrel.Add(CLEAR_QUEUE_ON_PLAY_KEY, ClearQueueOnPlay, TimeSpan.MaxValue);
        }

        public bool KeepScreenOn
        {
            get => _barrel.Get(KEEP_SCREEN_ON_KEY, _mediaManager.KeepScreenOn);
            set => _barrel.Add(KEEP_SCREEN_ON_KEY, KeepScreenOn, TimeSpan.MaxValue);
        }
    }
}
