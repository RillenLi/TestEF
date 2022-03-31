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
    class ConsoleMailbox
    {
        private readonly IMailRepository _mailRepository;
        private readonly IDomainRepository _domainRepository;
        public ConsoleMailbox(IMailRepository mailRepository, IDomainRepository domainRepository)
        {
            _mailRepository = mailRepository;
            _domainRepository = domainRepository;
        }

        public void ConMailboxList()
        {
            var ml = _mailRepository.MailList();
            if (ml != null)
            {
                Console.WriteLine($"{"ID",-3} - {"Name",-15} - {"Domain name",-20} - {"Username",-15} - {"Active?"} - {"Welcome mail?"}");
                foreach (Mailbox mm in ml)
                {
                    Console.WriteLine($"{mm.Id,-3} - {mm.Name,-15} - {mm.Domain.Name,-20} - {mm.Username,-15} - {mm.Active,7} - {mm.WelkomeMail}");
                }
                Console.ReadKey();
            }
            else Console.WriteLine("Не найдено!");
        }
        public void ConMailboxView()
        {
            Console.WriteLine("Выберите ID");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                Mailbox mb = _mailRepository.MailView(id);
                if (mb != null)
                {
                    Console.WriteLine($"{"ID",-3} - {"Name",-15} - {"Domain name",-20} - {"Username",-15} - {"Active?"} - {"Welcome mail?"}");
                    Console.WriteLine($"{mb.Id,-3} - {mb.Name,-15} - {mb.Domain.Name,-20} - {mb.Username,-15} - {mb.Active,7} - {mb.WelkomeMail}");
                }
                else Console.WriteLine("Не найдено");
                Console.ReadKey();
            }
            else Console.WriteLine("Не ID");
        }
        public void ConMailboxEdit()
        {
            Console.WriteLine("Выберите ID");
            if (int.TryParse(Console.ReadLine(), out int i))
            {
                Mailbox mlb = _mailRepository.MailView(i);
                if (mlb != null)
                {
                    Console.WriteLine($"Сменить имя ({mlb.Name})?");
                    mlb.Name = Console.ReadLine();
                    Console.WriteLine("Сменить пароль?");
                    string pas = Console.ReadLine();
                    Console.WriteLine("Повторите");
                    if (pas == Console.ReadLine()) mlb.Password = pas; else Console.WriteLine("Неверно");
                    Console.WriteLine($"Домен - {mlb.Domain.Name}");
                    var med = _domainRepository.DomainList();
                    foreach (var d in med)
                    {
                        Console.WriteLine($"{d.Id} - {d.Name}");
                    }
                    if (int.TryParse(Console.ReadLine(), out int did) && med.Any(m => m.Id == did))
                    {
                        mlb.DomainID = did;
                    }
                    else Console.WriteLine("Неверный ID");
                    Console.WriteLine($"Имя пользователя - {mlb.Username}");
                    mlb.Username = Console.ReadLine();
                    var valcontext = new ValidationContext(mlb);
                    var result = new List<ValidationResult>();
                    if (!Validator.TryValidateObject(mlb, valcontext, result, true))
                    {
                        Console.WriteLine("Неверные данные");
                        foreach (var error in result)
                        {
                            Console.WriteLine(error);
                        }
                    }
                    else
                    {
                        string str = _mailRepository.MailEdit(i, mlb);
                        Console.WriteLine(str);
                    }
                }
                else Console.WriteLine("Не найдено");
            }
            else Console.WriteLine("Не ID");
        }
        public void ConMailboxAdd()
        {
            Mailbox m = new Mailbox();
            Console.WriteLine("Введите имя почты");
            m.Username = Console.ReadLine();
            Console.WriteLine("Выберите домен");
            var madd = _domainRepository.DomainList();
            foreach (var d in madd)
            {
                Console.WriteLine($"{d.Id} - {d.Name}");
            }
            if (int.TryParse(Console.ReadLine(), out int did) && madd.Any(m => m.Id == did))
            {
                m.DomainID = did;
            }
            else Console.WriteLine("Неверный ID");
            Console.WriteLine("Введите пароль:");
            string p = Console.ReadLine();
            Console.WriteLine("Повторите пароль:");
            if (p == Console.ReadLine()) m.Password = p;
            Console.WriteLine("Введите имя пользователя:");
            m.Name = Console.ReadLine();
            Console.WriteLine("Приветствие? (1 - да, любое другое нет)");
            if (Console.ReadLine() == "1") m.WelkomeMail = true;
            else m.WelkomeMail = false;
            Console.WriteLine("Активировать? (1-да, остальное нет) ");
            if (int.TryParse(Console.ReadLine(), out int wa) && wa == 1)
            {
                m.Active = true;
            }
            else m.Active = false;
            var valcontext = new ValidationContext(m);
            var result = new List<ValidationResult>();
            if (!Validator.TryValidateObject(m, valcontext, result, true))
            {
                Console.WriteLine("Неверные данные");
                foreach (var error in result)
                {
                    Console.WriteLine(error);
                }
            }
            else
            {
                _mailRepository.MailAdd(m);
            }
        }
        public void ConMailboxDel()
        {
            Console.WriteLine("Введите ID");
            if (int.TryParse(Console.ReadLine(), out int iden))
            {
                Console.WriteLine(_mailRepository.MailDel(iden));
            }
            else Console.WriteLine("Не ID");
        }
    }
}
