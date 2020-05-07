using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AutoTask.Common;
using AutoTask.Model;

namespace AutoTask.DAL
{
    /// <summary>
    /// 自动任务
    /// </summary>
    public partial class AutoTaskDAL : MsSqlAccessBase
    {
        public AutoTaskDAL(IDbContextComponent context)
        {
            this.Connection = context.Connection;
            this.Transaction = context.Transaction;
            this.Config = new AutoTaskConfig();
        }







    }
}