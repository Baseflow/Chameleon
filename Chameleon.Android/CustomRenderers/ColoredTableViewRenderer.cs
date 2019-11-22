using System.ComponentModel;
using Android.Content;
using Android.Graphics.Drawables;
using Chameleon.Droid.CustomRenderers;
using Chameleon.Core;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ColoredTableView), typeof(ColoredTableViewRenderer))]
namespace Chameleon.Droid.CustomRenderers
{
    public class ColoredTableViewRenderer : TableViewRenderer
    {
        public ColoredTableViewRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<TableView> e)
        {
            base.OnElementChanged(e);
            if (Control == null)
                return;

            var listView = Control as Android.Widget.ListView;
            var coloredTableView = (ColoredTableView)Element;

            listView.Divider.SetTint(Color.Transparent.GetHashCode());
            listView.SetHeaderDividersEnabled(false);
            listView.Divider = new ColorDrawable(coloredTableView.SeparatorColor.ToAndroid());
            listView.DividerHeight = 2;
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == "SeparatorColor")
            {
                var listView = Control as Android.Widget.ListView;
                var coloredTableView = (ColoredTableView)Element;

                listView.Divider.SetTint(Color.Transparent.GetHashCode());
                listView.SetHeaderDividersEnabled(false);
                listView.Divider = new ColorDrawable(coloredTableView.SeparatorColor.ToAndroid());
                listView.DividerHeight = 2;
            }
        }
    }
}
