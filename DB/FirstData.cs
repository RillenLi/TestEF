using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestEF.Models;

namespace TestEF.DB
{
    public static class FirstData
    {
        public static void Init(AdmContext context)
        {
            if (!context.Domains.Any())
            {
                Domain dm = new Domain
                {
                    Name = "example.com",
                    Description = "Example domain",
                    DefaultAliases = false,
                    BackupMX = true,
                    MaxAlias = 5,
                    MaxMailbox = 10,
                    Aliases = new List<Alias>(),
                    Mailboxes = new List<Mailbox>()
                };
                context.Add(dm);
                context.SaveChanges();
            }
            if (!context.Aliases.Any())
            {
                Alias al= new Alias
                {
                    Name = "something",
                    MailToSent = "sometthing@yandex.ru",
                    DomainID=1,
                    Active=true
                };
                context.Add(al);
                context.SaveChanges();
            }
            if (!context.Mailboxes.Any())
            {
                Mailbox ml = new Mailbox
                {
                    Username = "user",
                    Password = "123",
                    Name = "Any User",
                    Active = true,
                    DomainID = 1,
                    WelkomeMail=false
                };
                context.Add(ml);
                context.SaveChanges();
            }
        }
    }
}
