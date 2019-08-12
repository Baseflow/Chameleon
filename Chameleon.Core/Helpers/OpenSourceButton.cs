using System;
using MvvmCross.Commands;

namespace Chameleon.Core.Helpers
{
    public class OpenSourceButton
    {
        public string Title { get; set; }
        public MvxAsyncCommand OpenBrowserCommand { get; set; }
        public string Url { get; set; }
    }
}
