using System;
using Xamarin.Forms;

namespace Chameleon.Core.Effects
{
    public class PressedStateEffect : RoutingEffect
    {
        public PressedStateEffect() : base("Chameleon.PressedStateEffect")
        {
        }

        public bool Borderless { get; set; } = false;

    }
}