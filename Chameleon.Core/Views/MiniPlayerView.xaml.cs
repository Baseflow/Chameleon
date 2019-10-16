using System.ComponentModel;
using Chameleon.Core.ViewModels;
using MvvmCross;
using MvvmCross.Forms.Views;
using MvvmCross.ViewModels;

namespace Chameleon.Core.Views
{
    [DesignTimeVisible(false)]
    public partial class MiniPlayerView : MvxContentView<MiniPlayerViewModel>
    {
        public MiniPlayerView()
        {
            InitializeComponent();

            if (!(ViewModel is MiniPlayerViewModel))
            {
                var _viewModelLoader = Mvx.IoCProvider.Resolve<IMvxViewModelLoader>();
                var request = new MvxViewModelInstanceRequest(typeof(MiniPlayerViewModel));
                request.ViewModelInstance = _viewModelLoader.LoadViewModel(request, null);
                ViewModel = request.ViewModelInstance as MiniPlayerViewModel;
            }
        }
    }
}
