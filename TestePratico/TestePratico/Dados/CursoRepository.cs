using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestePratico.Models;

namespace TestePratico.Dados
{
	public class CursoRepository : ICursoRepository
	{
		private readonly AppDbContext appDbContext;

		public CursoRepository(AppDbContext appDbContext)
		{
			this.appDbContext = appDbContext;
		}

		public async Task<Curso> AddCurso(Curso Curso)
		{
			if (Curso.CategoriaId > 0)
			{
				Curso.Categoria = appDbContext.Categorias.FirstOrDefault(x => x.Id == Curso.CategoriaId);
				appDbContext.Entry(Curso.Categoria).State = EntityState.Unchanged;
			}

			var result = await appDbContext.Cursos.AddAsync(Curso);
			await appDbContext.SaveChangesAsync();
			return result.Entity;
		}

		public async Task DeleteCurso(int CursoId)
		{
			var result = await appDbContext.Cursos
				.FirstOrDefaultAsync(e => e.Id == CursoId);

			if (result != null)
			{
				appDbContext.Cursos.Remove(result);
				await appDbContext.SaveChangesAsync();
			}
		}

		public async Task<Curso> GetCurso(int CursoId)
		{
			return await appDbContext.Cursos
				.Include(e => e.Categoria)
				.FirstOrDefaultAsync(e => e.Id == CursoId);
		}

		public async Task<IEnumerable<Curso>> GetCursos()
		{
			return await appDbContext.Cursos.ToListAsync();
		}

		public async Task<IEnumerable<Curso>> Search(string Descricao)
		{
			IQueryable<Curso> query = appDbContext.Cursos;

			if (!string.IsNullOrEmpty(Descricao))
			{
				query = query.Where(e => e.Descricao.Contains(Descricao));
			}

			return await query.ToListAsync();
		}

		public async Task<Curso> UpdateCurso(Curso Curso)
		{
			var result = await appDbContext.Cursos
				.FirstOrDefaultAsync(e => e.Id == Curso.Id);

			if (result != null)
			{
				result.Descricao = Curso.Descricao;
				result.DataInicio = Curso.DataInicio;
				result.DataFim = Curso.DataFim;
				result.QuantidadeAlunos = Curso.QuantidadeAlunos;
				result.Categoria = Curso.Categoria;

				await appDbContext.SaveChangesAsync();

				return result;
			}

			return null;
		}
	}
}
