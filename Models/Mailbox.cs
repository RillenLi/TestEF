using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace TestEF.Models
{
    public class Mailbox
    {
        public int Id { get; set; }
        [Required (ErrorMessage ="Не указано имя")]
        public string Username { get; set; }
        [Required(ErrorMessage ="Не выбран домен")]
        public int DomainID { get; set; }
        public Domain Domain { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }

        public bool Active { get; set; }
        public bool WelkomeMail { get; set; }

    }
}
