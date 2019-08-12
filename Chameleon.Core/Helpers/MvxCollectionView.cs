using System.Windows.Input;
using Xamarin.Forms;

namespace Chameleon.Core.Helpers
{
    public class MvxCollectionView : CollectionView
    {
        public ICommand ItemClick
        {
            get;
            set;
        }
    }
}
