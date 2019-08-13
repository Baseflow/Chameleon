using System;
using Xamarin.Forms;

namespace Chameleon.Core
{
    public partial class ColoredTableView : TableView
    {
        public static BindableProperty SeparatorColorProperty = BindableProperty.Create("SeparatorColor", typeof(Color), typeof(ColoredTableView), Color.White);
        public Color SeparatorColor
        {
            get
            {
                return (Color)GetValue(SeparatorColorProperty);
            }
            set
            {
                SetValue(SeparatorColorProperty, value);
            }
        }
        public ColoredTableView()
        {
            //InitializeComponent();
        }

       
    }
}
