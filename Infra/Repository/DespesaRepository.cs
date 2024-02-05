using Domain.Interfaces;
using Domain.Interfaces.Generics;
using Entities.Entidades;
using Infra.Configuration;
using Infra.Repository.Generics;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repository
{
    public class DespesaRepository : GenericsRepository<Despesa>, IDespesa
    {
        private readonly DbContextOptions<ApplicationContext> _optionsOptions;

        public DespesaRepository()
        {
            _optionsOptions = new DbContextOptions<ApplicationContext>();
        }
        public async Task<IList<Despesa>> ListarDespesasNaoPagasMesesAnteriores(string emailUsuario)
        {
            using (var banco = new ApplicationContext(_optionsOptions))
            {
                return await (from s in banco.SistemaFinceiro
                              join c in banco.Categoria on s.Id equals c.IdSistema
                              join us in banco.UsuarioSistemaFinanceiro on s.Id equals us.IdSistema
                              join d in banco.Despesas on c.Id equals d.IdCategoria
                              where us.EmailUsuario.Equals(emailUsuario) && s.Mes < DateTime.Now.Month && !d.Pago
                              select d).AsNoTracking().ToListAsync();

            }
        }

        public async Task<IList<Despesa>> ListarDespesaUsuario(string emailUsuario)
        {

            using (var banco = new ApplicationContext(_optionsOptions))
            {
                return await(from s in banco.SistemaFinceiro
                             join c in banco.Categoria on s.Id equals c.IdSistema
                             join us in banco.UsuarioSistemaFinanceiro on s.Id equals us.IdSistema
                             join d in banco.Despesas on c.Id equals d.IdCategoria
                             where us.EmailUsuario.Equals(emailUsuario) && s.Mes == d.Mes && s.Ano == d.Ano
                             select d).AsNoTracking().ToListAsync();
            }
        }
    }
}
