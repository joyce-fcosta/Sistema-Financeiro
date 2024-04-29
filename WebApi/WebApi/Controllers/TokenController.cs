using Entities.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Token;

namespace WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TokenController : ControllerBase
	{

		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		public TokenController(UserManager<ApplicationUser> userManager,
			SignInManager<ApplicationUser> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}

		[AllowAnonymous]
		[Produces("application/json")]
		[HttpPost("/api/CreateToken")]
		public async Task<IActionResult> CreateToken([FromBody] InputModel input)
		{
			if (string.IsNullOrWhiteSpace(input.Email) || string.IsNullOrWhiteSpace(input.Password))
			{
				return Unauthorized();// retorna não autorizado
			}

			var result = await _signInManager.PasswordSignInAsync(input.Email, input.Password, false, lockoutOnFailure: true);

			if (result.Succeeded)
			{
				var token = new TokenJWTBuilder()
					.AddSecurityKey(JwtSecurityKey.Create("this is my custom Secret key for authentication"))
					.AddSubject("Sistema Financeiro")
					.AddIssuer("Teste.Security.Bearer")
					.AddAudience("Teste.Security.Bearer")
					.AddClaims("UsuarioAPINumero", "1")
					.AddExpiryInMinutes(5)
					.Builder();

				return Ok(token.value);
			}

			return Unauthorized();

		}

		[AllowAnonymous]
		[Produces("application/json")]
		[HttpPost("/api/CreateToken")]
		public async Task<IActionResult> RefreshToken(string id)
		{
			var usuario = _userManager.FindByIdAsync(id);

			/*			var dataExpiracaoAcessToken = DateTime.Now.AddMinutes(5);
						var dataExpiracaoRefreshToken = DateTime.Now.AddMinutes(30);*/


			var acessToken = new TokenJWTBuilder()
					.AddSecurityKey(JwtSecurityKey.Create("this is my custom Secret key for authentication"))
					.AddSubject("Sistema Financeiro")
					.AddIssuer("Teste.Security.Bearer")
					.AddAudience("Teste.Security.Bearer")
					.AddClaims("UsuarioAPINumero", "1")
					.AddExpiryInMinutes(2)
					.Builder();

			var refreshToken = new TokenJWTBuilder()
					.AddSecurityKey(JwtSecurityKey.Create("this is my custom Secret key for authentication"))
					.AddSubject("Sistema Financeiro")
					.AddIssuer("Teste.Security.Bearer")
					.AddAudience("Teste.Security.Bearer")
					.AddExpiryInMinutes(30)
					.Builder();
			var retorno = new TokenModel
			{
				AccessToken = acessToken.value,
				RefreshToken = refreshToken.value
			};

			return Ok(retorno);

		}
	}
}
