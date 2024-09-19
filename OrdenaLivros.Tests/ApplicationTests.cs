using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrdenaLivros.Application.Contracts;
using OrdenaLivros.Application.Models;
using OrdenaLivros.Application.Services;
using OrdenaLivros.Core.Repositories;
using OrdenaLivros.Domain.Entities;
using OrdenaLivros.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdenaLivros.Tests
{
    public class ApplicationTests
    {
        private readonly IBookApplicationService _bookAppService;
        public ApplicationTests()
        {
            var service = new ServiceCollection();
            service.AddTransient<IBookApplicationService, BookApplicationService>();
            service.AddTransient<IBookRepository, BookRepository>();

            var provedor = service.BuildServiceProvider();
            _bookAppService = provedor.GetService<IBookApplicationService>();
        }

        [Fact]
        public async Task Cadastrar_Livro()
        {
            var books = new List<BookModel>
            {
                new() { Title = "Java How to Program", AuthorName = "Deitel & Deitel", EditionYear = 2007 },
                new() { Title = "Patterns of Enterprise Application Architecture", AuthorName = "Martin Fowler", EditionYear = 2002 },
                new() { Title = "Head First Design Patterns", AuthorName = "Elisabeth Freeman", EditionYear = 2004 },
                new() { Title = "Internet & World Wide Web: How to Program", AuthorName = "Deitel & Deitel", EditionYear = 2007 }
            };

            foreach (var book in books)
            {
                var bookCreated = await _bookAppService.Add(book);
                Assert.NotNull(bookCreated);
            }
        }

        [Fact]
        public async Task Cadastrar_Livro_Duplicado_Lanca_Excecao()
        {
            var book = new BookModel
            {
                Title = "Java How to Program",
                AuthorName = "Deitel & Deitel",
                EditionYear = 2007
            };

            var exception = await Assert.ThrowsAsync<Exception>(async () => await _bookAppService.Add(book));
            Assert.Equal("Livro já cadastrado", exception.Message);
        }
        [Fact]
        public async Task Cadastrar_Livro_Vazio_Lanca_Excecao()
        {
            var book = new BookModel
            {
                Title = "",
                AuthorName = "Deitel & Deitel",
                EditionYear = 2007
            };

            var exception = await Assert.ThrowsAsync<Exception>(async () => await _bookAppService.Add(book));
            Assert.Equal("Não pode ter campos vazios", exception.Message);
        }
    }
}
