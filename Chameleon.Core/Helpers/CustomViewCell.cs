using System;
using Xamarin.Forms;

namespace Chameleon.Core.Helpers
{
    public class CustomViewCell : ViewCell
    {
        public static readonly BindableProperty SelectedItemBackgroundColorProperty = BindableProperty.Create("SelectedItemBackgroundColor",
            typeof(Color), typeof(CustomViewCell), default);

        public Color SelectedItemBackgroundColor
        {
            get
            {
                return (Color)GetValue(SelectedItemBackgroundColorProperty);
            }
            set
            {
                SetValue(SelectedItemBackgroundColorProperty, value);
            }
        }
    }
}
