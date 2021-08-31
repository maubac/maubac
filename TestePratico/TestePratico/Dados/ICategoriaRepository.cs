using System.Collections.Generic;
using System.Threading.Tasks;
using TestePratico.Models;

namespace TestePratico.Dados
{
	public interface ICategoriaRepository
	{
		Task<IEnumerable<Categoria>> GetCategoria();
		Task<Categoria> GetCategoria(string Codigo);
	}
}