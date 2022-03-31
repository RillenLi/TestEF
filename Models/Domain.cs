using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using TestEF.Models;

namespace TestEF
{
    public class Domain
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Не указано имя домена")]
        public string Name { get; set; }
        public string Description { get; set; }
        public bool DefaultAliases { get; set; } = true;
        public bool BackupMX { get; set; } = false;
        [Range(1,100,ErrorMessage = "MaxAlias не в указанном диапозоне")]
        public int MaxAlias { get; set; }
        [Range(1,100,ErrorMessage = "MaxMailbox не в указанном диапозоне")]
        public int MaxMailbox { get; set; }
        public List<Alias> Aliases { get; set; }
        public List<Mailbox> Mailboxes { get; set; }
    }
}
