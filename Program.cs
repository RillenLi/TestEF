using System;
using System.Linq;
using TestEF.DAL;
using TestEF.DB;
using TestEF.Models;
using TestEF.ConsoleApp;

namespace TestEF
{
    class Program
    {
        static void Main(string[] args)
        {
            using (AdmContext db = new AdmContext())
            {
                FirstData.Init(db);
            }
                  
            string entData = "5";
            do
            {
                Console.WriteLine("Выберите базу данных для работы с ней:");
                Console.WriteLine("1 - Domains");
                Console.WriteLine("2 - Aliases");
                Console.WriteLine("3 - Mailboxes");
                Console.WriteLine("0 - Выход из программы");
                entData = Console.ReadLine();
                var cCgange = new ConsoleChange();
                if (entData != "0") cCgange.DbChange(entData);
                Console.WriteLine();
            }
            while (entData != "0");
        }
    }
}

