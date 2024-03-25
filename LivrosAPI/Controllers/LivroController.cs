using LivrosAPI.Models;
using LivrosAPI.Services.LivroService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;

namespace LivrosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivroController : ControllerBase
    {
        private readonly ILivroInterface _livroInterface;
        public LivroController(ILivroInterface livroInterface)
        {
            _livroInterface = livroInterface;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Livro>>> GetAllLivros()
        {
            var livros = await _livroInterface.GetAllLivros();

            if (!livros.Any())
            {
                return NotFound("Nenhum registro localizado!");
            }
            return Ok(livros);
        }

        [HttpGet("{livroId}")]
        public async Task<ActionResult<IEnumerable<Livro>>> GetLivroById(int livroId)
        {
            Livro livro = await _livroInterface.GetLivroById(livroId);
            if (livro == null)
            {
                return NotFound("Registro não encontrado");
            }
            return Ok(livro);
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<Livro>>> CreateLivro(Livro livro)
        {
            IEnumerable<Livro> livros = await _livroInterface.CreateLivro(livro);

            return Ok(livros);
        }
    }
}
