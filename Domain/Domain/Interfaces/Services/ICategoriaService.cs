using Entities.Entidades;


namespace Domain.Interfaces.Services
{
    public interface ICategoriaService
    {
        Task AdicionarCategoria(Categoria categoria);
        Task AtualizarCategoria(Categoria categoria);
    }
}
