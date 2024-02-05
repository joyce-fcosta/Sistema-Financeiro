using Domain.Interfaces;
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
    public class CategoriaRepository : GenericsRepository<Categoria>, ICategoria
    {
        private readonly DbContextOptions<ApplicationContext> _optionsOptions;

        public CategoriaRepository()
        {
            _optionsOptions = new DbContextOptions<ApplicationContext>();
        }

        //Vai listar todas as categorias relacionadas ao determinado usuario
        public async Task<IList<Categoria>> ListarCategoriasUsuarios(string emailUsuario)
        {
            using (var banco = new ApplicationContext(_optionsOptions))
            {
                return await (from s in banco.SistemaFinceiro
                              join c in banco.Categoria on s.Id equals c.IdSistema
                              join us in banco.UsuarioSistemaFinanceiro on s.Id equals us.IdSistema
                              where us.EmailUsuario.Equals(emailUsuario) && us.SistemaAtual
                              select c).AsNoTracking().ToListAsync() ;

            }
        }
    }
}
