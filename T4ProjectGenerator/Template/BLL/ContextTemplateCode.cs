using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T4ProjectGenerator
{
    [Context]
    public partial class ContextTemplate
    {

        private IList<DataSchema> _ColumnList;

        public ContextTemplate(ProjectConfig config, IList<DataSchema> columnList)
            : base(config)
        {
            _ColumnList = columnList;

            this.FileName = string.Format("{0}{1}{2}{3}Context.cs",
                config.CodeGenAreaDir, config.Name, config.ContextDir, config.ContextFilePrefix);
        }

    }

}
