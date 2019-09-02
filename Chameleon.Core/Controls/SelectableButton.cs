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
