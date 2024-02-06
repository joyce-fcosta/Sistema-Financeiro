
using Domain.Interfaces;
using Domain.Interfaces.Services;
using Entities;
using Entities.Entidades;

namespace Domain.Services
{
    public class UsuarioSistemaFinanceiroService : IUsuarioSistemaFinanceiroService
    {
        private readonly IUsuarioSistemaFinanceiro _ususarioSistemaFinanceiroRepository;

        public UsuarioSistemaFinanceiroService(IUsuarioSistemaFinanceiro ususarioSistemaFinanceiroRepository)
        {
            _ususarioSistemaFinanceiroRepository = ususarioSistemaFinanceiroRepository;
        }
                  
        public async Task CadastrarUsuarioNoSistema(UsuarioSistemaFinanceiro usuarioSistemaFinanceiro)
        {
            await _ususarioSistemaFinanceiroRepository.Add(usuarioSistemaFinanceiro);
        }
    }
}
