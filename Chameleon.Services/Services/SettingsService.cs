using System;
using Chameleon.Services.Extensions;
using MediaManager;
using MonkeyCache;

namespace Chameleon.Services.Services
{
    public class SettingsService : ISettingsService
    {
        private readonly IMediaManager _mediaManager;
        private readonly IBarrel _barrel;

        private const string VolumeKey = "volume";
        private const string StepSizeKey = "stepSize";
        private const string BalanceKey = "balance";
        private const string ClearQueueOnPlayKey = "clearQueueOnPlay";
        private const string KeepScreenOnKey = "keepScreenOn";

        public SettingsService(IBarrel barrel, IMediaManager mediaManager)
        {
            _mediaManager = mediaManager ?? throw new ArgumentNullException(nameof(mediaManager));
            _barrel = barrel ?? throw new ArgumentNullException(nameof(barrel));
        }

        public TimeSpan StepSizes
        {
            get => _barrel.GetOrCreate(StepSizeKey, _mediaManager.StepSize);
            set => _barrel.Add(StepSizeKey, value, TimeSpan.MaxValue);
        }

        public int Volume
        {
            get => _barrel.GetOrCreate(VolumeKey, _mediaManager.Volume.MaxVolume);
            set => _barrel.Add(VolumeKey, value, TimeSpan.MaxValue);
        }

        public double Balance
        {
            get => _barrel.GetOrCreate(BalanceKey, _mediaManager.Volume.Balance);
            set => _barrel.Add(BalanceKey, value, TimeSpan.MaxValue);
        }

        public bool ClearQueueOnPlay
        {
            get => _barrel.GetOrCreate(ClearQueueOnPlayKey, _mediaManager.ClearQueueOnPlay);
            set => _barrel.Add(ClearQueueOnPlayKey, value, TimeSpan.MaxValue);
        }

        public bool KeepScreenOn
        {
            get => _barrel.GetOrCreate(KeepScreenOnKey, _mediaManager.KeepScreenOn);
            set => _barrel.Add(KeepScreenOnKey, value, TimeSpan.MaxValue);
        }
    }
}
