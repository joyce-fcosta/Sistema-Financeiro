using Domain.Interfaces;
using Entities.Entidades;
using Infra.Configuration;
using Infra.Repository.Generics;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repository
{
    public class SistemaFinanceiroRepository : GenericsRepository<SistemaFinanceiro>, ISistemaFinanceiro
    {
        private readonly DbContextOptions<ApplicationContext> _optionsOptions;
        public SistemaFinanceiroRepository()
        {
            _optionsOptions = new DbContextOptions<ApplicationContext>();
        }

        public async Task<IList<SistemaFinanceiro>> ListarSistemasUsuario(string emailUsuario)
        {
            using (var banco = new ApplicationContext(_optionsOptions))
            {
                return await (from s in banco.SistemaFinceiro
                              join us in banco.UsuarioSistemaFinanceiro on s.Id equals us.IdSistema
                              where us.EmailUsuario.Equals(emailUsuario)
                              select s).AsNoTracking().ToListAsync();
            }
        }
    }
}
