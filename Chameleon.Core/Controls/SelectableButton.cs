using System;
using System.Collections.Generic;
using System.Text;
using Chameleon.Core.Effects;
using Xamarin.Forms;

namespace Chameleon.Core.Controls
{
    public class SelectableButton : ImageButton
    {
        public SelectableButton()
        {
            Effects.Add(new TransparentSelectableEffect());
        }
    }
}
