using Xamarin.Forms;

namespace Chameleon.Core.Effects
{
    public class TransparentSelectableEffect : RoutingEffect
    {
        public TransparentSelectableEffect() : base("Chameleon.TransparentSelectableEffect")
        {
        }

        public bool Borderless { get; set; } = true;
    }
}
