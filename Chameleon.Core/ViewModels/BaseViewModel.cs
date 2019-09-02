using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Chameleon.Services;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Localization;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Chameleon.Core.ViewModels
{
    public abstract class BaseViewModel : MvxNavigationViewModel
    {
        public BaseViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
        }

        protected static IMvxTextProvider _textProvider { get; } = Mvx.IoCProvider.Resolve<IMvxTextProvider>();

        public virtual IMvxAsyncCommand BackCommand => new MvxAsyncCommand(async () => await NavigationService.Close(this));

        private string _title;
        public virtual string Title
        {
            get => !string.IsNullOrEmpty(_title) ? _title : GetText(GetType().Name, "Title");
            set => SetProperty(ref _title, value);
        }

        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }

        public override Task Initialize()
        {
            return ReloadData();
        }

        /// <summary>
        /// Gets the internationalized string at the given <paramref name="index"/>, which is the key of the resource.
        /// </summary>
        /// <param name="index">Index key of the string from the resources of internationalized strings.</param>
        public string this[string index, string viewmodel] => _textProvider.GetText(AppSettings.TextProviderNamespace, !string.IsNullOrEmpty(viewmodel) ? viewmodel : GetType().Name, index);

        public virtual string GetText(string viewModel, string key)
        {
            return _textProvider.GetText(AppSettings.TextProviderNamespace, viewModel, key);
        }

        public virtual string GetText(string key)
        {
            return GetText(GetType().Name, key);
        }

        public string GetCallerMemberText([CallerMemberName] string key = "")
        {
            return GetText(key);
        }

        public IMvxLanguageBinder TextSource => new MvxLanguageBinder(AppSettings.TextProviderNamespace, GetType().Name);

        private IMvxAsyncCommand<bool> _reloadCommand;
        public IMvxAsyncCommand<bool> ReloadCommand
        {
            get
            {
                return _reloadCommand = _reloadCommand ?? new MvxAsyncCommand<bool>(async (forceReload) =>
                {
                    IsLoading = true;

                    await ReloadData(forceReload).ConfigureAwait(false);

                    IsLoading = false;
                });
            }
        }

        public virtual Task ReloadData(bool forceReload = false)
        {
            return Task.CompletedTask;
        }
    }

    public abstract class BaseViewModel<TParameter> : BaseViewModel, IMvxViewModel<TParameter>
    {
        public BaseViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
        }

        public abstract void Prepare(TParameter parameter);
    }

    public abstract class BaseViewModel<TParameter, TResult> : BaseViewModel, IMvxViewModel<TParameter, TResult>
    {
        public BaseViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
        }

        public TaskCompletionSource<object> CloseCompletionSource { get; set; }

        public abstract void Prepare(TParameter parameter);

        public override void ViewDestroy(bool viewFinishing = true)
        {
            if (CloseCompletionSource != null && !CloseCompletionSource.Task.IsCompleted &&
                !CloseCompletionSource.Task.IsFaulted)
            {
                CloseCompletionSource?.TrySetCanceled();
            }

            base.ViewDestroy(viewFinishing);
        }

        public override IMvxAsyncCommand BackCommand =>
            new MvxAsyncCommand(async () => await NavigationService.Close(this, default(TResult)));
    }

    public abstract class BaseViewModelResult<TResult> : BaseViewModel, IMvxViewModelResult<TResult>
    {
        public BaseViewModelResult(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
        }

        public TaskCompletionSource<object> CloseCompletionSource { get; set; }

        public override void ViewDestroy(bool viewFinishing = true)
        {
            if (CloseCompletionSource != null && !CloseCompletionSource.Task.IsCompleted &&
                !CloseCompletionSource.Task.IsFaulted)
            {
                CloseCompletionSource?.TrySetCanceled();
            }

            base.ViewDestroy(viewFinishing);
        }

        public override IMvxAsyncCommand BackCommand =>
            new MvxAsyncCommand(async () => await NavigationService.Close(this, default(TResult)));
    }
}
