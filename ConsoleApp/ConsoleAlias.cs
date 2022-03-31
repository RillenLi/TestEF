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
    class ConsoleAlias
    {
        private readonly IAliasRepository _aliasRepository;
        private readonly IDomainRepository _domainRepository;
        public ConsoleAlias(IAliasRepository aliasRepository, IDomainRepository domainRepository)
        {
            _aliasRepository = aliasRepository;
            _domainRepository = domainRepository;
        }
        public void ConAliasList()
        {
            var allist = _aliasRepository.AliasList();
            if (allist != null)
            {
                Console.WriteLine($"{"ID",-3} - {"Name",-15} - {"Domain name",-20} - {"Mail to sent",-20} - Active?");
                foreach (Alias ali in allist)
                {
                    Console.WriteLine($"{ali.Id,-3} - {ali.Name,-15} - {ali.Domain.Name,-20} - {ali.MailToSent,-20} - {ali.Active}");
                }
            }
            else Console.WriteLine("Нет данных");
        }
        public void ConAliasView()
        {
            Console.WriteLine("Введите ID");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var alvw = _aliasRepository.AliasView(id);
                if (alvw != null)
                {
                    Console.WriteLine($"{"ID",-3} - {"Name",-15} - {"Domain name",-20} - {"Mail to sent",-20} - Active?");
                    Console.WriteLine($"{alvw.Id,-3} - {alvw.Name,-15} - {alvw.Domain.Name,-20} - {alvw.MailToSent,-20} - {alvw.Active}");
                }
                else Console.WriteLine("Не найдено");
            }
            else Console.WriteLine("Не ID");
        }
        public void ConAliasEdit()
        {
            Console.WriteLine("Введите ID");
            if (int.TryParse(Console.ReadLine(), out int ided))
            {
                Alias aledit = _aliasRepository.AliasView(ided);
                if (aledit != null)
                {
                    Console.WriteLine($"Текущее имя: {aledit.Name}");
                    aledit.Name = Console.ReadLine();
                    Console.WriteLine($"Текущий домен: {aledit.Domain.Name}");
                    var dom = _domainRepository.DomainList();
                    foreach (var d in dom)
                    {
                        Console.WriteLine($"{d.Id} - {d.Name}");
                    }
                    if (int.TryParse(Console.ReadLine(), out int did) && dom.Any(d => d.Id == did)) aledit.DomainID = did;
                    else Console.WriteLine("Неверный ID");
                    Console.WriteLine($"Почта: {aledit.MailToSent}");
                    aledit.MailToSent = Console.ReadLine();
                    Console.WriteLine($"Активна - {aledit.Active}");
                    string a = Console.ReadLine();
                    if (a == "1") aledit.Active = true;
                    else aledit.Active = false;
                    var valcontext = new ValidationContext(aledit);
                    var results = new List<ValidationResult>();
                    if (!Validator.TryValidateObject(aledit, valcontext, results, true))
                    {
                        Console.WriteLine("Неверные данные");
                        foreach (var error in results)
                        {
                            Console.WriteLine(error);
                        }
                    }
                    else
                    {
                        string alstr = _aliasRepository.AliasEdit(ided, aledit);
                        Console.WriteLine(alstr);
                    }
                }
                else Console.WriteLine("Не найдено");
            }
            else Console.WriteLine("Не ID");
        }
        public void ConAliasAdd()
        {
            Console.WriteLine("Имя alias?");
            Alias al = new Alias();
            al.Name = Console.ReadLine();
            Console.WriteLine("Выберите домен?");
            var ald = _domainRepository.DomainList();
            foreach (Domain adom in ald)
            {
                Console.WriteLine($"{adom.Id} - {adom.Name}");
            }
            if (int.TryParse(Console.ReadLine(), out int alid) && ald.Any(a => a.Id == alid)) al.DomainID = alid;
            else Console.WriteLine("Неверный ID");
            Console.WriteLine("Введите email");
            string m = Console.ReadLine();
            if (m != null) al.MailToSent = m;
            Console.WriteLine("Активировать? (1-да, остальное нет) ");
            string aal = Console.ReadLine();
            if (aal == "1") al.Active = true;
            else al.Active = false;
            var valcontext = new ValidationContext(al);
            var result = new List<ValidationResult>();
            if (!Validator.TryValidateObject(al, valcontext, result, true))
            {
                Console.WriteLine("Неверные данные");
                foreach (var error in result)
                {
                    Console.WriteLine(error);
                }
            }
            else _aliasRepository.AliasAdd(al);
        }
        public void ConAliasDel()
        {
            Console.WriteLine("Выберите ID");
            if (int.TryParse(Console.ReadLine(), out int iddel))
            {
                string del = _aliasRepository.AliasDel(iddel);
                Console.WriteLine(del);
            }
            else Console.WriteLine("Не ID");
        }
    }
}
