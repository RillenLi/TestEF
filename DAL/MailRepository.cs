using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestEF.DAL;
using TestEF.DB;
using Microsoft.EntityFrameworkCore;
using TestEF.Models;

namespace TestEF.DAL
{
    class MailRepository:IMailRepository
    {
        private AdmContext adm;
        public bool disposed;
        public MailRepository(AdmContext context)
        {
            this.adm = context;
        }
        public IEnumerable<Mailbox> MailList()
        {
            var mb = adm.Mailboxes.Include(m=>m.Domain).ToList();
            return mb;
        }
        public Mailbox MailView(int id)
        {
            Mailbox mb = adm.Mailboxes.Include(m=>m.Domain).FirstOrDefault(m => m.Id == id);
            return mb;
        }
        public string MailEdit(int id, Mailbox mb)
        {
            string ret;
            var mlb = adm.Mailboxes.FirstOrDefault(m => m.Id == id);
            if (mlb != null)
            {
                mlb = mb;
                adm.Mailboxes.Update(mlb);
                adm.SaveChanges();
                ret = "Успешно";
            }
            else ret = "Не найдено";
            return ret;
        }
        public void MailAdd(Mailbox mb)
        {
            adm.Mailboxes.Add(mb);
            adm.SaveChanges();
        }
        public string MailDel(int id)
        {
            string ret;
            var mlb = adm.Mailboxes.FirstOrDefault(m => m.Id == id);
            if (mlb != null)
            {
                adm.Mailboxes.Remove(mlb);
                adm.SaveChanges();
                ret = "Успешно";
            }
            else ret = "Не найдено";
            return ret;
        }
        public virtual void Dispose(bool disposing)
        {
            if (this.disposed)
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
