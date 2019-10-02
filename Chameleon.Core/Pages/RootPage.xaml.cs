using System.ComponentModel;
using Chameleon.Core.ViewModels;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;


namespace Chameleon.Core.Views
{
    [DesignTimeVisible(false)]
    [MvxTabbedPagePresentation(TabbedPosition.Root, WrapInNavigationPage = true)]
    public partial class RootPage : MvxTabbedPage<RootViewModel>
    {
        public RootPage()
        {
            InitializeComponent();
        }

        private bool _firstTime = true;

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (_firstTime)
            {
                ViewModel.ShowInitialViewModelsCommand.ExecuteAsync();
                _firstTime = false;
            }
        }
    }
}
