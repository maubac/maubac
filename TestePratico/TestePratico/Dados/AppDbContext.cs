using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using TestePratico.Models;

namespace TestePratico.Dados
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
		}

		public DbSet<Curso> Cursos { get; set; }
		public DbSet<Categoria> Categorias { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			ConfiguraCurso(modelBuilder);
			ConfiguraCategoria(modelBuilder);
		}

		private void ConfiguraCurso(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Curso>(Cur =>
			{
				Cur.ToTable("tbCurso");
				Cur.HasKey(c => c.Id).HasName("id");
				Cur.Property(c => c.Id).HasColumnName("id").ValueGeneratedOnAdd();
				Cur.Property(c => c.Descricao).HasColumnName("Descricao").HasMaxLength(100);
				Cur.Property(c => c.DataInicio).HasColumnName("DataInicio");
				Cur.Property(c => c.DataFim).HasColumnName("DataFim");
				Cur.Property(c => c.QuantidadeAlunos).HasColumnName("QuantidadeAlunos");
				Cur.HasOne(c => c.Categoria);
			});
		}

		private void ConfiguraCategoria(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Categoria>(Cat =>
			{
				Cat.ToTable("tbCategoria");
				Cat.HasKey(c => c.Id).HasName("id");
				Cat.Property(c => c.Id).HasColumnName("id").ValueGeneratedOnAdd();
				Cat.Property(p => p.Codigo).HasColumnName("codigo");
				Cat.Property(p => p.Descricao).HasColumnName("Descricao");
			});
		}
	}
}