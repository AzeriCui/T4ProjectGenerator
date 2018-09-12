using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T4ProjectGenerator
{
    public partial class DbProviderFactory
    {
        public DbProviderFactory(ProjectConfig config)
            : base(config)
        {
            this.FileName = string.Format("{0}{1}{2}DbProviderFactory.cs", config.CodeGenAreaDir, config.Name, config.CommonDir);
        }
    }
}
