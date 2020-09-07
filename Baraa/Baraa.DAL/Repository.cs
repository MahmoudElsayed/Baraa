using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace Baraa.DAL
{
    public class Repository<T> : IRepository<T> where T : class
    {
        //   static Logger _Logger = new Logger(typeof(Repository<T>));
        DbSet<T> dbSet;
        AppContext context;
        IUnitOfWork _uow;
        private IConfiguration Configuration;

        DbSet<T> IRepository<T>.DbSet => dbSet;

        public Repository(IUnitOfWork UOW , IConfiguration configuration)

        {
            try
            {
                //  context = appContext;
                _uow = UOW;
                context = _uow.GetContext;
                //context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
                Configuration = configuration;
                dbSet = context.Set<T>();
            }
            catch (Exception ex)
            {
                //  _Logger.Error(ex.Message, ex);
            }
        }
        public bool Insert(T TEntity)
        {
            try
            {
                dbSet.Add(TEntity);

                return _uow.SaveChanges();
            }
            catch //(Exception ex)
            {
                //_Logger.Error(ex.Message, ex);
                return false;
            }
        }
        public bool InsertWithoutSaveChanges(T TEntity)
        {
            try
            {
                dbSet.Add(TEntity);
                return true;
            }
            catch //(Exception ex)
            {
                //_Logger.Error(ex.Message, ex);
                return false;
            }
        }

        public bool Update(Guid ID , T TEntity)
        {
            try
            {
                T old = GetByID(ID);
                context.Entry<T>(TEntity).CurrentValues.SetValues(TEntity);
                context.Entry<T>(TEntity).State = EntityState.Modified;
                return _uow.SaveChanges();
            }
            catch //(Exception ex)
            {
                // _Logger.Error(ex.Message, ex);
                return false;
            }
        }
        public bool Update(T oldEntity , T newEntity)
        {
            try
            {
                context.Entry<T>(oldEntity).State = EntityState.Modified;
                context.Entry<T>(oldEntity).CurrentValues.SetValues(newEntity);
            }
            catch(Exception ex) { }
            return true;
        }

        public bool SaveChanges()
        {
            return _uow.SaveChanges();
        }

        public bool Delete(T TEntity)
        {
            try
            {
                dbSet.Remove(TEntity);

                return _uow.SaveChanges();
            }
            catch ////(Exception ex)
            {
                //_Logger.Error(ex.Message, ex);
                return false;
            }
        }
        public bool DeleteWithoutSaveChanges(T TEntity)
        {
            try
            {
                dbSet.Remove(TEntity);
                return true;
            }
            catch ////(Exception ex)
            {
                //_Logger.Error(ex.Message, ex);
                return false;
            }
        }
        public bool Delete(Expression<Func<T , bool>> where)
        {
            try
            {
                var objects = context.Set<T>().Where(where).ToList();
                foreach (var item in objects)
                {
                    context.Set<T>().Remove(item);
                }

                return true;
            }
            catch ////(Exception ex)
            {
                //_Logger.Error(ex.Message, ex);
                return false;
            }
        }
        public bool DeleteAll()
        {
            try
            {
                var objects = context.Set<T>().AsEnumerable();
                foreach (var item in objects)
                {
                    context.Set<T>().Remove(item);
                }

                return true;
            }
            catch ////(Exception ex)
            {
                //_Logger.Error(ex.Message, ex);
                return false;
            }
        }

        public bool Delete(Guid ID)
        {
            try
            {
                T entity = GetByID(ID);
                return Delete(entity);
            }
            catch //(Exception ex)
            {
                // _Logger.Error(ex.Message, ex);
                return false;
            }
        }

        public T GetByID(Guid ID)
        {
            try
            {
                return (T)dbSet.Find(ID);
            }
            catch //(Exception ex)
            {
                // _Logger.Error(ex.Message, ex);
                return null;
            }
        }

        public IQueryable<T> GetAll()
        {
            try
            {
                return dbSet.AsQueryable();
            }
            catch //(Exception ex)
            {
                // _Logger.Error(ex.Message, ex);
                return null;
            }
        }


        public IQueryable<T> Find(Func<T , bool> predicate)
        {
            try
            {
                return dbSet.Where(predicate).AsQueryable<T>();
            }
            catch (Exception ex)
            {
                // _Logger.Error(ex.Message, ex);
                return null;
            }
        }
        public bool ExecuteSQL(string sqlQuery)
        {
            try
            {
              context.Database.ExecuteSqlCommand(sqlQuery);
                _uow.SaveChanges();
                return true;
            }
            catch //(Exception ex)
            {
                //_Logger.Error(ex.Message, ex);
                return false;
            }
        }

        [Obsolete]
        public List<T> ExecuteStoredProcedure(string storedName, SqlParameter[] parameters)
        {
            string ConnectionString = Microsoft
   .Extensions
   .Configuration
   .ConfigurationExtensions
   .GetConnectionString(Configuration, "DefaultConnection");

            SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand(storedName, sqlConnection);
            cmd.Parameters.AddRange(parameters);

            sqlConnection.Open();
            cmd.CommandType = CommandType.StoredProcedure;
            var reader = cmd.ExecuteReader();

            DataTable tbl = new DataTable();
            tbl.Load(reader);
            sqlConnection.Close();
            return ConvertDataTable<T>(tbl);


        }
        public List<U> ExecuteStoredProcedure<U>(string storedName, SqlParameter[] parameters = null, CommandType commandType = CommandType.StoredProcedure)
        {
            string ConnectionString = Microsoft
                                       .Extensions
                                       .Configuration
                                       .ConfigurationExtensions
                                       .GetConnectionString(Configuration, "DefaultConnection");

            SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand(storedName, sqlConnection);
            if (parameters != null)
                cmd.Parameters.AddRange(parameters);

            sqlConnection.Open();
            cmd.CommandType = commandType;
            var reader = cmd.ExecuteReader();

            DataTable tbl = new DataTable();
            tbl.Load(reader, LoadOption.PreserveChanges);
            sqlConnection.Close();
            return ConvertDataTable<U>(tbl);


        }
        private static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows) data.Add(GetItem<T>(row));
            return data;
        }
        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                PropertyInfo pro = temp.GetProperty(column.ColumnName);
                if (pro != null && pro.CanWrite)
                {
                    try
                    {
                        object col = dr[column.ColumnName];
                        if (pro.PropertyType.Name == "Boolean" && (col.ToString() == "0" || col.ToString() == "1")) col = !(col.ToString() == "0");
                        pro.SetValue(obj , col , null);
                    }
                    catch
                    {
                        pro.SetValue(obj , null , null);
                    }
                }
            }
            return obj;
        }
        public List<T> ExecuteStoredProcedure(string storedName, params object[] parameters)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            return context.Query<T>().FromSqlRaw("EXEC " + storedName, parameters).ToList();
        }
        public int Count()
        {
            return dbSet.Count();
        }

     
    }
}
