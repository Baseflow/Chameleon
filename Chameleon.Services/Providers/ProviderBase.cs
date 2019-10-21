using System;
using System.Collections.Generic;
using System.Text;
using Chameleon.Services.Models;
using MediaManager.Media;

namespace Chameleon.Services.Providers
{
    public class ProviderBase : ILibraryProvider, ISourceProvider
    {
        public bool Enabled { get; set; } = true;

        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public bool Soon { get; set; }

        public virtual bool CanEdit => true;
    }
}
