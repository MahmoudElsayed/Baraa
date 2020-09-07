using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baraa.DAL
{
    public interface IUnitOfWork : IDisposable
    {
        AppContext GetContext { get; }
        bool Commit();
        bool SaveChanges();
    }
}
