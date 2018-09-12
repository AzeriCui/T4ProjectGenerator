using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T4ProjectGenerator
{
    public partial class IDbContextComponent
    {
        public IDbContextComponent(ProjectConfig config)
            : base(config)
        {
            this.FileName = string.Format("{0}{1}{2}IDbContextComponent.cs", config.CodeGenAreaDir, config.Name, config.CommonDir);
        }
    }
}
