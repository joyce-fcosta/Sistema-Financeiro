using Entities.Entidades;

namespace Domain.Interfaces.Services
{
	public interface IDespesaService
	{
		Task AdicionarDespesa(Despesa despesa);
		Task AtualizarDespesa(Despesa despesa);
		Task<object> CarregarGraficos(string emailUsuario);

	}
}
