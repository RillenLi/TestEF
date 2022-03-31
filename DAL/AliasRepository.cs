using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestEF.DB;
using Microsoft.EntityFrameworkCore;
using TestEF.Models;
namespace TestEF.DAL
{
    class AliasRepository:IAliasRepository
    {
        private AdmContext adm;
        private bool disposed = false;
        public IEnumerable<Alias> AliasList()
        {
            var al = adm.Aliases.Include(a => a.Domain).ToList();
            return al;
        }
        public Alias AliasView(int id)
        {
            var av = adm.Aliases.Include(a=>a.Domain).FirstOrDefault(a => a.Id == id);
            return av;
        }
        public string AliasEdit(int id, Alias alias)
        {
            string ret;
            var al = adm.Aliases.FirstOrDefault(a => a.Id == id);
            if (al != null)
            {
                al = alias;
                adm.Aliases.Update(al);
                adm.SaveChanges();
                ret = "Успешно";
            }
            else ret = "Не найдено";
            return ret;
        }
        public void AliasAdd(Alias al)
        {
            adm.Add(al);
            adm.SaveChanges();
        }
        public string AliasDel(int id)
        {
            string ret;
            var al = adm.Aliases.FirstOrDefault(a => a.Id == id);
            if (al != null)
            {
                adm.Aliases.Remove(al);
                adm.SaveChanges();
                ret = "Успешно";
            }
            else ret = "Не найдено";
            return ret;
        }
        public AliasRepository(AdmContext context)
        {
            this.adm = context;
        }
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    adm.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
