using System.ComponentModel;
using Chameleon.Core.ViewModels;
using MvvmCross.Forms.Views;

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
