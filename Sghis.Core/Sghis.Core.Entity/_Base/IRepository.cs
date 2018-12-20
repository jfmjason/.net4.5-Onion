using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sghis.Core.Entity.Base
{
    /// <summary>
    /// IRepository, repository objects should implement all methods
    /// </summary>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Add Entity
        /// </summary>
        /// <param name="entity">Your Entity to be added e.g. Person</param>
        /// <returns name="entity">The added Entity with Id set from database.</param>
        T Add(T entity);

        /// <summary>
        /// Update Entity
        /// </summary>
        /// <param name="entity">Your Entity to be updated e.g. Person</param>
        /// <returns name="void"></param>
        void Update(T entity);

        /// <summary>
        /// Update Entity by force, this is usually used when the entity was updated outside the context
        /// </summary>
        /// <param name="entity">Your Entity to be updated e.g. Person</param>
        /// <returns name="void"></param>
        void UpdateForce(T entity);

        /// <summary>
        /// Delete Entity
        /// </summary>
        /// <param name="entity">Your Entity to be deleted e.g. Person</param>
        /// <returns name="void"></param>
        void Delete(T entity);

        /// <summary>
        /// Get Entity By Id
        /// </summary>
        /// <param name="id">your entity id</param>
        /// <returns name="Entity">Entity</param>
        T GetById(object id);

        /// <summary>
        /// Get Entity by specified criteria
        /// </summary>
        /// <param name="criteria">Lamda expression</param>
        /// <returns name="Entity"></param>
        T GetByCriteria(Func<T, bool> criteria);

        /// <summary>
        /// Get All Entity as IQueryable
        /// </summary>        
        /// <returns name="IQueryable"></param>
        IQueryable<T> GetAll();

        /// <summary>
        /// Get All Entity as IQueryable and EF context will not track the changes
        /// because of possible changes happened outside the context
        /// </summary>        
        /// <returns name="IQueryable"></param>
        IQueryable<T> GetAllNoTrack();

        /// <summary>
        /// Commit: to apply changes in the current context to your database.
        /// </summary>        
        /// <returns name="int">Number of affected records.</param>
        int Commit();
    }



    /// <summary>
    /// IDbHelper, provide a simple solution to call stored procedure in a EF context
    /// </summary>
    public interface IDbHelper<T> where T : class
    {
        /// <summary>
        /// Get All entity<T> as List
        /// </summary>        
        /// <param name="sql">sql statement or stored procedure name</param>
        /// <param name="parameter">sql parameter(s)</param>
        /// <returns name="List<T>">list of object <T> </param>
        List<T> GetAll(string sql, object parameter = null);

        /// <summary>
        /// Get entity<T> as entity.
        /// </summary>        
        /// <param name="sql">sql statement or stored procedure name</param>
        /// <param name="parameter">sql parameter(s)</param>
        /// <returns name="object<T>">object <T> </param>
        T Get(string sql, object parameter = null);

        /// <summary>
        /// Execute sql command.
        /// </summary>        
        /// <param name="sql">sql statement or stored procedure name</param>
        /// <param name="parameter">sql parameter(s)</param>
        /// <returns name="int">number of records affected.</param>
        int ExecuteCommand(string sql, object parameter = null);
    }
}
