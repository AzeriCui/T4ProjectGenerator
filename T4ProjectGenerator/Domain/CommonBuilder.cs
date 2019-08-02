using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T4ProjectGenerator
{
    public class CommonBuilder
    {
        //public List<Base> CommonList = new List<Base>();

        //public CommonBuilder(ProjectConfig config)
        //{
        //    CommonList.Add(new ConfigManager(config));
        //    CommonList.Add(new DbProviderFactory(config));
        //    CommonList.Add(new Expr(config));
        //    CommonList.Add(new IDbContextComponent(config));
        //    CommonList.Add(new MsSqlAccessBase(config));
        //    CommonList.Add(new MsSqlConfig(config));
        //    CommonList.Add(new ServiceContextBase(config));

        //    DataManager manager = new DataManager(config);
        //    IList<DataSchema> schemaList = manager.GetDatabaseSchema();

        //    foreach (var item in schemaList.GroupBy(o => o.TableName))
        //    {
        //        CommonList.Add(new ModelTemplate(config, item.Key, item.ToList()));

        //        CommonList.Add(new ManagerTemplate(config, item.Key, item.ToList()));
        //        CommonList.Add(new ManagerDevelopTemplate(config, item.Key, item.ToList()));

        //        CommonList.Add(new ServiceTemplate(config, item.Key, item.ToList()));
        //        CommonList.Add(new ServiceDevelopTemplate(config, item.Key, item.ToList()));
        //    }

        //    IList<DataSchema> tableList = schemaList.GroupBy(o => o.TableName)
        //        .Select(o => new DataSchema()
        //        {
        //            TableName = o.Key,
        //            TableDescription = o.FirstOrDefault().TableDescription
        //        }).ToList();
        //    CommonList.Add(new ContextTemplate(config, tableList));

        //}

        public void Run()
        {
            string configString = File.ReadAllText("ProjectConfig.json");
            List<ProjectConfig> collection = JsonConvert.DeserializeObject<List<ProjectConfig>>(configString);

            var baseList = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(o => o.GetTypes().Where(p => p.BaseType == typeof(Base)));

            var commonList = baseList.Where(o => o.GetCustomAttributes(typeof(CommonAttribute), true).Length > 0);
            var serviceList = baseList.Where(o => o.GetCustomAttributes(typeof(ServiceAttribute), true).Length > 0);
            var contextList = baseList.Where(o => o.GetCustomAttributes(typeof(ContextAttribute), true).Length > 0);

            foreach (ProjectConfig config in collection)
            {
                DeleteDir(config);

                foreach (var item in commonList)
                {
                    Base baseClass = (Base)Activator.CreateInstance(item, config);
                    baseClass.RenderToFile();
                }

                DataManager manager = new DataManager(config);
                IList<DataSchema> schemaList = manager.GetDatabaseSchema();

                foreach (var item in schemaList.GroupBy(o => o.TableName))
                {
                    foreach (var serviceItem in serviceList)
                    {
                        Base baseClass = (Base)Activator.CreateInstance(serviceItem, config, item.Key, item);
                        baseClass.RenderToFile();
                    }
                }

                IList<DataSchema> tableList = schemaList.GroupBy(o => o.TableName)
                    .Select(o => new DataSchema()
                    {
                        TableName = o.Key,
                        TableDescription = o.FirstOrDefault().TableDescription
                    }).OrderBy(o => o.TableName).ToList();
                foreach (var contextItem in contextList)
                {
                    Base baseClass = (Base)Activator.CreateInstance(contextItem, config, tableList);
                    baseClass.RenderToFile();
                }

            }
        }

        private void DeleteDir(ProjectConfig config)
        {
            if (Directory.Exists(config.CodeGenAreaDir + config.Name))
            {
                Directory.Delete(config.CodeGenAreaDir + config.Name, true);
            }
        }


    }
}
