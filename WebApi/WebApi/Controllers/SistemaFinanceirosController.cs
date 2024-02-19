using Domain.Interfaces;
using Domain.Interfaces.Services;
using Entities.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace WebApi.Controllers
{
	public class SistemaFinanceirosController : Controller
	{
		private readonly ISistemaFinanceiro _ISistemaFinanceiro;
		private readonly ISistemaFinanceiroService _ISistemaFinanceiroService;

		public SistemaFinanceirosController(ISistemaFinanceiro sistemaFinanceiro, ISistemaFinanceiroService sistemaFinanceiroService)
		{
			_ISistemaFinanceiro = sistemaFinanceiro;
			_ISistemaFinanceiroService = sistemaFinanceiroService;
		}

		[HttpGet("/api/ListarSistemasUsuario")]
		[Produces("application/json")]
		public async Task<object> ListaSistemaUsuario(string emailUsuario)
		{
			return await _ISistemaFinanceiro.ListarSistemasUsuario(emailUsuario);
		}

		[HttpGet("/api/ObterSistemaUsuario")]
		[Produces("application/json")]
		public async Task<object> ObterSistemaUsuario(int id)
		{
			return await _ISistemaFinanceiro.GetEntityById(id);
		}

		[HttpDelete("/api/DeleteSistemaUsuario")]
		[Produces("application/json")]
		public async Task<object> DeleteSistemaUsuario(int id)
		{
			var sistemaFinanceiro = await _ISistemaFinanceiro.GetEntityById(id);

			await _ISistemaFinanceiro.Delete(sistemaFinanceiro);

			return Task.FromResult(sistemaFinanceiro);
		}

		[HttpPost("/api/AdicionaSistemFinanceiro")]
		[Produces("application/json")]
		public async Task<object> AdicionaSistemFinanceiro(SistemaFinanceiro sistemaFinanceiro)
		{
			await _ISistemaFinanceiroService.AdicionarSistemaFinanceiro(sistemaFinanceiro);

			return Task.FromResult(sistemaFinanceiro); //retorna o objeto se adicionou
		}

		[HttpPut("/api/AtualizarSistemaFinanceiro")]
		[Produces("application/json")]
		public async Task<object> AtualizarSistemaFinanceiro(SistemaFinanceiro sistemaFinanceiro)
		{
			await _ISistemaFinanceiroService.AtualizarSistemaFinanceiro(sistemaFinanceiro);

			return Task.FromResult(sistemaFinanceiro); //retorna o objeto se adicionou
		}
	}
}
