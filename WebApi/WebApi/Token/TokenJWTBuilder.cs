using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace WebApi.Token
{
	public class TokenJWTBuilder
	{
		private SecurityKey securityKey = null; // A key que vai ser gerada
		private string subject = ""; //O nome da aplicação
		private string issuer = ""; //Remetente
		private string audience = ""; //O token do receptor
		private Dictionary<string, string> claims = new Dictionary<string, string>();// quando os tokens forem gerados vão ser dado ao usuário que vai acessar, onde vão estar  definidos nos controllers
		private int expiryInMinutes = 10; //Tempo de expiração

		public TokenJWTBuilder AddSubject(string subject)
		{
			this.subject = subject;
			return this;
		}

		public TokenJWTBuilder AddSecurityKey(SecurityKey securityKey)
		{
			this.securityKey = securityKey;
			return this;
		}
		public TokenJWTBuilder AddIssuer(string issuer)
		{
			this.issuer = issuer;
			return this;
		}
		public TokenJWTBuilder AddAudience(string audience)
		{
			this.audience = audience;
			return this;
		}
		public TokenJWTBuilder AddExpiryInMinutes(int expiryInMinutes)
		{
			this.expiryInMinutes = expiryInMinutes;
			return this;
		}
		public TokenJWTBuilder AddClaims(Dictionary<string, string> claims) //Adiciona o tipo e o valor 
		{
			this.claims.Union(claims);
			return this;
		}
		public TokenJWTBuilder AddClaims(string type, string value)
		{
			this.claims.Add(type, value);
			return this;
		}


		public TokenJWT Builder()
		{
			EnsureArguments(); //Validações do que vamos precisar para gerar tokens 

			var claims = new List<Claim>
			{
				new Claim(JwtRegisteredClaimNames.Sub,this.subject),
				new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
			}.Union(this.claims.Select(item => new Claim(item.Key, item.Value)));


			var token = new JwtSecurityToken(
				issuer: this.issuer,
				audience: this.audience,
				claims: claims,
				expires: DateTime.UtcNow.AddMinutes(this.expiryInMinutes),
				signingCredentials: new SigningCredentials(this.securityKey, SecurityAlgorithms.HmacSha256));

			return new TokenJWT(token);
		}

		private void EnsureArguments()
		{
			if (this.securityKey == null) throw new ArgumentNullException("Security key");
			if (string.IsNullOrWhiteSpace(this.subject) == null) throw new ArgumentNullException("Subject");
			if (string.IsNullOrWhiteSpace(this.audience) == null) throw new ArgumentNullException("Audience");
			if (string.IsNullOrWhiteSpace(this.issuer) == null) throw new ArgumentNullException("Issuer");

			if (this.securityKey == null) throw new ArgumentNullException("Security key");
		}
	}
}
