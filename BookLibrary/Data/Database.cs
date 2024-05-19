using BookLibrary.Model;
using SQLite;

namespace BookLibrary.Data;

public class Database
{
    readonly SQLiteAsyncConnection database;
    public Database(string dbPath)
    {
        database = new SQLiteAsyncConnection(dbPath);
        database.CreateTableAsync<BookItemModel>().Wait();
    }
    
    public Task<List<BookItemModel>> GetAllBookAsync()
    {
        return database.Table<BookItemModel>().ToListAsync();
    }
    
    public Task<int> SaveBookAsync(BookItemModel item)
    {

        if (item.Id != 0)
        {
            return database.UpdateAsync(item);
        }
        else
        {
            return database.InsertAsync(item);
        }
    }
    
    public Task<int> DeleteBookAsync(BookItemModel item)
    {
        return database.DeleteAsync(item);
    }
    
    public Task<BookItemModel> GetItemAsync(int id)
    {
        return database.Table<BookItemModel>().Where(i => i.Id == id).FirstOrDefaultAsync();
    }
}