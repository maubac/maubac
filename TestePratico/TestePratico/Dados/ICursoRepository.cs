using System.Collections.Generic;
using System.Threading.Tasks;
using TestePratico.Models;

namespace TestePratico.Dados
{
	public interface ICursoRepository
	{
		Task<Curso> AddCurso(Curso Curso);
		Task DeleteCurso(int CursoId);
		Task<Curso> GetCurso(int CursoId);
		Task<IEnumerable<Curso>> GetCursos();
		Task<IEnumerable<Curso>> Search(string Descricao);
		Task<Curso> UpdateCurso(Curso Curso);
	}
}