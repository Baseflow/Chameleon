using System;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Chameleon.Core.Helpers;
using Chameleon.iOS.CustomRenderers;

//[assembly: ExportRenderer(typeof(CustomViewCell), typeof(CustomViewCellRenderer))]
namespace Chameleon.iOS.CustomRenderers
{
    public class CustomViewCellRenderer : ViewCellRenderer
    {
        //public override UITableViewCell GetCell(Cell item, UITableViewCell reusableCell, UITableView tv)
        //{
        //    var cell = base.GetCell(item, reusableCell, tv);
        //    var view = item as CustomViewCell;
        //    cell.SelectedBackgroundView = new UIView
        //    {
        //        BackgroundColor = view.SelectedItemBackgroundColor.ToUIColor(),
        //    };
        //    return cell;
        //}
    }
}
