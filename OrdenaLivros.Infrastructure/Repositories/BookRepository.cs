using Microsoft.EntityFrameworkCore;
using OrdenaLivros.Core.Repositories;
using OrdenaLivros.Domain.Entities;
using OrdenaLivros.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdenaLivros.Infrastructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly SqlServerContext _context;

        public BookRepository()
        {
            _context = new SqlServerContext();
        }

        public async Task<List<Book>> BooksOrderer(bool ascending)
        {
            var query = _context.Book.AsQueryable();

            if (ascending)
            {
                query = query.OrderBy(x => x.Title)
                             .ThenBy(x => x.AuthorName);
            }
            else
            {
                query = query.OrderByDescending(x => x.Title)
                             .ThenByDescending(x => x.AuthorName);
            }

            return await query.ToListAsync();
        }

        public async Task<Book> Add(Book book)
        {
            _context.Book.Add(book);
            _context.SaveChanges();

            return book;
        }

        public async Task<List<Book>> GetAll()
        {
            return await _context.Book.ToListAsync();
        }
        public async Task<Book> SearchByLivro(Book book)
        {
            return await _context.Book
                    .FirstOrDefaultAsync(x => x.Title == book.Title
                                            && x.AuthorName == book.AuthorName
                                            && x.EditionYear == book.EditionYear);
        }
    }
}
