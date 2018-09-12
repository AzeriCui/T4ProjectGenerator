using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T4ProjectGenerator
{

    public class DataManager
    {

        private string SchemaSqlString
        {
            get
            {
                return @"
                    SELECT  obj.name as TableName,    --表名  
                    epTwo.[value] as [TableDescription],
                    col.colorder AS SerialNum , --序号  
                    col.name AS ColumnName ,    --列名  
                    ISNULL(ep.[value], '') AS [Description] ,   --列说明  
                    t.name AS [DataType] ,   --数据类型  
                    col.length AS [DataLength] ,    --长度  
                    ISNULL(COLUMNPROPERTY(col.id, col.name, 'Scale'), 0) AS Point ,  --小数点  
                    CASE WHEN COLUMNPROPERTY(col.id, col.name, 'IsIdentity') = 1 THEN 'True'  
                    ELSE 'False'  
                    END AS Identify ,     --标识  
                    CASE WHEN EXISTS ( SELECT   1  
                    FROM     dbo.sysindexes si  
                    INNER JOIN dbo.sysindexkeys sik ON si.id = sik.id  
                    AND si.indid = sik.indid  
                    INNER JOIN dbo.syscolumns sc ON sc.id = sik.id  
                    AND sc.colid = sik.colid  
                    INNER JOIN dbo.sysobjects so ON so.name = si.name  
                    AND so.xtype = 'PK'  
                    WHERE    sc.id = col.id  
                    AND sc.colid = col.colid ) THEN 'True'  
                    ELSE 'False'  
                    END AS PK ,   --是否主键  
                    CASE WHEN col.isnullable = 1 THEN 'True'  
                    ELSE 'False'  
                    END AS [IsNull] ,   --能否为空  
                    ISNULL(comm.text, '') AS DefaultValue    --默认值  
                    FROM    dbo.syscolumns col  
                    LEFT  JOIN dbo.systypes t ON col.xtype = t.xusertype  
                    inner JOIN dbo.sysobjects obj ON col.id = obj.id  
                    AND obj.xtype = 'U'  
                    AND obj.status >= 0  
                    LEFT  JOIN dbo.syscomments comm ON col.cdefault = comm.id  
                    LEFT  JOIN sys.extended_properties ep ON col.id = ep.major_id  
                    AND col.colid = ep.minor_id  
                    AND ep.name = 'MS_Description'  
                    LEFT  JOIN sys.extended_properties epTwo ON obj.id = epTwo.major_id  
                    AND epTwo.minor_id = 0  
                    AND epTwo.name = 'MS_Description'  
                    " + Config.SelectWhere;
            }
        }

        public ProjectConfig Config { get; set; }

        public DataManager(ProjectConfig config)
        {
            this.Config = config;
        }

        public IList<DataSchema> GetDatabaseSchema()
        {
            IList<DataSchema> schemaList = new List<DataSchema>();
            string connString = Config.ConnectionString;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand comm = new SqlCommand())
                {
                    conn.Open();
                    comm.Connection = conn;
                    comm.CommandText = SchemaSqlString;
                    using (SqlDataReader reader = comm.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DataSchema schema = new DataSchema();
                            schema.TableName = Convert.ToString(reader["TableName"]);
                            schema.TableDescription = Convert.ToString(reader["TableDescription"]);
                            schema.SerialNum = Convert.ToInt32(reader["SerialNum"]);
                            schema.ColumnName = Convert.ToString(reader["ColumnName"]);
                            schema.Description = Convert.ToString(reader["Description"]);
                            schema.DataType = Convert.ToString(reader["DataType"]);
                            schema.DataLength = Convert.ToInt32(reader["DataLength"]);
                            schema.Point = Convert.ToInt32(reader["Point"]);
                            schema.Identify = Convert.ToBoolean(reader["Identify"]);
                            schema.PK = Convert.ToBoolean(reader["PK"]);
                            schema.IsNull = Convert.ToBoolean(reader["IsNull"]);
                            schema.DefaultValue = Convert.ToString(reader["DefaultValue"]);
                            schemaList.Add(schema);
                        }
                    }
                }
            }

            return schemaList;
        }

        public static string GetInsertField(IList<DataSchema> schemaList)
        {
            IList<DataSchema> tempList = schemaList.Where(o => (o.Identify == false && o.PK == false) || (o.Identify == false && o.PK == true)).ToList();
            return string.Format("[{0}]", string.Join("], [", tempList.Select(o => o.ColumnName)));
        }

        public static string GetInsertValue(IList<DataSchema> schemaList)
        {
            IList<DataSchema> tempList = schemaList.Where(o => (o.Identify == false && o.PK == false) || (o.Identify == false && o.PK == true)).ToList();
            return string.Format("@{0} ", string.Join(" , @", tempList.Select(o => o.ColumnName)));
        }

        public static bool IsUpdate(IList<DataSchema> schemaList)
        {
            if (schemaList.Count(o => (o.PK == true)) == schemaList.Count)
            {
                return false;
            }
            bool isUpdate = schemaList.Count(o => (o.Identify == false && o.PK == false)) > 0;
            if (!isUpdate)
            {
                return isUpdate;
            }
            isUpdate = isUpdate & (schemaList.Count(o => (o.PK == true)) > 0);
            return isUpdate;
        }

        public static string GetUpdateField(IList<DataSchema> schemaList, bool isPK)
        {
            IList<string> tempList = schemaList.Where(o => (o.PK == isPK)).Select(o => string.Format("[{0}] = @{0}", o.ColumnName)).ToList();
            return string.Join(", ", tempList);
        }

    }

    public class DataSchema
    {
        public string TableName { get; set; }
        public string TableDescription { get; set; }
        public int SerialNum { get; set; }
        public string ColumnName { get; set; }
        public string Description { get; set; }
        public string DataType { get; set; }
        public int DataLength { get; set; }
        public int Point { get; set; }
        public bool Identify { get; set; }
        public bool PK { get; set; }
        public bool IsNull { get; set; }
        public string DefaultValue { get; set; }
        public string CSharpDataType
        {
            get
            {
                string dataType = "string";
                switch (this.DataType)
                {
                    case "bigint":
                        dataType = "long";
                        break;
                    case "image":
                    case "timestamp":
                    case "varbinary":
                    case "binary":
                        dataType = "byte[]";
                        break;
                    case "bit":
                        dataType = "bool";
                        break;
                    case "char":
                    case "nchar":
                    case "text":
                    case "ntext":
                    case "nvarchar":
                    case "varchar":
                        dataType = "string";
                        break;
                    case "date":
                    case "datetime":
                    case "datetime2":
                    case "datetimeoffset":
                    case "smalldatetime":
                    case "time":
                        dataType = "System.DateTime";
                        break;
                    case "decimal":
                    case "money":
                    case "smallmoney":
                        dataType = "decimal";
                        break;
                    case "float":
                    case "real":
                        dataType = "float";
                        break;
                    case "int":
                    case "smallint":
                    case "tinyint":
                        dataType = "int";
                        break;
                    case "uniqueidentifier":
                        dataType = "System.Guid";
                        break;
                }
                return dataType;
            }
        }
    }
}
