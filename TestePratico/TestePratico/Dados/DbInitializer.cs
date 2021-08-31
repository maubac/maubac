using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestePratico.Models;

namespace TestePratico.Dados
{
	public static class DbInitializer
	{
		public static void Initialize(AppDbContext context)
		{
			context.Database.EnsureCreated();

			if (context.Categorias.Any())
			{
				return;
			}

			var categorias = new Categoria[]
			{
				new Categoria { Descricao = "Comportamental", Codigo = "01" },
				new Categoria { Descricao = "Programação", Codigo = "02" },
				new Categoria { Descricao = "Qualidade", Codigo = "03" },
				new Categoria { Descricao = "Processos", Codigo = "04" }
			};

			foreach (var Categ in categorias)
			{
				context.Categorias.Add(Categ);
			}
			context.SaveChanges();
		}
	}
}
