using System;
using System.Collections.Generic;
using System.Text;
using Chameleon.Services.Models;
using MediaManager;
using MediaManager.Media;

namespace Chameleon.Services.Providers
{
    public class ProviderBase : NotifyPropertyChangedBase, ILibraryProvider, ISourceProvider
    {
        protected bool _enabled = false;
        public virtual bool Enabled { 
            get => _enabled; 
            set => SetProperty(ref _enabled, value);
        }

        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public bool Soon { get; set; }

        public virtual bool CanEdit => true;
    }
}
