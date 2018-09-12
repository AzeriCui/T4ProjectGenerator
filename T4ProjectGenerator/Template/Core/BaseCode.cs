using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T4ProjectGenerator
{
    public partial class Base
    {
        public ProjectConfig Config { get; set; }

        public string FileName { get; set; }

        public Base(ProjectConfig config)
        {
            this.Config = config;
        }

        public void RenderToFile()
        {
            if (!string.IsNullOrWhiteSpace(this.FileName))
            {
                if (!Directory.Exists(Path.GetDirectoryName(this.FileName)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(this.FileName));
                }

                if (!(File.Exists(this.FileName) && File.ReadAllText(this.FileName) == this.TransformText()))
                {
                    File.WriteAllText(this.FileName, this.TransformText());
                }
            }
        }

    }

}
