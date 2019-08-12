using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Windows.Input;
using MvvmCross.Binding;
using MvvmCross.Binding.Bindings.Target;
using Xamarin.Forms;

namespace Chameleon.Core.Helpers
{
    public class MvxCollectionViewItemClickPropertyTargetBinding : MvxPropertyInfoTargetBinding<MvxCollectionView>
    {
        private bool _subscribed;

        public override MvxBindingMode DefaultMode => MvxBindingMode.TwoWay;

        public MvxCollectionViewItemClickPropertyTargetBinding(object target, PropertyInfo targetPropertyInfo) : base(target, targetPropertyInfo)
        {
        }

        protected override void SetValueImpl(object target, object value)
        {
            var view = target as MvxCollectionView;
            if (view == null) return;

            view.ItemClick = (ICommand)value;
        }

        public override void SubscribeToEvents()
        {
            var myView = View;
            if (myView == null)
            {
                MvxBindingLog.Error($"Error - MyView is null in {nameof(MvxCollectionViewItemClickPropertyTargetBinding)}");
                return;
            }

            _subscribed = true;
            myView.SelectionChanged += HandleMyPropertyChanged;
        }

        private void HandleMyPropertyChanged(object sender, EventArgs e)
        {
            var view = sender as MvxCollectionView;
            var args = e as ItemTappedEventArgs;
            if (args == null) return;

            if (view?.ItemClick == null)
            {
                MvxBindingLog.Error($"Error - ItemClick is null in {nameof(MvxCollectionViewItemClickPropertyTargetBinding)}");
                return;
            }

            view.ItemClick.Execute(args.Item);
        }

        protected override void Dispose(bool isDisposing)
        {
            base.Dispose(isDisposing);

            if (isDisposing)
            {
                var myView = View;
                if (myView != null && _subscribed)
                {
                    myView.SelectionChanged -= HandleMyPropertyChanged;
                    _subscribed = false;
                }
            }
        }
    }
}
