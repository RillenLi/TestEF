using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestEF.Models;

namespace TestEF.DAL
{
    interface IAliasRepository:IDisposable
    {
        IEnumerable<Alias> AliasList();
        Alias AliasView(int id);
        string AliasEdit(int id, Alias al);
        void AliasAdd(Alias al);
        string AliasDel(int id);
    }
}
