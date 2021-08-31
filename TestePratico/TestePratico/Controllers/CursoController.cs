using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestePratico.Dados;
using TestePratico.Models;

namespace TestePratico.Controllers
{
	[ApiController]
	[Route("/api/Curso")]
	public class CursoController : ControllerBase
	{
		private readonly ICursoRepository CursoRepository;

		public CursoController(ICursoRepository CursoRepository)
		{
			this.CursoRepository = CursoRepository;
		}

		[HttpGet("{search}")]
		public async Task<ActionResult<IEnumerable<Curso>>> Search(string name)
		{
			try
			{
				var result = await CursoRepository.Search(name);

				if (result.Any())
				{
					return Ok(result);
				}

				return NotFound();
			}
			catch (Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError,
				"Error retrieving data from the database");
			}
		}

		[HttpGet]
		public async Task<ActionResult> GetCursos()
		{
			try
			{
				return Ok(await CursoRepository.GetCursos());
			}
			catch (Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError,
					"Error retrieving data from the database");
			}
		}

		[HttpGet("{id:int}")]
		public async Task<ActionResult<Curso>> GetCurso(int id)
		{
			try
			{
				var result = await CursoRepository.GetCurso(id);

				if (result == null)
				{
					return NotFound();
				}

				return result;
			}
			catch (Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError,
					"Error retrieving data from the database");
			}
		}

		[HttpPost]
		public async Task<ActionResult<Curso>> CreateCurso(Curso pCurso)
		{
			try
			{
				if (pCurso == null)
					return BadRequest();

				var lCurso = await CursoRepository.GetCurso(pCurso.Id);

				if (lCurso != null)
				{
					ModelState.AddModelError("Curso", "Curso already in use");
					return BadRequest(ModelState);
				}

				if (pCurso.DataFim < pCurso.DataInicio)
				{
					ModelState.AddModelError("Curso", "Curso DataFim invalid");
					return BadRequest(ModelState);
				}

				var lCursos = CursoRepository.GetCursos().Result.FirstOrDefault(x => x.DataInicio >= pCurso.DataInicio && x.DataInicio <= pCurso.DataFim
					|| x.DataFim >= pCurso.DataInicio && x.DataFim <= pCurso.DataFim);

				if (lCursos != null)
				{
					ModelState.AddModelError("Curso", "Existe(m) curso(s) planejados(s) dentro do período informado.");
					return BadRequest(ModelState);
				}

				var createdCursos = await CursoRepository.AddCurso(pCurso);

				return CreatedAtAction(nameof(GetCurso),
					new { id = createdCursos.Id }, createdCursos);
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, "Error creating new Curso record");
			}
		}

		[HttpPut("{id:int}")]
		public async Task<ActionResult<Curso>> UpdateCurso(int id, Curso pCurso)
		{
			try
			{
				if (id != pCurso.Id)
					return BadRequest("Curso ID mismatch");

				var CursoToUpdate = await CursoRepository.GetCurso(id);

				if (CursoToUpdate == null)
				{
					return NotFound($"Curso with Id = {id} not found");
				}

				return await CursoRepository.UpdateCurso(pCurso);
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, "Error updating Curso record");
			}
		}

		[HttpDelete("{id:int}")]
		public async Task<ActionResult> DeleteCurso(int id)
		{
			try
			{
				var CursoToDelete = await CursoRepository.GetCurso(id);

				if (CursoToDelete == null)
				{
					return NotFound($"Curso with Id = {id} not found");
				}

				await CursoRepository.DeleteCurso(id);

				return Ok($"Curso with Id = {id} deleted");
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting Curso record");
			}
		}
	}
}