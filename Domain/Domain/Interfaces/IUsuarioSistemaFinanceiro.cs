using Domain.Interfaces.Generics;
using Entities.Entidades;

namespace Domain.Interfaces
{
    public interface IUsuarioSistemaFinanceiro : IGenerics<UsuarioSistemaFinanceiro>
    {
        Task<IList<UsuarioSistemaFinanceiro>> ListarUsuariosSistema(int idSistema);
        Task RemoveUsuarios(List<UsuarioSistemaFinanceiro> usuarios);
        Task<UsuarioSistemaFinanceiro> ObterUsuarioPorEmail(string emailUsuario);
    }
}
