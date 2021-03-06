using Dapper;
using System;
using System.Collections.Generic;
using System.Data;

namespace AutoTask.Common
{
    /// <summary>
    /// sql语句
    /// </summary>
    public interface ISqlConfig
    {
        /// <summary>
        /// table 
        /// </summary>
        string TableName { get; }

        /// <summary>
        /// insert into table ({0}) values ({1}) 
        /// </summary>
        string Insert { get; }

        /// <summary>
        /// insert into table ({0}) values ({1}); select scope_identity()
        /// </summary>
        string InsertIdentity { get; }

        /// <summary>
        /// update table set {0} where {1}
        /// </summary>
        string Update { get; }

        /// <summary>
        /// delete from table where {0}
        /// </summary>
        string Delete { get; }

        /// <summary>
        /// select count(*) from table 
        /// </summary>
        string SelectAllCount { get; }

        /// <summary>
        /// select count(*) from table where {0} 
        /// </summary>
        string SelectCount { get; }

        /// <summary>
        /// select top({0}) * from table where {1}
        /// </summary>
        string SelectTop { get; }

        /// <summary>
        /// select top({0}) * from table where {1} order by {2}
        /// </summary>
        string SelectTopOrder { get; }

        /// <summary>
        /// select * from table 
        /// </summary>
        string SelectAll { get; }

        /// <summary>
        /// select * from table where {0} 
        /// </summary>
        string SelectWhere { get; }

        /// <summary>
        /// select * from table where {0} order by {1} 
        /// </summary>
        string SelectWhereOrder { get; }

        /// <summary>
        /// select * from (select row_number() over (order by {1}) as RowIndex, * from table where {0}) as t where t.RowIndex between {2} and {3} 
        /// </summary>
        string SelectPage { get; }
    }

    /// <summary>
    /// 数据库访问组件
    /// </summary>
    public interface IDbContextComponent : IDisposable
    {
        /// <summary>
        /// 数据库连接
        /// </summary>
        IDbConnection Connection { get; set; }
        /// <summary>
        /// 数据库事务
        /// </summary>
        IDbTransaction Transaction { get; set; }
    }

    /// <summary>
    /// 事务处理组件
    /// </summary>
    public interface IDbTransactionAccess : IDisposable
    {
        /// <summary>
        /// 开启状态
        /// </summary>
        bool IsBeginTrans { get; set; }

        /// <summary>
        /// 开启
        /// </summary>
        void BeginTrans();

        /// <summary>
        /// 提交
        /// </summary>
        void Commit();

        /// <summary>
        /// 回滚
        /// </summary>
        void Rollback();
    }

    /// <summary>
    /// 数据库访问接口
    /// </summary>
    public interface IDbAccess
    {
        /// <summary>
        /// 添加或批量添加
        /// </summary>
        int Add<TEntity>(params TEntity[] entities) where TEntity : class;

        /// <summary>
        /// 单个添加
        /// </summary>
        TIdentity Add<TEntity, TIdentity>(TEntity entity)
            where TEntity : class
            where TIdentity : IConvertible;

        /// <summary>
        /// 修改
        /// </summary>
        int Update<TEntity>(TEntity entity) where TEntity : class;

        /// <summary>
        /// 删除
        /// </summary>
        int Delete(Expr expr);

        /// <summary>
        /// 判断存在
        /// </summary>
        bool IsExist(Expr expr);

        /// <summary>
        /// 总数量
        /// </summary>
        int GetCount();

        /// <summary>
        /// 查询条件数量
        /// </summary>
        int GetCount(Expr expr);

        /// <summary>
        /// 查询实体
        /// </summary>
        TEntity Get<TEntity>(Expr expr) where TEntity : class;

        /// <summary>
        /// 查询实体
        /// </summary>
        TEntity Get<TEntity>(Expr expr, OrderByExpr orderBy) where TEntity : class;

        /// <summary>
        /// 查询列表
        /// </summary>
        IEnumerable<TEntity> GetList<TEntity>() where TEntity : class;

        /// <summary>
        /// 查询列表
        /// </summary>
        IEnumerable<TEntity> GetList<TEntity>(Expr expr) where TEntity : class;

        /// <summary>
        /// 查询列表
        /// </summary>
        IEnumerable<TEntity> GetList<TEntity>(Expr expr, int top) where TEntity : class;

        /// <summary>
        /// 查询列表
        /// </summary>
        IEnumerable<TEntity> GetList<TEntity>(Expr expr, int top, OrderByExpr orderBy) where TEntity : class;

        /// <summary>
        /// 查询列表
        /// </summary>
        IEnumerable<TEntity> GetList<TEntity>(Expr expr, OrderByExpr orderBy) where TEntity : class;

        /// <summary>
        /// 查询列表
        /// </summary>
        IEnumerable<TEntity> GetList<TEntity>(Expr expr, OrderByExpr orderBy, int startPage, int endPage) where TEntity : class;
    }

    /// <summary>
    /// 数据库访问的抽象类。
    /// 此类提供对数据库读写的基础函数。
    /// </summary>
    public class MsSqlAccessBase : IDbAccess
    {
        protected IDbConnection Connection { get; set; }
        protected IDbTransaction Transaction { get; set; }
        protected ISqlConfig Config { get; set; }

        public MsSqlAccessBase() { }

        /// <summary>
        /// 添加或批量添加
        /// </summary>
        public int Add<TEntity>(params TEntity[] entities) where TEntity : class
        {
            return this.Connection.Execute(this.Config.Insert, entities, this.Transaction);
        }

        /// <summary>
        /// 单个添加
        /// </summary>
        public TIdentity Add<TEntity, TIdentity>(TEntity entity)
            where TEntity : class
            where TIdentity : IConvertible
        {
            return this.Connection.ExecuteScalar<TIdentity>(this.Config.InsertIdentity, entity, this.Transaction);
        }

        /// <summary>
        /// 修改
        /// </summary>
        public int Update<TEntity>(TEntity entity) where TEntity : class
        {
            return this.Connection.Execute(this.Config.Update, entity, this.Transaction);
        }

        /// <summary>
        /// 删除
        /// </summary>
        public int Delete(Expr expr)
        {
            DynamicParameters parameters = new DynamicParameters();
            string deleteString = string.Format(this.Config.Delete, expr.ToWhere(parameters));
            return this.Connection.Execute(deleteString, parameters, this.Transaction);
        }

        /// <summary>
        /// 判断存在
        /// </summary>
        public bool IsExist(Expr expr)
        {
            DynamicParameters parameters = new DynamicParameters();
            string selectString = string.Format(this.Config.SelectCount, expr.ToWhere(parameters));
            int result = this.Connection.ExecuteScalar<int>(selectString, parameters, this.Transaction);
            return result > 0;
        }

        /// <summary>
        /// 总数量
        /// </summary>
        public int GetCount()
        {
            DynamicParameters parameters = new DynamicParameters();
            return this.Connection.ExecuteScalar<int>(this.Config.SelectAllCount, transaction: this.Transaction);
        }

        /// <summary>
        /// 查询条件数量
        /// </summary>
        public int GetCount(Expr expr)
        {
            DynamicParameters parameters = new DynamicParameters();
            string selectString = string.Format(this.Config.SelectCount, expr.ToWhere(parameters));
            return this.Connection.ExecuteScalar<int>(selectString, parameters, this.Transaction);
        }

        /// <summary>
        /// 查询实体
        /// </summary>
        public TEntity Get<TEntity>(Expr expr) where TEntity : class
        {
            DynamicParameters parameters = new DynamicParameters();
            string selectString = string.Format(this.Config.SelectTop, 1, expr.ToWhere(parameters));
            return this.Connection.QueryFirstOrDefault<TEntity>(selectString, parameters, this.Transaction);
        }

        /// <summary>
        /// 查询实体
        /// </summary>
        public TEntity Get<TEntity>(Expr expr, OrderByExpr orderBy) where TEntity : class
        {
            DynamicParameters parameters = new DynamicParameters();
            string selectString = string.Format(this.Config.SelectTopOrder, 1, expr.ToWhere(parameters), orderBy.ToOrderBy());
            return this.Connection.QueryFirstOrDefault<TEntity>(selectString, parameters, this.Transaction);
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        public IEnumerable<TEntity> GetList<TEntity>() where TEntity : class
        {
            return this.Connection.Query<TEntity>(this.Config.SelectAll, transaction: this.Transaction);
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        public IEnumerable<TEntity> GetList<TEntity>(Expr expr) where TEntity : class
        {
            DynamicParameters parameters = new DynamicParameters();
            string selectString = string.Format(this.Config.SelectWhere, expr.ToWhere(parameters), 1);
            return this.Connection.Query<TEntity>(selectString, parameters, this.Transaction);
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        public IEnumerable<TEntity> GetList<TEntity>(Expr expr, int top) where TEntity : class
        {
            DynamicParameters parameters = new DynamicParameters();
            string selectString = string.Format(this.Config.SelectTop, top, expr.ToWhere(parameters));
            return this.Connection.Query<TEntity>(selectString, parameters, this.Transaction);
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        public IEnumerable<TEntity> GetList<TEntity>(Expr expr, int top, OrderByExpr orderBy) where TEntity : class
        {
            DynamicParameters parameters = new DynamicParameters();
            string selectString = string.Format(this.Config.SelectTopOrder, top, expr.ToWhere(parameters), orderBy.ToOrderBy());
            return this.Connection.Query<TEntity>(selectString, parameters, this.Transaction);
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        public IEnumerable<TEntity> GetList<TEntity>(Expr expr, OrderByExpr orderBy) where TEntity : class
        {
            DynamicParameters parameters = new DynamicParameters();
            string selectString = string.Format(this.Config.SelectWhereOrder, expr.ToWhere(parameters), orderBy.ToOrderBy());
            return this.Connection.Query<TEntity>(selectString, parameters, this.Transaction);
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        public IEnumerable<TEntity> GetList<TEntity>(Expr expr, OrderByExpr orderBy, int startPage, int endPage) where TEntity : class
        {
            DynamicParameters parameters = new DynamicParameters();
            string selectString = string.Format(this.Config.SelectPage, expr.ToWhere(parameters), orderBy.ToOrderBy(), startPage, endPage);
            return this.Connection.Query<TEntity>(selectString, parameters, this.Transaction);
        }
    }

    /// <summary>
    /// 业务逻辑类提供对数据库读写的基础函数
    /// </summary>
    public class ServiceContextBase : IDbAccess
    {
        protected MsSqlAccessBase Manager { get; set; }

        public ServiceContextBase() { }

        public int Add<TEntity>(params TEntity[] entities) where TEntity : class
        {
            if (entities == null || entities.Length <= 0)
            {
                throw new ArgumentNullException("entities", "添加实体集不能为空");
            }
            return this.Manager.Add<TEntity>(entities);
        }

        public TIdentity Add<TEntity, TIdentity>(TEntity entity)
            where TEntity : class
            where TIdentity : IConvertible
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity", "添加实体不能为空");
            }
            return this.Manager.Add<TEntity, TIdentity>(entity);
        }

        public int Update<TEntity>(TEntity entity) where TEntity : class
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity", "修改实体不能为空");
            }
            return this.Manager.Update<TEntity>(entity);
        }

        public int Delete(Expr expr)
        {
            return this.Manager.Delete(expr);
        }

        public bool IsExist(Expr expr)
        {
            return this.Manager.IsExist(expr);
        }

        public int GetCount()
        {
            return this.Manager.GetCount();
        }

        public int GetCount(Expr expr)
        {
            return this.Manager.GetCount(expr);
        }

        public TEntity Get<TEntity>(Expr expr) where TEntity : class
        {
            return this.Manager.Get<TEntity>(expr);
        }

        public TEntity Get<TEntity>(Expr expr, OrderByExpr orderBy) where TEntity : class
        {
            return this.Manager.Get<TEntity>(expr, orderBy);
        }

        public IEnumerable<TEntity> GetList<TEntity>() where TEntity : class
        {
            return this.Manager.GetList<TEntity>();
        }

        public IEnumerable<TEntity> GetList<TEntity>(Expr expr) where TEntity : class
        {
            return this.Manager.GetList<TEntity>(expr);
        }

        public IEnumerable<TEntity> GetList<TEntity>(Expr expr, int top) where TEntity : class
        {
            return this.Manager.GetList<TEntity>(expr, top);
        }

        public IEnumerable<TEntity> GetList<TEntity>(Expr expr, int top, OrderByExpr orderBy) where TEntity : class
        {
            return this.Manager.GetList<TEntity>(expr, top, orderBy);
        }

        public IEnumerable<TEntity> GetList<TEntity>(Expr expr, OrderByExpr orderBy) where TEntity : class
        {
            return this.Manager.GetList<TEntity>(expr, orderBy);
        }

        public IEnumerable<TEntity> GetList<TEntity>(Expr expr, OrderByExpr orderBy, int startPage, int endPage) where TEntity : class
        {
            return this.Manager.GetList<TEntity>(expr, orderBy, startPage, endPage);
        }
    }

    /// <summary>
    /// 业务逻辑类实现事务
    /// </summary>
    public abstract class DbProviderFactory : IDbTransactionAccess
    {
        public bool IsBeginTrans { get; set; }

        protected abstract IDbContextComponent Context { get; }

        public DbProviderFactory()
        {
            this.IsBeginTrans = false;
        }

        public void BeginTrans()
        {
            if (this.IsBeginTrans == false)
            {
                this.Context.Transaction = this.Context.Connection.BeginTransaction();
                this.IsBeginTrans = true;
            }
        }

        public void Commit()
        {
            if (this.IsBeginTrans == false)
            {
                throw new ArgumentNullException("Commit", "未开启事务不能进行操作");
            }
            this.Context.Transaction.Commit();
            this.IsBeginTrans = false;
        }

        public void Rollback()
        {
            if (this.IsBeginTrans == false)
            {
                throw new ArgumentNullException("Rollback", "未开启事务不能进行操作");
            }
            this.Context.Transaction.Rollback();
            this.IsBeginTrans = false;
        }

        public void Dispose()
        {
            if (this.IsBeginTrans)
            {
                Rollback();
            }
            this.Context.Dispose();
        }
    }
}