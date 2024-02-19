using System.IdentityModel.Tokens.Jwt;

namespace WebApi.Token
{
    public class TokenJWT
    {
        private readonly JwtSecurityToken token;
        
        internal TokenJWT(JwtSecurityToken token)
        {
            this.token = token;
        }

        public DateTime ValidTo => token.ValidTo; //tempo em que o token vai permanecer valido

        public string value => new JwtSecurityTokenHandler().WriteToken(token); //Vai pegar e escrever no SecurityTokenHandler 
	}
}
