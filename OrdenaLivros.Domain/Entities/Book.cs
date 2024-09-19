using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdenaLivros.Domain.Entities
{
    public class Book
    {
        public Book(string title, string authorName, int editionYear)
        {
            Title = title;
            AuthorName = authorName;
            EditionYear = editionYear;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string AuthorName { get; set; }
        public int EditionYear { get; set; }
    }
}
