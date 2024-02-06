using Entities.Entidades;

namespace Domain.Interfaces.Services
{
    public interface IUsuarioSistemaFinanceiroService
    {
        Task CadastrarUsuarioNoSistema(UsuarioSistemaFinanceiro usuarioSistemaFinanceiro);
    }
}
