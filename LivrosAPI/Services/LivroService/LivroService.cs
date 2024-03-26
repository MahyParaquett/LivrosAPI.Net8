using Dapper;
using LivrosAPI.Models;
using System.Data.SqlClient;

namespace LivrosAPI.Services.LivroService
{
    public class LivroService : ILivroInterface
    {
        //injeção de dependencia para eu acessar o appsettings (_configuration)
        //e consequentemente a string de conexão(getConnection)

        private readonly IConfiguration _configuration;
        private readonly string getConnection;
        public LivroService(IConfiguration configutation)
        {
            _configuration = configutation;
            getConnection = _configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<Livro>> CreateLivro(Livro livro)
        {
            using (var con = new SqlConnection(getConnection))
            {
                var sql = "insert into Livros (titulo, autor) values (@titulo, @autor)";
                await con.ExecuteAsync(sql, livro);

                return await con.QueryAsync<Livro>("select * from livros");
                
            }

        }

        public async Task<IEnumerable<Livro>> DeleteLivro(int livroId)
        {
            using (var con = new SqlConnection(getConnection))
            {
                var sql = "delete from Livros where id = @Id";
                await con.ExecuteAsync(sql, new { Id = livroId });
                
                return await con.QueryAsync<Livro>("select * from Livros");
            }
        }

        public async Task<IEnumerable<Livro>> GetAllLivros()
        {//using abre a string de conexão e fecha ao fim do bloco
            using (var con = new SqlConnection(getConnection))
            {
                var sql = "select * from Livros";
                //Entre <tipo de retorno que eu espero>(comando sql)
                return await con.QueryAsync<Livro>(sql);
            }
        }

        public async Task<Livro> GetLivroById(int livroId)
        {
            using (var con = new SqlConnection(getConnection))
            {
                var sql = "select * from Livros where id = @Id";
                //Entre <tipo de retorno que eu espero>(comando sql)
                return await con.QueryFirstOrDefaultAsync<Livro>(sql, new {Id = livroId});
            }
        }

        public async Task<IEnumerable<Livro>> UpdateLivro(Livro livro)
        {
            using (var con = new SqlConnection(getConnection))
            {
                var sql = "update Livros set titulo= @titulo, autor= @autor where id = @Id";
                
                await con.ExecuteAsync(sql, livro);

                return await con.QueryAsync<Livro>("select * from Livros");
            }
        }
    }
}
