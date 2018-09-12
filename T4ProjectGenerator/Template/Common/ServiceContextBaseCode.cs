using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T4ProjectGenerator
{
    public partial class ServiceContextBase
    {
        public ServiceContextBase(ProjectConfig config)
            : base(config)
        {
            this.FileName = string.Format("{0}{1}{2}ServiceContextBase.cs", config.CodeGenAreaDir, config.Name, config.CommonDir);
        }
    }
}
