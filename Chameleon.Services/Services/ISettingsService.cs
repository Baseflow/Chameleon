using System;
namespace Chameleon.Services.Services
{
    public interface ISettingsService
    {
        TimeSpan StepSizes { get; set; }
        int Volume { get; set; }
        double Balance { get; set; }
        bool ClearQueueOnPlay { get; set; }
        bool KeepScreenOn { get; set; }
    }
}
