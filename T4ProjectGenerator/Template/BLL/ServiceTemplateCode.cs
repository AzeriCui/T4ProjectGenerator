using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T4ProjectGenerator
{
    [Service]
    public partial class ServiceTemplate
    {

        private string _TableName;
        private IList<DataSchema> _ColumnList;

        public ServiceTemplate(ProjectConfig config, string tableName, IList<DataSchema> columnList)
            : base(config)
        {
            _TableName = tableName;
            _ColumnList = columnList;

            this.FileName = string.Format("{0}{1}{2}{3}{4}.cs",
                config.CodeGenAreaDir, config.Name, config.ServiceDir, _TableName, config.ServiceFileSuffix);
        }

    }

}
