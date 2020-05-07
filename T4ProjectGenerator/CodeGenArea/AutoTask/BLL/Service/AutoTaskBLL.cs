using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AutoTask.Common;
using AutoTask.Model;
using AutoTask.DAL;

namespace AutoTask.BLL
{
    /// <summary>
    /// 自动任务
    /// </summary>
    public partial class AutoTaskBLL : ServiceContextBase
    {
        public AutoTaskBLL(IDbContextComponent context)
        {
            this.Manager = new AutoTaskDAL(context);
        }






    }
}