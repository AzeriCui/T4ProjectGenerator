using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T4ProjectGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            string configString = File.ReadAllText("ProjectConfig.json");
            List<ProjectConfig> collection = JsonConvert.DeserializeObject<List<ProjectConfig>>(configString);
            foreach (ProjectConfig item in collection)
            {
                CommonBuilder builder = new CommonBuilder(item);
                builder.Run();
            }

            Console.WriteLine("OK");
        }
    }
}
