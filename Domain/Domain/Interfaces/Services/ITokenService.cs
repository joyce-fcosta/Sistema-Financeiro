namespace Domain.Interfaces.Services
{
	public interface ITokenService
	{
		Task LoginSemSenha(string id);

		Task GerarToken(string email);
	}
}
