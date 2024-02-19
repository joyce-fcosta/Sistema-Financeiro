using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Notification
{
    public class Notifica
    {
        public Notifica()
        {
            Notificacoes = new List<Notifica>();
        }

        [NotMapped]
        public string NomePropriedade { get; set; }
		[NotMapped]
		public string Mensagem { get; set; }
		[NotMapped]
		public List<Notifica> Notificacoes { get; set; }


        public bool ValidarPropriedadeString(string valor, string nomePropriedade)
        {
            if (string.IsNullOrWhiteSpace(nomePropriedade) || string.IsNullOrWhiteSpace(valor))
            {
                Notificacoes.Add(new Notifica
                {
                    Mensagem = "Campo Obrigatório",
                    NomePropriedade = nomePropriedade
                });
                return false;
            }
            return true;
        }

        public bool ValidarPropriedadeInmt(int valor, string nomePropriedade)
        {
            if (string.IsNullOrWhiteSpace(nomePropriedade) || valor <  1)
            {
                Notificacoes.Add(new Notifica
                {
                    Mensagem = "Campo Obrigatório",
                    NomePropriedade = nomePropriedade
                });
                return false;
            }
            return true;
        }
    }
}
