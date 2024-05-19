using System.Collections.ObjectModel;
using System.Windows.Input;
using BookLibrary.Data;
using BookLibrary.Model;
using BookLibrary.Services;

namespace BookLibrary.ViewModels;

public class HomePageViewModel : BaseViewModel
{
    public HomePageViewModel(BaseServices baseServices) : base(baseServices)
    {
        this.LoadDataAsync();
        AddBookCommand = new Command(async () =>
        {
            await NavigationService.NavigateAsync("AddBookPage");
        });
    }

    private void BookTappedCommandExecute(object obj)
    {
        var bookItem = (BookItemModel) obj;
        var param = new NavigationParameters();
        param.Add("BookItem", bookItem);
        NavigationService.NavigateAsync("AddBookPage", param);
    }


    public override void OnNavigatedTo(INavigationParameters parameters)
    {
        base.OnNavigatedTo(parameters);
        this.LoadDataAsync();
    }

    private async Task LoadDataAsync()
    {
        BookItemSource = new ObservableCollection<BookItemModel>(await MauiProgram.Database.GetAllBookAsync());
    }

    private ObservableCollection<BookItemModel> bookItemSource = new();
    public ObservableCollection<BookItemModel> BookItemSource
    {        
        get => bookItemSource;
        set => SetProperty(ref bookItemSource, value);
    }
    
    private BookItemModel selectedBookItem = new();
    public BookItemModel SelectedBookItem
    {
        get => selectedBookItem;
        set => SetProperty(ref selectedBookItem, value, selectedItemCommandExcute);
    }

    private async void selectedItemCommandExcute()
    {
        var param = new NavigationParameters();
        param.Add("BookItem", SelectedBookItem);
        await NavigationService.NavigateAsync("AddBookPage", param);    
    }

    public ICommand AddBookCommand { get; set; }
}