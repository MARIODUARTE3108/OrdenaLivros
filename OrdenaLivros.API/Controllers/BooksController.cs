using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrdenaLivros.Application.Contracts;
using OrdenaLivros.Application.Models;
using OrdenaLivros.Domain.Entities;

namespace OrdenaLivros.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookApplicationService _bookAppService;

        public BooksController(IBookApplicationService bookAppService)
        {
            _bookAppService = bookAppService;
        }

        [HttpPost]
        public async Task<IActionResult> CadastrarNoticia(BookModel model)
        {
            try
            {
                await _bookAppService.Add(model);

                return StatusCode(200, new { message = "Livro cadastrado com sucesso!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> BuscarLivros()
        {
            var livros = await _bookAppService.GetAll();
            return StatusCode(200, livros);
        }

        [HttpGet("Ordenados")]
        public async Task<IActionResult> BuscarLivrosOrdenados(bool ascending)
        {
            var livros = await _bookAppService.BooksOrderer(ascending);
            return StatusCode(200, livros);
        }
    }
}
