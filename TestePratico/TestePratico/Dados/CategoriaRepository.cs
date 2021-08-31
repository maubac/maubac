using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestePratico.Models;

namespace TestePratico.Dados
{
	public class CategoriaRepository : ICategoriaRepository
	{
		private readonly AppDbContext appDbContext;

		public CategoriaRepository(AppDbContext appDbContext)
		{
			this.appDbContext = appDbContext;
		}

		public async Task<Categoria> GetCategoria(string Codigo)
		{
			return await appDbContext.Categorias
				.FirstOrDefaultAsync(d => d.Codigo == Codigo);
		}

		public async Task<IEnumerable<Categoria>> GetCategoria()
		{
			return await appDbContext.Categorias.ToListAsync();
		}
	}
}
