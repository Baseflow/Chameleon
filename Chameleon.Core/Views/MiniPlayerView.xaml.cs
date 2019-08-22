using System;
using System.Collections.Generic;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Chameleon.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MiniPlayerView : MvxContentView
    {
        public MiniPlayerView()
        {
            InitializeComponent();
        }
    }
}
