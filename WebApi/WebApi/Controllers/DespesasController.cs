using Domain.Interfaces;
using Domain.Interfaces.Services;
using Entities.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace WebApi.Controllers
{
	[Route("api/[controller]")]
	[Authorize]
	public class DespesasController : Controller
	{

		private readonly IDespesa _despesas;
		private readonly IDespesaService _despesaService;

		public DespesasController(IDespesa despesas, IDespesaService despesaService)
		{
			_despesas = despesas;
			_despesaService = despesaService;
		}

		[HttpGet("/api/ListarDespesasUsuario")]
		[Produces("application/json")]
		public async Task<object> ListarDespesasUsuario(string emailUsuario)
		{
			return _despesas.ListarDespesaUsuario(emailUsuario);
		}

		[HttpPost("/api/AdicionarDespesa")]
		[Produces("application/json")]
		public async Task<object> AdicionarDespesa(Despesa despesa)
		{
			try
			{
				await _despesaService.AdicionarDespesa(despesa);
			}
			catch (Exception)
			{
				return Task.FromResult(false);
			}
			return Task.FromResult(true);
		}

		[HttpPut("/api/AtualizarDespesa")]
		[Produces("application/json")]
		public async Task<object> AtualizarDespesa(Despesa despesa)
		{
			try
			{
				await _despesaService.AtualizarDespesa(despesa);
			}
			catch (Exception)
			{
				return Task.FromResult(false);
			}
			return Task.FromResult(true);
		}

		[HttpDelete("/api/DeletarDespesa")]
		[Produces("application/json")]
		public async Task<object> DeletarDespesa(int idDespesa)
		{
			try
			{
				var despesa = await _despesas.GetEntityById(idDespesa);

				await _despesas.Delete(despesa);
			}
			catch (Exception)
			{
				return Task.FromResult(false);
			}
			return Task.FromResult(true);
		}

		[HttpGet("/api/DeletarDespesa")]
		[Produces("application/json")]
		public async Task<object> CarregaGraficoDespesa(string emailUsusario)
		{
			return await _despesaService.CarregarGraficos(emailUsusario);
		}
	}
}
