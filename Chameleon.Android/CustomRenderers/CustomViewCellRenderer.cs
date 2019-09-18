using System;
using System.ComponentModel;
using Android.Content;
using Android.Graphics.Drawables;
using Android.Views;
using Chameleon.Android.CustomRenderers;
using Chameleon.Core.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomViewCell), typeof(CustomViewCellRenderer))]
namespace Chameleon.Android.CustomRenderers
{
    public class CustomViewCellRenderer : ViewCellRenderer
    {
        private global::Android.Views.View cellCore;
        private Drawable unselectedBackground;
        private bool selected;


        protected override global::Android.Views.View GetCellCore(Cell item, global::Android.Views.View convertView, ViewGroup parent, Context context)
        {
            cellCore = base.GetCellCore(item, convertView, parent, context);
            selected = false;
            unselectedBackground = cellCore.Background;
            return cellCore;
        }

        protected override void OnCellPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            base.OnCellPropertyChanged(sender, args);
            if (args.PropertyName == "IsSelected")
            {
                selected = !selected;
                if (selected)
                {
                    var extendedViewCell = sender as CustomViewCell;
                    cellCore.SetBackgroundColor(extendedViewCell.SelectedItemBackgroundColor.ToAndroid());
                }
                else
                {
                    cellCore.SetBackground(unselectedBackground);
                }
            }
        }
    }
}
