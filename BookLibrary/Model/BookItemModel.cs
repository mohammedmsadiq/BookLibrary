using System.ComponentModel;
using SQLite;

namespace BookLibrary.Model;

public class BookItemModel 
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Author { get; set; }
    public string Genre { get; set; }
    public string Publisher { get; set; }
    public int PublicationYear { get; set; }
    public DateTime DateCreated { get; set; }

}