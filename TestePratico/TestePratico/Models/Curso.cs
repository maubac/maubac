using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestePratico.Models
{
	public class Curso
	{
		public Curso() { }

		public Curso(String pDescricao, DateTime pDataInicio, DateTime pDataFim, Int32 pQtdAlunos, Int32 pCategoria)
		{
			this.Descricao = pDescricao;
			this.DataInicio = pDataInicio;
			this.DataFim = pDataFim;
			this.QuantidadeAlunos = pQtdAlunos;
			this.CategoriaId = pCategoria;
		}

		[Required]
		public int Id { get; set; }
		public string Descricao { get; set; }
		public DateTime DataInicio { get; set; }
		public DateTime DataFim { get; set; }
		public Int32 QuantidadeAlunos { get; set; }
		public Int32 CategoriaId { get; set; }
		public Categoria Categoria { get; set; }
	}
}
