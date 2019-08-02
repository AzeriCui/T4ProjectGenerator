using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T4ProjectGenerator
{
    [Common]
    public partial class ConfigManager
    {
        public ConfigManager(ProjectConfig config)
            : base(config)
        {
            this.FileName = string.Format("{0}{1}{2}ConfigManager.cs", config.CodeGenAreaDir, config.Name, config.CommonDir);
        }
    }
}
