using System;
using System.Collections.Generic;
using System.Text;
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
