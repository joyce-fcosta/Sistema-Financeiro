using Domain.Interfaces;
using Domain.Interfaces.Services;
using Entities.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
	[Route("api/[controller]")]
	[Authorize]
	public class UsuarioSistemaFinanceiroController : Controller
	{

		public readonly IUsuarioSistemaFinanceiro _usuarioSistemaFinanceiro;
		public readonly IUsuarioSistemaFinanceiroService _usuarioSistemaFinanceiroService;
		public UsuarioSistemaFinanceiroController(IUsuarioSistemaFinanceiro usuarioSistemaFinanceiro, IUsuarioSistemaFinanceiroService usuarioSistemaFinanceiroService)
		{
			_usuarioSistemaFinanceiro = usuarioSistemaFinanceiro;
			_usuarioSistemaFinanceiroService = usuarioSistemaFinanceiroService;
		}

		[HttpGet("/api/ListaSistemaUsuario")]
		[Produces("application/json")]
		public async Task<object> ListaSistemaUsuario(int idSistema)
		{
			return await _usuarioSistemaFinanceiro.ListarUsuariosSistema(idSistema);
		}

		[HttpPost("/api/CadastrarUsuarioNoSistema")]
		[Produces("application/json")]
		public async Task<object> CadastrarUsuarioNoSistema([FromBody] UsuarioSistemaFinanceiro usuarioSistemaFinanceiro)
		{
			try
			{
				await _usuarioSistemaFinanceiroService.CadastrarUsuarioNoSistema(usuarioSistemaFinanceiro);
			}
			catch (Exception)
			{
				return Task.FromResult(false);
			}
			return Task.FromResult(true);
		}

		[HttpDelete("/api/DeletaUsuarioNoSistema")]
		[Produces("application/json")]
		public async Task<object> DeletaUsuarioNoSistema(int idSistema)
		{
			try
			{
				var usuarioSistemaFinanceiro = await _usuarioSistemaFinanceiro.GetEntityById(idSistema);

				await _usuarioSistemaFinanceiro.Delete(usuarioSistemaFinanceiro);
			}
			catch (Exception)
			{
				return Task.FromResult(false);
			}
			return Task.FromResult(true);
		}

		[HttpPut("/api/DeletaUsuarioNoSistema")]
		[Produces("application/json")]
		public async Task<object> AtualizaUsuarioNoSistema([FromBody] UsuarioSistemaFinanceiro usuarioSistemaFinanceiro)
		{
			try
			{
				await _usuarioSistemaFinanceiro.Uppdate(usuarioSistemaFinanceiro);
			}
			catch (Exception)
			{
				return Task.FromResult(false);
			}
			return Task.FromResult(true);
		}
	}
}
