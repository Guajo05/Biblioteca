namespace Biblioteca;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public int Year { get; set; }

    public Book(int id, string title, string author, int year)
    {
        Id = id;
        Title = title;
        Author = author;
        Year = year;
    }

    public override string ToString()
    {
        return $"[{Id,3}] {Title} - {Author} ({Year})";
    }
}