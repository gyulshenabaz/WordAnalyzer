using System.Linq;
using System.Text.RegularExpressions;

namespace WordAnalyzer
{
    /// <summary>
    /// This class is used for removal of any images and notes from a book 
    /// </summary>
    public class BookReader
    {
        /// <summary>Returns all words in a book</summary>
        /// <param name="bookText">The raw text content of a book.</param>
        public virtual string[] GetAllWords(string bookText)
        {
            string cleanTextBook = RemoveUnnecessaryContent(bookText);
            
            return ExtractAllWords(cleanTextBook);
        }

        /// <summary>Extracts all words in a book using Regex</summary>
        /// Information about why this regex was used: https://www.regular-expressions.info/unicode.html
        /// <param name="bookText">The raw text content of a book.</param>
        private string[] ExtractAllWords(string bookText)
        {
            string regexPattern = @"[\p{L}-]+";
            var wordRegex = new Regex(regexPattern);

            var words = wordRegex.Matches(bookText)
                .Select(m => m.Value)
                .ToArray();

            return words;
        }
        
        /// <summary>Removes unnecessary content from the Chitanka books.
        /// Chitanka books have embedded image tags to add illustrations to a book
        /// </summary>
        /// <param name="bookText">The raw text content of a book.</param>
        private string RemoveUnnecessaryContent(string bookText)
        {
            var imageRegex = new Regex(@"{img:.+}");
            var textWithoutImages  = imageRegex.Replace(bookText, string.Empty);
            
            return textWithoutImages;
        }
    }
}