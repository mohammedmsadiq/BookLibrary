using System.ComponentModel;
using System.Windows.Input;
using BookLibrary.Services;

namespace BookLibrary.ViewModels;

public abstract class BaseViewModel : BindableBase, IPageLifecycleAware, IInitialize, INavigationAware, IDestructible,
    IActiveAware, IConfirmNavigation, IConfirmNavigationAsync, INotifyPropertyChanged
{
    private readonly BaseServices _baseServices;
    protected INavigationService NavigationService => _baseServices.NavigationService;
    protected IRegionManager RegionManager => _baseServices.RegionManager;
    protected IPageDialogService PageDialogs => _baseServices.PageDialogs;
    protected IEventAggregator EventAggregator => _baseServices.EventAggregator;
    protected BaseViewModel(BaseServices baseServices)
    {
        _baseServices = baseServices;
        GoBackCommand = new Command(async () =>
        {
            await NavigationService.GoBackAsync();
        });
    }
    
    #region IDestructible

    public void Destroy()
    {
        throw new NotImplementedException();
    }

    #endregion IDestructible

    #region IInitialize

    public void Initialize(INavigationParameters parameters)
    {
    }

    #endregion IInitialize
    
    #region CommandsAsyncTasks
    private async Task GotoBacktoRootTaskAsync()
    {
        await NavigationService.GoBackToRootAsync();
    }

    public async Task GotoBackTaskAsync()
    {
        await NavigationService.GoBackAsync();
    }
    #endregion

    #region IPageLifecycleAware

    public virtual void OnAppearing()
    {
    }

    public virtual void OnDisappearing()
    {
    }

    #endregion IPageLifecycleAware

    #region INavigationAware

    public virtual void OnNavigatedFrom(INavigationParameters parameters)
    {
    }

    public virtual void OnNavigatedTo(INavigationParameters parameters)
    {
    }

    #endregion INavigationAware

    #region IActiveAware

    public bool IsActive { get; set; }

    public event EventHandler IsActiveChanged;

    private void OnIsActiveChanged()
    {
        IsActiveChanged?.Invoke(this, EventArgs.Empty);

        if (IsActive)
            OnIsActive();
        else
            OnIsNotActive();
    }

    protected virtual void OnIsActive()
    {
    }

    protected virtual void OnIsNotActive()
    {
    }

    #endregion IActiveAware

    #region IConfirmNavigation

    public virtual bool CanNavigate(INavigationParameters parameters)
    {
        return true;
    }

    public virtual Task<bool> CanNavigateAsync(INavigationParameters parameters)
    {
        return Task.FromResult(CanNavigate(parameters));
    }

    #endregion IConfirmNavigation

    #region ExecuteAsyncTask

    protected async Task ExecuteAction(Action action)
    {
        Device.BeginInvokeOnMainThread(() => { IsBusy = true; });
        try
        {
            action();
        }
        catch (Exception ex)
        {
            await ShowErrorMessage(ex);
        }
        finally
        {
            Device.BeginInvokeOnMainThread(() => { IsBusy = false; });
        }
    }

    protected async Task ExecuteAsyncTask(Func<Task> actionDelegate)
    {
        Device.BeginInvokeOnMainThread(() => { IsBusy = true; });
        try
        {
            await ExecuteAsyncTaskWithNoLoading(actionDelegate);
        }
        catch (Exception ex)
        {
            await ShowErrorMessage(ex);
        }
        finally
        {
            Device.BeginInvokeOnMainThread(() => { IsBusy = false; });
        }
    }

    protected async Task ExecuteAsyncTaskWithNoLoading(Func<Task> actionDelegate)
    {
        try
        {
            await actionDelegate();
        }
        catch (Exception ex)
        {
            await ShowErrorMessage(ex);
        }
    }

    protected async Task ShowErrorMessage(Exception ex)
    {
        //Debug.WriteLine("--------->" + ErrorMessageTitle + "=======>" + ex.Message);

        //Dialog service, show error.
        await _baseServices.PageDialogs.DisplayAlertAsync(ErrorMessageTitle, ex.Message, ErrorMessageOkButton);
    }

    #endregion ExecuteAsyncTask
    
    public ICommand GoBackCommand { get; set; }

    private bool _isBusy;
    public bool IsBusy
    {
        get => _isBusy;
        set => SetProperty(ref _isBusy, value);
    }

    private string? _errorMessageTitle;
    public string? ErrorMessageTitle
    {
        get => _errorMessageTitle;
        set => SetProperty(ref _errorMessageTitle, value);
    }

    private string? _errorMessage;
    public string? ErrorMessage
    {
        get => _errorMessage;
        set => SetProperty(ref _errorMessage, value);
    }
    
    private string? _errorMessageOkButton;
    public string? ErrorMessageOkButton
    {
        get => _errorMessageOkButton;
        set => SetProperty(ref _errorMessageOkButton, value);
    }
    
    private string? _title;
    public string? Title
    {
        get => _title;
        set => SetProperty(ref _title, value);
    }
}