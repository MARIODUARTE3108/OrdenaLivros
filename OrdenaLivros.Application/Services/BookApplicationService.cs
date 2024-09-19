using OrdenaLivros.Application.Contracts;
using OrdenaLivros.Application.Models;
using OrdenaLivros.Application.Validators;
using OrdenaLivros.Core.Repositories;
using OrdenaLivros.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdenaLivros.Application.Services
{
    public class BookApplicationService : IBookApplicationService
    {
        private readonly IBookRepository _bookRepository;

        public BookApplicationService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public Task<List<Book>> BooksOrderer(bool ascending)
        {
            return _bookRepository.BooksOrderer(ascending);
        }
        public async Task<Book> Add(BookModel model)
        {
            var validator = new CreateBookValidator();
            var validationResult = await validator.ValidateAsync(model);

            if (!validationResult.IsValid)
            {
                var errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new Exception($"Não pode ter campos vazios");
            }

            var existingBook = await _bookRepository.SearchByLivro(new Book(model.Title, model.AuthorName, model.EditionYear));

            if (existingBook != null)
            {
                throw new Exception("Livro já cadastrado");
            }

            var newBook = new Book(model.Title, model.AuthorName, model.EditionYear);
            return await _bookRepository.Add(newBook);
        }
        public Task<List<Book>> GetAll()
        {
            return _bookRepository.GetAll();
        }
    }
}
