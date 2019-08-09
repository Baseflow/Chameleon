using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chameleon.Core.ViewModels;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms;

namespace Chameleon.Core.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    [MvxContentPagePresentation(WrapInNavigationPage = true)]
    public partial class PlaylistOverviewPage : MvxContentPage<PlaylistOverviewViewModel>
    {
        public PlaylistOverviewPage()
        { 
            InitializeComponent();
        }
    }
}
