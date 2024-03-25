using LivrosAPI.Models;

namespace LivrosAPI.Services.LivroService
{
    public interface ILivroInterface
    {
        //IEnurable --> é igual uma lista
        Task<IEnumerable<Livro>> GetAllLivros();
        Task<Livro> GetLivroById(int livroId);
        Task<IEnumerable<Livro>> CreateLivro(Livro livro);
        Task<IEnumerable<Livro>> UpdateLivro(Livro livro);
        Task<IEnumerable<Livro>>DeleteLivro(int livroId);

    }
}
