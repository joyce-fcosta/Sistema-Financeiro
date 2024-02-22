using Domain.Interfaces;
using Domain.Interfaces.Services;
using Entities.Entidades;

namespace Domain.Services
{
	public class DespesaService : IDespesaService
	{
		private readonly IDespesa _despesaRepository;

		public DespesaService(IDespesa despesaRepository)
		{
			_despesaRepository = despesaRepository;
		}
		public async Task AdicionarDespesa(Despesa despesa)
		{
			var data = DateTime.Now;
			despesa.DataCadastro = data;
			despesa.Ano = data.Year;
			despesa.Mes = data.Month;

			var valido = despesa.ValidarPropriedadeString(despesa.Nome, "Nome");
			if (valido)
				await _despesaRepository.Add(despesa);
		}

		public async Task AtualizarDespesa(Despesa despesa)
		{
			var data = DateTime.Now;
			despesa.DataAlteracao = data;

			if (despesa.Pago)
				despesa.DataPagamento = data;


			var valido = despesa.ValidarPropriedadeString(despesa.Nome, "Nome");
			if (valido)
				await _despesaRepository.Add(despesa);

		}

		public async Task<object> CarregarGraficos(string emailUsuario)
		{
			var despesasUsuario = await _despesaRepository.ListarDespesaUsuario(emailUsuario);
			var despesasAnterior = await _despesaRepository.ListarDespesasNaoPagasMesesAnteriores(emailUsuario);

			var despesasNaoPagasMesesAnteriores = despesasAnterior.Any() ? despesasAnterior.ToList().Sum(x => x.Valor) : 0;

			var despesasPagas = despesasUsuario.Where(d => d.Pago && d.TipoDespesa == Entities.Enums.EnumTipoDespesa.Conta).Sum(x => x.Valor);

			var despesasPendentes = despesasUsuario.Where(d => !d.Pago && d.TipoDespesa == Entities.Enums.EnumTipoDespesa.Conta).Sum(x => x.Valor);

			var investimentos = despesasUsuario.Where(d => d.TipoDespesa == Entities.Enums.EnumTipoDespesa.Conta).Sum(x => x.Valor);

			return new
			{
				sucesso = "OK",
				despesasPagas = despesasPagas,
				despesasPendentes = despesasPendentes,
				despesasNaoPagasMesesAnteriores = despesasNaoPagasMesesAnteriores,
				investimentos = investimentos
			};
		}
	}
}
