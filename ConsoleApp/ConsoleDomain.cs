using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using TestEF.DAL;
using TestEF.Models;

namespace TestEF.ConsoleApp
{
    class ConsoleDomain
    {
        private readonly IDomainRepository _domainRepository;

        public ConsoleDomain(IDomainRepository domainRepository)
        {
            _domainRepository = domainRepository;
        }
        public void ConDomainList()
        {
            var dl = _domainRepository.DomainList();
            if (dl != null)
            {
                Console.WriteLine($"{"ID",-3} - {"Name ",-20} - {"Description",-20} - {"DefAlias"} - {"BackupMX"} - {"Aliases",7} - {"Mailboxes",9}");
                foreach (Domain d in dl)
                {
                    int alias;
                    if (d.Aliases == null) alias = 0;
                    else alias = d.Aliases.Count(a => a.DomainID == d.Id);
                    int mail;
                    if (d.Mailboxes == null) mail = 0;
                    else mail = d.Mailboxes.Count(m => m.DomainID == d.Id);
                    Console.WriteLine($"{d.Id,-3} - {d.Name,-20} - {d.Description,-20} - {d.DefaultAliases,-8} - {d.BackupMX,-8} - {alias,3}/{d.MaxAlias,-3} - {mail,4}/{ d.MaxMailbox,-4}");
                }
                Console.ReadKey();
            }
            else Console.WriteLine("Нет данных");
        }
        public void ConDomainView()
        {
            Console.WriteLine("Введите ID");
            if (int.TryParse(Console.ReadLine(), out int idvw))
            {
                Domain dom = _domainRepository.DomainView(idvw);
                if (dom != null)
                {
                    Console.WriteLine($"{"ID",-3} - {"Name ",-20} - {"Description",-20} - {"DefAlias"} - {"BackupMX"}");
                    Console.WriteLine($"{dom.Id,-3} - {dom.Name,-20} - {dom.Description,-20} - {dom.DefaultAliases} - {dom.BackupMX}");
                    Console.WriteLine("Aliases: ");
                    if (dom.Aliases != null)
                    {
                        var al = dom.Aliases.ToList();
                        foreach (Alias a in al)
                        {
                            Console.WriteLine(a.Name);
                        }
                    }
                    Console.WriteLine("Mailboxes: ");
                    if (dom.Mailboxes != null)
                    {
                        var ml = dom.Mailboxes.ToList();
                        foreach (Mailbox m in ml)
                        {
                            Console.WriteLine(m.Username);
                        }
                    }
                    Console.ReadKey();
                }
                else Console.WriteLine("Не найдено");
            }
            else Console.WriteLine("Не ID");
        }
        public void ConDomainEdit()
        {
            Console.WriteLine("Введите ID");
            if (int.TryParse(Console.ReadLine(), out int ided))
            {
                Domain dedit = _domainRepository.DomainView(ided);
                if (dedit != null)
                {
                    Console.WriteLine($"Текущее имя: {dedit.Name}");
                    dedit.Name = Console.ReadLine();
                    Console.WriteLine($"Текущее описание: {dedit.Description}");
                    dedit.Description = Console.ReadLine();
                    Console.WriteLine($"MaxAliases: {dedit.MaxAlias}");
                    if (int.TryParse(Console.ReadLine(), out int ma))
                    {
                        dedit.MaxAlias = ma;
                    }
                    Console.WriteLine($"MaxMailboxes: {dedit.MaxMailbox}");
                    if (int.TryParse(Console.ReadLine(), out int mm))
                    {
                        dedit.MaxMailbox = mm;
                    }
                    Console.WriteLine($"DefaultAlias: {dedit.DefaultAliases} (1 - да, всё остальное нет)");
                    string defal = Console.ReadLine();
                    if (defal == "1") dedit.DefaultAliases = true;
                    else dedit.DefaultAliases = false;
                    Console.WriteLine($"Backup MX: {dedit.BackupMX} (1 - да, остальное нет)");
                    string back = Console.ReadLine();
                    if (back == "1") dedit.BackupMX = true;
                    else dedit.BackupMX = false;
                    var results = new List<ValidationResult>();
                    var valcontext = new ValidationContext(dedit);
                    if (!Validator.TryValidateObject(dedit, valcontext, results, true))
                    {
                        Console.WriteLine("Неверные данные");
                        foreach (var error in results)
                        {
                            Console.WriteLine(error.ErrorMessage);
                        }
                    }
                    else
                    {
                        string s = _domainRepository.DomainEdit(dedit);
                        Console.WriteLine(s);
                    }
                }
                else Console.WriteLine("Не найдено");
            }
            else Console.WriteLine("Не ID");
        }
        public void ConDomainAdd()
        {
            Domain domadd = new Domain();
            Console.WriteLine("Введите название домена");
            domadd.Name = Console.ReadLine();
            Console.WriteLine("Введите описание");
            domadd.Description = Console.ReadLine();
            Console.WriteLine("Введите количество alias(не больше 100)");
            if (int.TryParse(Console.ReadLine(), out int maxal))
            {
                domadd.MaxAlias = maxal;
            }
            else domadd.MaxAlias = 0;
            Console.WriteLine("Введите количество почтовых ящиков(не больше 100)");
            if (int.TryParse(Console.ReadLine(), out int maxbox))
            {
                domadd.MaxMailbox = maxbox;
            }
            else domadd.MaxMailbox = 0;
            Console.WriteLine("Выберите включены ли alias по умолчанию? (1 - да, любое другое значение нет)");
            string defa = Console.ReadLine();
            if (defa == "1") domadd.DefaultAliases = true;
            else domadd.DefaultAliases = false;
            Console.WriteLine("Использовать сервер как резервный? (1-да, любое другое нет)");
            string mx = Console.ReadLine();
            if (mx == "1") domadd.BackupMX = true;
            else domadd.BackupMX = false;
            var results = new List<ValidationResult>();
            var valcontext = new ValidationContext(domadd);
            if (!Validator.TryValidateObject(domadd, valcontext, results, true))
            {
                Console.WriteLine("Неверные данные");
                foreach (var error in results)
                {
                    Console.WriteLine(error.ErrorMessage);
                }
            }
            else
            {
                _domainRepository.DomainAdd(domadd);
                Console.WriteLine("Успешно");
            }
        }
        public void ConDomainDel()
        {
            Console.WriteLine("Введите ID");
            if (int.TryParse(Console.ReadLine(), out int iddel))
            {
                Console.WriteLine(_domainRepository.DomainDel(iddel));
            }
            else Console.WriteLine("Не ID");
        }
    }
}
