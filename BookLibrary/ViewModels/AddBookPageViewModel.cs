using System.ComponentModel;
using System.Reflection;
using System.Windows.Input;
using BookLibrary.Model;
using BookLibrary.Services;

namespace BookLibrary.ViewModels;

public class AddBookPageViewModel : BaseViewModel
{
    public AddBookPageViewModel(BaseServices baseServices) : base(baseServices)
    {
        Title= "Add Book";
        SaveButtonText= "Save";
        
        SaveButtonCommand = new Command(async () =>
        {                
            ExecuteAsyncTask(async () =>
            {
                var nullProperties = GetNullProperties();
                if (nullProperties.Any())
                {
                    var nullPropertiesString = string.Join(", ", nullProperties);
                    await PageDialogs.DisplayAlertAsync("Error", $"The following fields are null: {nullPropertiesString}", "OK");
                    return;
                }
                else if (BookItem.PublicationYear < 1000 || BookItem.PublicationYear > DateTime.Now.Year+1)
                {
                    await PageDialogs.DisplayAlertAsync("Error", $"The date is not in range. it needs to be in the range of 1000 until now", "OK");
                    return;
                }
                else
                {
                    BookItem.DateCreated = DateTime.Now;
                    await MauiProgram.Database.SaveBookAsync(bookItem);
                    await NavigationService.GoBackAsync();
                }
            });
        });
        
         DeleteButtonCommand = new Command(async () =>
        {                
            await MauiProgram.Database.DeleteBookAsync(bookItem);
            await NavigationService.GoBackAsync();
        });
    }

    public override void OnNavigatedTo(INavigationParameters parameters)
    {
        base.OnNavigatedTo(parameters);
        if (parameters != null)
        {
            if (parameters.ContainsKey("BookItem"))
            {
                BookItem = parameters.GetValue<object>("BookItem") as BookItemModel;
            }
        }
    }
    
    private BookItemModel bookItem = new();
    public BookItemModel BookItem
    {
        get => bookItem;
        set => SetProperty(ref bookItem, value);
    }

    private string saveButtonText;
    public string SaveButtonText
    {
        get => saveButtonText;
        set => SetProperty(ref saveButtonText, value);
    }
    
    public List<string> GetNullProperties()
    {
        List<string> nullProperties = new List<string>();

        foreach (PropertyInfo property in typeof(BookItemModel).GetProperties())
        {
            if (property.Name == "PublicationYear")
            {
                continue;
            }
            if (property.GetValue(BookItem) == null)
            {
                nullProperties.Add(property.Name);
            }
        }

        return nullProperties;
    }
    
    public ICommand SaveButtonCommand { get; set; }
    public ICommand DeleteButtonCommand { get; set; }
}