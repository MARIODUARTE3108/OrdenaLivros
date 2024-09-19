using OrdenaLivros.Application.Models;
using OrdenaLivros.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdenaLivros.Application.Contracts
{
    public interface IBookApplicationService
    {
        Task<List<Book>> BooksOrderer(bool ascending);
        Task<Book> Add(BookModel model);
        Task<List<Book>> GetAll();
    }
}
