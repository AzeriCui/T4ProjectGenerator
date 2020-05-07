using System;
using System.Data;
using System.Data.SqlClient;
using AutoTask.Common;

namespace AutoTask.BLL
{
    public partial class AutoTaskContext : DbProviderFactory
    {
        private Lazy<IDbContextComponent> _Context;

        protected override IDbContextComponent Context
        {
            get { return _Context.Value; }
        }

        public AutoTaskContext()
            : base()
        {
            _Context = new Lazy<IDbContextComponent>(() => new AutoTaskContextWrapper());
            _ClientAutoTask = new Lazy<AutoTaskBLL>(() => new AutoTaskBLL(Context));
        }

        private Lazy<AutoTaskBLL> _ClientAutoTask = null;
        /// <summary>
        /// 自动任务
        /// </summary>
        public AutoTaskBLL ClientAutoTask
        {
            get { return _ClientAutoTask.Value; }
        }

    }

    internal class AutoTaskContextWrapper : IDbContextComponent
    {
        public IDbConnection Connection { get; set; }

        public IDbTransaction Transaction { get; set; }

        public AutoTaskContextWrapper()
        {
            this.Connection = new SqlConnection(ConfigManager.GetValue("AutoTaskContext"));
            if (this.Connection.State != ConnectionState.Open)
            {
                this.Connection.Open();
            }
        }

        public void Dispose()
        {
            if (this.Transaction != null)
            {
                this.Transaction.Dispose();
                this.Transaction = null;
            }
            if (this.Connection != null)
            {
                this.Connection.Close();
                this.Connection.Dispose();
                this.Connection = null;
            }
        }
    }
}