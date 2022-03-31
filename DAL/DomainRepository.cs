using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestEF.DB;

namespace TestEF.DAL
{
    class DomainRepository:IDomainRepository
    {
        private AdmContext adm;
        private bool disposed=false;

        public DomainRepository(AdmContext context)
        {
            this.adm = context;
        }
        public IEnumerable<Domain> DomainList()
        {
            var dl = adm.Domains.Include(dom=>dom.Mailboxes).Include(dom=>dom.Aliases).ToList();
            return dl;
        }
        public Domain DomainView(int id)
        {
            var dom = adm.Domains.Include(dom=>dom.Aliases).Include(dom=>dom.Mailboxes).FirstOrDefault(d => d.Id == id);
            return dom;
        }
        public string DomainEdit(Domain dm)
        {
            string ret;
            //var dom = adm.Domains.FirstOrDefault(d => d.Id == id);
            if (dm != null)
            {
                //dom = dm;
                //adm.Update(dom);
                adm.SaveChanges();
                ret = "Успешно";
            }
            else ret = "Не найдено";
            return ret;
        }
        public void DomainAdd(Domain dm)
        {
            adm.Add(dm);
            adm.SaveChanges();
        }
        public string DomainDel(int id)
        {
            var dom = adm.Domains.FirstOrDefault(d => d.Id == id);
            string ret;
            if (dom != null)
            {
                adm.Remove(dom);
                adm.SaveChanges();
                ret = "Успешно";
            }
            else ret = "Не найдено";
            return ret;
        }
        protected virtual void Dispose(bool disposing)
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
