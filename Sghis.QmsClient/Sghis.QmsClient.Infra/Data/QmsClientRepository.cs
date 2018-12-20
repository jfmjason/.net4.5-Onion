using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using Sghis.Core.Entity.Base;

namespace Sghis.QmsClient.Infra.Data
{
    public class QmsClientRepository<T> : IRepository<T> where T : class
    {
        private QmsClientDbContext context;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">Your DbContext, that will be injected to this Repository object</param>
        public QmsClientRepository(QmsClientDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Add Entity
        /// </summary>
        /// <param name="entity">Your Entity to be added e.g. Person</param>
        /// <returns name="entity">The added Entity with Id set from database.</param>
        public T Add(T entity)
        {
            return Entity.Add(entity);
        }

        /// <summary>
        /// Update Entity
        /// </summary>
        /// <param name="entity">Your Entity to be updated e.g. Person</param>
        /// <returns name="void"></param>
        public void Update(T entity)
        {
            if (entity == null)
            {
                throw new Exception("Not exists!");
            }
        }

        public void UpdateForce(T entity)
        {
            if (entity == null)
            {
                throw new Exception("Not exists!");
            }
            context.Entry(entity).State = EntityState.Modified;
        }

        /// <summary>
        /// Delete Entity
        /// </summary>
        /// <param name="entity">Your Entity to be deleted e.g. Person</param>
        /// <returns name="void"></param>
        public void Delete(T entity)
        {
            Entity.Remove(entity);
        }

        /// <summary>
        /// Get Entity By Id
        /// </summary>
        /// <param name="id">your entity id</param>
        /// <returns name="Entity">Entity</param>
        public T GetById(object id)
        {
            return Entity.Find(id);
        }
        /// <summary>
        /// Get Entity by specified criteria
        /// </summary>
        /// <param name="criteria">Lamda expression</param>
        /// <returns name="Entity"></param>
        public T GetByCriteria(Func<T, bool> criteria)
        {
            return Entity.Where(criteria).SingleOrDefault();
        }

        /// <summary>
        /// Get All Entity as IQueriable
        /// </summary>        
        /// <returns name="IQueryable"></param>
        public IQueryable<T> GetAll()
        {
            return Entity.AsQueryable<T>();
        }

        /// <summary>
        /// Get All Entity as IQueriable by specified criteria
        /// </summary>        
        /// <param name="criteria">Lamda expression</param>
        /// <returns name="IQueryable"></param>
        public IQueryable<T> GetAllByCriteria(Func<T, bool> criteria)
        {
            return Entity.Where(criteria).AsQueryable<T>();
        }

        /// <summary>
        /// Entity, provides and access to you Entity.
        /// </summary>        
        /// <returns name="Entity">you entity</param>
        public IDbSet<T> Entity
        {
            get { return context.Set<T>(); }
        }

        /// <summary>
        /// Commit: to flush changes in the current context to your database.
        /// </summary>        
        /// <returns name="int">Number of affected records.</param>
        public int Commit()
        {
            return context.SaveChanges();
        }

        public IQueryable<T> GetAllNoTrack()
        {
            return Entity.AsNoTracking().AsQueryable<T>();
        }
    }



    public class QmsClientDbHelper<T> : IDbHelper<T> where T : class
    {
        private QmsClientDbContext context;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">Your DbContext, that will be injected to this Repository object</param>
        public QmsClientDbHelper(QmsClientDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Get All entity<T> as List
        /// </summary>        
        /// <param name="sql">sql statement or stored procedure name</param>
        /// <param name="parameter">sql parameter(s)</param>
        /// <returns name="List<T>">list of object <T> </param>
        public List<T> GetAll(string sql, object parameter = null)
        {
            return context.Database.SqlQuerySmart<T>(sql, parameter).ToList();
        }

        /// <summary>
        /// Get entity<T> as entity.
        /// </summary>        
        /// <param name="sql">sql statement or stored procedure name</param>
        /// <param name="parameter">sql parameter(s)</param>
        /// <returns name="object<T>">object <T> </param>
        public T Get(string sql, object parameter = null)
        {
            return context.Database.SqlQuerySmart<T>(sql, parameter).First();
        }

        /// <summary>
        /// Execute sql command.
        /// </summary>        
        /// <param name="sql">sql statement or stored procedure name</param>
        /// <param name="parameter">sql parameter(s)</param>
        /// <returns name="int">number of records affected.</param>
        public int ExecuteCommand(string sql, object parameter = null)
        {
            return context.Database.ExecuteSqlCommandSmart(sql, parameter);
        }
    }
}
