using Domain.Interfaces;
using Domain.Interfaces.Services;
using Entities.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
	[Route("api/[controller]")]
	[Authorize]
	public class CategoriaController : Controller
	{
		private ICategoria _categoria;
		private ICategoriaService _categoriaService;

		public CategoriaController(ICategoria categoria, ICategoriaService categoriaService)
		{
			_categoria = categoria;
			_categoriaService = categoriaService;
		}

		[HttpGet("/api/ListarCategoriasUsuario")]
		[Produces("application/json")]
		public async Task<object> ListarCategoriasUsuario(string emailUsuario)
		{
			return _categoria.ListarCategoriasUsuarios(emailUsuario);
		}

		[HttpPost("/api/AdiconarCategoria")]
		[Produces("application/json")]
		public async Task<object> AdiconarCategoria(Categoria categoria)
		{
			try
			{
				await _categoriaService.AdicionarCategoria(categoria);
			}
			catch (Exception)
			{
				return Task.FromResult(false);
			}
			return Task.FromResult(true);
		}

		[HttpPut("/api/AtualizarCategoria")]
		[Produces("application/json")]
		public async Task<object> AtualizarCategoria(Categoria categoria)
		{
			try
			{
				await _categoriaService.AtualizarCategoria(categoria);
			}
			catch (Exception)
			{
				return Task.FromResult(false);
			}
			return Task.FromResult(true);
		}

		[HttpDelete("/api/DeletarCategoria")]
		[Produces("application/json")]
		public async Task<object> DeletarCategoria(int idCategoria)
		{
			try
			{
				var categoria = await _categoria.GetEntityById(idCategoria);

				await _categoria.Delete(categoria);

			}
			catch (Exception)
			{
				return Task.FromResult(false);
			}
			return Task.FromResult(true);
		}
	}
}
