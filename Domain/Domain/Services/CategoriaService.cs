using Domain.Interfaces;
using Domain.Interfaces.Services;
using Entities.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ICategoria _categoriaRepository;
        public CategoriaService(ICategoria categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }
        public async Task AdicionarCategoria(Categoria categoria)
        {
            var valido = categoria.ValidarPropriedadeString(categoria.Nome, "Nome");
            if (valido)
                await _categoriaRepository.Add(categoria);
        }

        public async Task AtualizarCategoria(Categoria categoria)
        {
            var valido = categoria.ValidarPropriedadeString(categoria.Nome, "Nome");
            if (valido)
                await _categoriaRepository.Uppdate(categoria);
        }
    }
}
