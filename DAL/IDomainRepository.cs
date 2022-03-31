using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestEF.Models;

namespace TestEF.DAL
{
    interface IDomainRepository:IDisposable  
    {
        IEnumerable<Domain> DomainList();
        Domain DomainView(int id);
        string DomainEdit(Domain dom);
        void DomainAdd(Domain dom);
        string DomainDel(int id);
    }
}
