using Domain.Interfaces.Generics;
using Entities.Entidades;

namespace Domain.Interfaces
{
    public interface IDespesa : IGenerics<Despesa>
    {
        Task<IList<Despesa>> ListarDespesaUsuario(string emailUsuario);

        Task<IList<Despesa>> ListarDespesasNaoPagasMesesAnteriores(string emailUsuario);    
    }
}
