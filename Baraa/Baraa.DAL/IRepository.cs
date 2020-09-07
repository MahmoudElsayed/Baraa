using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;

namespace Baraa.DAL
{
    public interface IRepository<T> where T : class
    {
        public Microsoft.EntityFrameworkCore.DbSet<T> DbSet { get; }
        bool Insert(T TEntity);
        bool InsertWithoutSaveChanges(T TEntity);

        bool Update(Guid ID , T TEntity);
        bool Update(T oldTEntity, T newTEntity);
        bool Delete(T TEntity);
        bool DeleteWithoutSaveChanges(T TEntity);
        bool Delete(Guid ID);
        bool Delete(Expression<Func<T , bool>> where);
        bool DeleteAll();
        T GetByID(Guid ID);
        IQueryable<T> GetAll();
        IQueryable<T> Find(Func<T , bool> predicate);
        bool ExecuteSQL(string sqlQuery);
        bool SaveChanges();
        public List<T> ExecuteStoredProcedure(string storedName, params object[] parameters);

        public List<T> ExecuteStoredProcedure(string storedName, SqlParameter[] parameters);
        public List<U> ExecuteStoredProcedure<U>(string storedName, SqlParameter[] parameters = null, CommandType commandType = CommandType.StoredProcedure);
        public int Count();

    }
}
