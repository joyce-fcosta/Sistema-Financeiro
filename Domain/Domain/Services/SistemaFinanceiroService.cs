using Domain.Interfaces;
using Domain.Interfaces.Services;
using Entities.Entidades;

namespace Domain.Services
{
    public class SistemaFinanceiroService : ISistemaFinanceiroService
    {
        private readonly ISistemaFinanceiro _sistemaFinanceiroRepository;

        public SistemaFinanceiroService(ISistemaFinanceiro sistemaFinanceiroRepository)
        {
            _sistemaFinanceiroRepository = sistemaFinanceiroRepository;
        }
        public async Task AdicionarSistemaFinanceiro(SistemaFinanceiro sistemaFinanceiro)
        {
            var valido = sistemaFinanceiro.ValidarPropriedadeString(sistemaFinanceiro.Nome, "Nome");
            if (valido)
            {
                var data = DateTime.Now;

                sistemaFinanceiro.DiaFechamento = 1;
                sistemaFinanceiro.Ano = data.Year;
                sistemaFinanceiro.Mes = data.Month;
                sistemaFinanceiro.AnoCopia = data.Year;
                sistemaFinanceiro.MesCopia = data.Month;
                sistemaFinanceiro.GerarCopiaDespesa = true; //Se vai ser preciso copiar a despesa para proximo mês

                await _sistemaFinanceiroRepository.Add(sistemaFinanceiro);
            }
        }

        public async Task AtualizarSistemaFinanceiro(SistemaFinanceiro sistemaFinanceiro)
        {
            var valido = sistemaFinanceiro.ValidarPropriedadeString(sistemaFinanceiro.Nome, "Nome");
            if (valido)
            {
                sistemaFinanceiro.DiaFechamento = 1;
                await _sistemaFinanceiroRepository.Uppdate(sistemaFinanceiro);
            }

        }
    }
}
