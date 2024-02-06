using Entities.Entidades;

namespace Domain.Interfaces.Services
{
    public interface ISistemaFinanceiroService
    {
        Task AdicionarSistemaFinanceiro(SistemaFinanceiro sistemaFinanceiro);
        Task AtualizarSistemaFinanceiro(SistemaFinanceiro sistemaFinanceiro);
    }
}
