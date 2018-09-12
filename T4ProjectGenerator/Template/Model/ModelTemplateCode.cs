using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T4ProjectGenerator
{
    public partial class ModelTemplate
    {

        private string _TableName;
        private IList<DataSchema> _ColumnList;

        public ModelTemplate(ProjectConfig config, string tableName, IList<DataSchema> columnList)
            : base(config)
        {
            _TableName = tableName;
            _ColumnList = columnList;

            this.FileName = string.Format("{0}{1}{2}{3}{4}.cs",
                config.CodeGenAreaDir, config.Name, config.ModelDir, _TableName, config.ModelFileSuffix);
        }

    }

}
