namespace WordAnalyzer
{
    /// <summary>
    /// This class is used to represent a book structure
    /// </summary>
    public class Book
    {
        public string Title { get; }
        
        public string Author { get; }
        
        public string Text { get; }
        
        public Book(string title, string author, string text)
        {
            this.Title = title;
            this.Author = author;
            this.Text = text;
        }
    }
}