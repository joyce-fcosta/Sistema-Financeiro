using Entities.Notification;
using System.ComponentModel.DataAnnotations;

namespace Entities.Entidades
{
    public class Base : Notifica
    {
        [Display(Name = "Código")]
        public int Id { get; set; }

        [Display(Name = "Nome")]
        public string Nome { get; set; }
    }
}
