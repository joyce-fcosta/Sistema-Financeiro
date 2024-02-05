using Domain.Interfaces;
using Entities.Entidades;
using Infra.Configuration;
using Infra.Repository.Generics;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repository
{
    public class UsuarioSistemaFinanceiroRepository : GenericsRepository<UsuarioSistemaFinanceiro>, IUsuarioSistemaFinanceiro
    {
        private readonly DbContextOptions<ApplicationContext> _optionsOptions;
        public UsuarioSistemaFinanceiroRepository()
        {
            _optionsOptions = new DbContextOptions<ApplicationContext>();
        }
        public async Task<IList<UsuarioSistemaFinanceiro>> ListarUsuariosSistema(int idSistema)
        {
            using (var banco = new ApplicationContext(_optionsOptions))
            {
                return await (from us in banco.UsuarioSistemaFinanceiro
                              where us.IdSistema == idSistema
                              select us).AsNoTracking().ToListAsync();

            }
        }

        public async Task<UsuarioSistemaFinanceiro> ObterUsuarioPorEmail(string emailUsuario)
        {
            using (var banco = new ApplicationContext(_optionsOptions))
            {
                return await (from us in banco.UsuarioSistemaFinanceiro
                              where us.EmailUsuario.Equals(emailUsuario)
                              select us).AsNoTracking().FirstOrDefaultAsync();
            }
        }

        public async Task RemoveUsuarios(List<UsuarioSistemaFinanceiro> usuarios)
        {
            using (var banco = new ApplicationContext(_optionsOptions))
            {
                banco.UsuarioSistemaFinanceiro.RemoveRange(usuarios);
                await banco.SaveChangesAsync();

            }
        }
    }
}
