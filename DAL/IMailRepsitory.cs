using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestEF.Models;

namespace TestEF.DAL
{
    interface IMailRepository:IDisposable
    {
        IEnumerable<Mailbox> MailList();
        Mailbox MailView(int id);
        string MailEdit(int id,Mailbox mb);
        void MailAdd(Mailbox mb);
        string MailDel(int id);
    }
}
