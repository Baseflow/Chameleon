using System;
using System.Collections.Generic;
using System.ComponentModel;
using Chameleon.Core.ViewModels;
using MvvmCross;
using MvvmCross.Forms.Views;
using MvvmCross.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Chameleon.Core.Views
{
    [DesignTimeVisible(false)]
    public partial class MiniPlayerView : MvxContentView<MiniPlayerViewModel>
    {
        public MiniPlayerView()
        {
            InitializeComponent();
        }
    }
}
