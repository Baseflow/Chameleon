using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using MvvmCross.Core;
using MvvmCross.Forms.Platforms.Uap.Core;
using MvvmCross.Forms.Platforms.Uap.Views;
using MvvmCross.Platforms.Uap.Core;
using Chameleon.Core;
using Xamarin.Forms;
using MediaManager;

namespace Chameleon.UWP
{
    sealed partial class App
    {
        public App()
        {
            Forms.SetFlags("CollectionView_Experimental");
            CrossMediaManager.Current.Init();
            InitializeComponent();
        }
    }

    public abstract class ChameleonApp : MvxWindowsApplication<MvxFormsWindowsSetup<Core.App, FormsApp>, Core.App, FormsApp, MainPage>
    {
    }
}
