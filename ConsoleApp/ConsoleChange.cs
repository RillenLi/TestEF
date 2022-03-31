using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestEF.DAL;
using TestEF.DB;
namespace TestEF.ConsoleApp
{
    class ConsoleChange
    {
        public void DbChange(string ed)
        {
            switch (ed)
            {
                case "1":
                    Console.WriteLine();
                    Console.WriteLine("База - Domains");
                    Menu();
                    DomainChange();
                    break;
                case "2":
                    Console.WriteLine();
                    Console.WriteLine("База - Aliases");
                    Menu();
                    AliasChange();
                    break;
                case "3":
                    Console.WriteLine();
                    Console.WriteLine("База - Mailboxes");
                    Menu();
                    MailChange();
                    break;
                case "0":
                    break;
                default:
                    Console.WriteLine("Выберите базу");
                    break;
            }
        }
        public void DomainChange()
        {
            string change;
            using (var context = new AdmContext())
            {
                IDomainRepository domainRepository = new DomainRepository(context);
                var dConsole = new ConsoleDomain(domainRepository);
                change = Console.ReadLine();
                switch (change)
                {
                    case "1":
                        dConsole.ConDomainList();
                        break;
                    case "2":
                        dConsole.ConDomainView();
                        break;
                    case "3":
                        dConsole.ConDomainEdit();
                        break;
                    case "4":
                        dConsole.ConDomainAdd();
                        break;
                    case "5":
                        dConsole.ConDomainDel();
                        break;
                    default:
                        Console.WriteLine("Выберите действие");
                        break;
                }
            }
        }
        public void AliasChange()
        {
            string change;
            using (var context = new AdmContext())
            {
                IAliasRepository aliasRepository = new AliasRepository(context);
                IDomainRepository domainRepository = new DomainRepository(context);
                var aConsole = new ConsoleAlias(aliasRepository, domainRepository);
                change = Console.ReadLine();
                switch (change)
                {
                    case "1":
                        aConsole.ConAliasList();
                        break;
                    case "2":
                        aConsole.ConAliasView();
                        break;
                    case "3":
                        aConsole.ConAliasEdit();
                        break;
                    case "4":
                        aConsole.ConAliasAdd();
                        break;
                    case "5":
                        aConsole.ConAliasDel();
                        break;
                    default:
                        Console.WriteLine("Выберите действие!");
                        break;
                }
            }
        }
        public void MailChange()
        {
            string change;
            using (var context = new AdmContext())
            {
                IMailRepository mailRepository = new MailRepository(context);
                IDomainRepository domainRepository = new DomainRepository(context);
                var mConsole = new ConsoleMailbox(mailRepository, domainRepository);
                change = Console.ReadLine();
                {
                    switch (change)
                    {
                        case "1":
                            mConsole.ConMailboxList();
                            break;
                        case "2":
                            mConsole.ConMailboxView();
                            break;
                        case "3":
                            mConsole.ConMailboxEdit();
                            break;
                        case "4":
                            mConsole.ConMailboxAdd();
                            break;
                        case "5":
                            mConsole.ConMailboxDel();
                            break;
                        default:
                            Console.WriteLine("Выберите действие");
                            break;
                    }
                }
            }
        }
        public void Menu()
        {
            Console.WriteLine("Выберите действие:");
            Console.WriteLine("1 - Листинг");
            Console.WriteLine("2 - Просмотр по ID");
            Console.WriteLine("3 - Редактировать по ID");
            Console.WriteLine("4 - Добавить");
            Console.WriteLine("5 - Удалить");
            Console.WriteLine("0 - Назад");
        }
    }
}
