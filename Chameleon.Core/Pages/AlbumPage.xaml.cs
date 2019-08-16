using System;
using System.ComponentModel;
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
    public partial class AlbumPage : MvxContentPage<AlbumViewModel>
    {
        public AlbumPage()
        {
            InitializeComponent();
        }
    }
}
