using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T4ProjectGenerator
{
    public class CommonBuilder
    {
        public List<Base> CommonList = new List<Base>();

        public CommonBuilder(ProjectConfig config)
        {
            CommonList.Add(new ConfigManager(config));
            CommonList.Add(new DbProviderFactory(config));
            CommonList.Add(new Expr(config));
            CommonList.Add(new IDbContextComponent(config));
            CommonList.Add(new MsSqlAccessBase(config));
            CommonList.Add(new MsSqlConfig(config));
            CommonList.Add(new ServiceContextBase(config));

            DataManager manager = new DataManager(config);
            IList<DataSchema> schemaList = manager.GetDatabaseSchema();

            foreach (var item in schemaList.GroupBy(o => o.TableName))
            {
                CommonList.Add(new ModelTemplate(config, item.Key, item.ToList()));

                CommonList.Add(new ManagerTemplate(config, item.Key, item.ToList()));
                CommonList.Add(new ManagerDevelopTemplate(config, item.Key, item.ToList()));

                CommonList.Add(new ServiceTemplate(config, item.Key, item.ToList()));
                CommonList.Add(new ServiceDevelopTemplate(config, item.Key, item.ToList()));
            }

            IList<DataSchema> tableList = schemaList.GroupBy(o => o.TableName)
                .Select(o => new DataSchema()
                {
                    TableName = o.Key,
                    TableDescription = o.FirstOrDefault().TableDescription
                }).ToList();
            CommonList.Add(new ContextTemplate(config, tableList));

        }

        public void Run()
        {
            foreach (var item in CommonList)
            {
                item.RenderToFile();
            }
        }

    }
}
