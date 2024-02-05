using Domain.Interfaces.Generics;
using Entities.Entidades;

namespace Domain.Interfaces
{
    public interface ISistemaFinanceiro : IGenerics<SistemaFinanceiro>
    {
        Task<IList<SistemaFinanceiro>> ListarSistemasUsuario(string emailUsuario);
    }
}

