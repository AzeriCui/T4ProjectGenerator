using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T4ProjectGenerator
{
    public class ProjectConfig
    {
        /// <summary>
        /// 项目名字
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 生成代码指向目录
        /// </summary>
        public string CodeGenAreaDir { get; set; }
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string ConnectionString { get; set; }
        /// <summary>
        /// 查询条件
        /// </summary>
        public string SelectWhere { get; set; }

        /// <summary>
        /// common 命名空间
        /// </summary>
        public string CommonNamespace { get; set; }
        /// <summary>
        /// common 相对目录
        /// </summary>
        public string CommonDir { get; set; }


        /// <summary>
        /// model 命名空间
        /// </summary>
        public string ModelNamespace { get; set; }
        /// <summary>
        /// model 相对目录
        /// </summary>
        public string ModelDir { get; set; }
        /// <summary>
        /// model 类后缀
        /// </summary>
        public string ModelClassSuffix { get; set; }
        /// <summary>
        /// model 文件后缀
        /// </summary>
        public string ModelFileSuffix { get; set; }


        /// <summary>
        /// manager 命名空间
        /// </summary>
        public string ManagerNamespace { get; set; }
        /// <summary>
        /// manager 相对目录
        /// </summary>
        public string ManagerDir { get; set; }
        /// <summary>
        /// manager 类后缀
        /// </summary>
        public string ManagerClassSuffix { get; set; }
        /// <summary>
        /// manager 文件后缀
        /// </summary>
        public string ManagerFileSuffix { get; set; }
        ///// <summary>
        ///// manager 开发块相对目录
        ///// </summary>
        //public string ManagerDevelopDir { get; set; }


        /// <summary>
        /// service 命名空间
        /// </summary>
        public string ServiceNamespace { get; set; }
        /// <summary>
        /// service 相对目录
        /// </summary>
        public string ServiceDir { get; set; }
        /// <summary>
        /// service 类后缀
        /// </summary>
        public string ServiceClassSuffix { get; set; }
        /// <summary>
        /// service 文件后缀
        /// </summary>
        public string ServiceFileSuffix { get; set; }
        ///// <summary>
        ///// service 开发块相对目录
        ///// </summary>
        //public string ServiceDevelopDir { get; set; }


        /// <summary>
        /// context 命名空间
        /// </summary>
        public string ContextNamespace { get; set; }
        /// <summary>
        /// context 相对目录
        /// </summary>
        public string ContextDir { get; set; }
        /// <summary>
        /// context 类后缀
        /// </summary>
        public string ContextClassPrefix { get; set; }
        /// <summary>
        /// context 文件后缀
        /// </summary>
        public string ContextFilePrefix { get; set; }
        /// <summary>
        /// context 连接字符串KEY
        /// </summary>
        public string ContextConnectionStringKey { get; set; }




    }
}
