using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace TestEF.Models
{
    public class Alias
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Не указано имя")]
        public string Name { get; set; }
        [EmailAddress(ErrorMessage ="Неверный email")]
        public string MailToSent { get; set; }
        [Required(ErrorMessage = "Не выбран домен")]
        public int DomainID{get;set;}
        public Domain Domain { get; set; }
        public bool Active { get; set; }
    }
}
