using MvvmCross.Commands;

namespace Chameleon.Core.Helpers
{
    public class OpenSourceButton
    {
        public string Title { get; set; }
        public IMvxAsyncCommand<OpenSourceButton> Command { get; set; }
        public string Url { get; set; }
    }
}
