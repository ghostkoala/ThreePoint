using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ThreePoint.IRepository
{
    /// <summary>
    /// 仓储通用接口
    /// </summary>
    public interface IBaseRepository<T> : IDisposable
    {
        #region  创建记录（异步与同步方法）

        bool Create(T model, bool isCommit = true);

        Task<bool> CreateAsync(T model, bool isCommit = true);

        bool CreateList<T1>(List<T1> T, bool IsCommit = true) where T1 : class;
        Task<bool> CreateListAsync<T1>(List<T1> T, bool IsCommit = true) where T1 : class;

        #endregion

        #region 修改记录 （异步和同步方法）

        bool Edit(T model, bool isCommit = true);
        Task<bool> EditAsync(T model, bool isCommit = true);

        /// <summary>
        /// 批量
        /// </summary>
        /// <param name="whereLambda">表达式</param>
        /// <param name="updateLambda">m_Rep.BatchUpdate(a=>a.Age==36,a=>new SysSample() { Age = 37});</param>
        /// <returns></returns>
        int BatchUpdate(Expression<Func<T, bool>> whereLambda, Expression<Func<T, T>> updateLambda);

        Task<int> BatchUpdateAsync(Expression<Func<T, bool>> whereLambda, Expression<Func<T, T>> updateLambda);
        #endregion

        #region 删除记录 （异步和同步方法）

        bool Delete(T model, bool isCommit = true);
        Task<bool> DeleteAsync(T model, bool isCommit = true);
        /// <summary>
        /// 按主键删除
        /// </summary>
        /// <param name="keyValues"></param>
        int Delete(params object[] keyValues);
        Task<int> DeleteAsync(params object[] keyValues);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="whereLambda">删除条件</param>
        /// <returns></returns>
        int BatchDelete(Expression<Func<T, bool>> whereLambda);
        Task<int> BatchDeleteAsync(Expression<Func<T, bool>> whereLambda);
        #endregion

        #region 查询记录（异步和同步方法）

        T GetById(params object[] keyValues);

        Task<T> GetByIdAsync(params object[] keyValues);

        T GetSingleWhere(Expression<Func<T, bool>> whereLambda);
        /// <summary>
        /// 获得所有数据
        /// </summary>
        /// <returns></returns>
        IQueryable<T> GetList();
        /// <summary>
        /// 根据表达式获取数据
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        IQueryable<T> GetList(Expression<Func<T, bool>> whereLambda);


        IQueryable<T> GetList<S>(int pageSize, int pageIndex, out int total
            , Expression<Func<T, bool>> whereLambda, bool isAsc, Expression<Func<T, bool>> orderByLambda);

        #endregion

        #region 执行数据库语句 (同步和异步方法)
        /// <summary>
        /// 执行一条SQL语句
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        int ExecuteSqlCommand(string sql);
        //int ExecuteSqlCommand(string sql, params SqlParameter[] sp);
        Task<int> ExecuteSqlCommandAsync(string sql);
        IQueryable<T> SqlQuery(string sql);
        IQueryable<T> SqlQuery(string sql, params object[] paras);
        #endregion

        bool IsExist(object id);
        int SaveChanges();
    }
}
