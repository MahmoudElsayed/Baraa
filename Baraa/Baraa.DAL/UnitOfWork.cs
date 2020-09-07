using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baraa.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private AppContext context;
        private bool isCaching;
        //Logger _Logger = new Logger(typeof(UnitOfWork));
        public UnitOfWork(AppContext appContext , bool isCaching = false)
        {
            this.isCaching = isCaching;
            context = appContext;
        }
        public AppContext GetContext
        {
            get
            {
                return context;
            }
        }
        public bool SaveChanges()
        {
            bool SaveDone = true;
            try
            {
                //if (isCaching)
                //{
                SaveDone = context.SaveChanges() > 0 ? true : false;
                // }
                return SaveDone;
            }
            catch (Exception ex)
            {
                //   _Logger.Error(ex.Message, ex);
                Dispose();
                return false;
            }
        }

        public bool Commit()
        {
            try
            {
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                //   _Logger.Error(ex.Message, ex);
                Dispose();
                return false;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {

            if (disposing)
            {
                this.context.Dispose();
                // free managed resources
            }

        }
    }
}
