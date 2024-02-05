using Domain.Interfaces.Generics;
using Entities.Entidades;



namespace Domain.Interfaces
{
    public interface ICategoria : IGenerics<Categoria>
    {
        Task<IList<Categoria>> ListarCategoriasUsuarios(string emailUsuario);
    }
}
