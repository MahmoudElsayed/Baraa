using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baraa.DAL.Contract
{
    public interface IUnitOfWork : IDisposable
    {
        BaraaContext GetContext { get; }
        bool Commit();
        bool SaveChanges();
    }
}
