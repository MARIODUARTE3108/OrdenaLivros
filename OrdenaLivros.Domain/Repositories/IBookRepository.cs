using OrdenaLivros.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdenaLivros.Core.Repositories
{
    public interface IBookRepository
    {
        Task<List<Book>> BooksOrderer(bool ascending);
        Task<Book> Add(Book book);
        Task<List<Book>> GetAll();
        Task<Book> SearchByLivro(Book book);
    }
}
