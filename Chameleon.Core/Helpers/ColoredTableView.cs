using System;
using Xamarin.Forms;

namespace Chameleon.Core
{
    public partial class ColoredTableView : TableView
    {
        public ColoredTableView()
        {
        }

        public ColoredTableView(TableRoot root) : base(root)
        {
        }

        public static BindableProperty SeparatorColorProperty = BindableProperty.Create(nameof(SeparatorColor), typeof(Color), typeof(ColoredTableView), Color.White);
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
    }
}
