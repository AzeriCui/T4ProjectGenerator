﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本: 12.0.0.0
//  
//     对此文件的更改可能会导致不正确的行为。此外，如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
// ------------------------------------------------------------------------------
namespace T4ProjectGenerator
{
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "E:\zy\T4\T4\T4ProjectGenerator\T4ProjectGenerator\Template\DAL\ManagerTemplate.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "12.0.0.0")]
    public partial class ManagerTemplate : Base
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public override string TransformText()
        {
            this.Write("using System;\r\nusing System.Collections.Generic;\r\nusing System.Linq;\r\nusing Syste" +
                    "m.Text;\r\n\r\nusing ");
            
            #line 11 "E:\zy\T4\T4\T4ProjectGenerator\T4ProjectGenerator\Template\DAL\ManagerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Config.CommonNamespace));
            
            #line default
            #line hidden
            this.Write(";\r\nusing ");
            
            #line 12 "E:\zy\T4\T4\T4ProjectGenerator\T4ProjectGenerator\Template\DAL\ManagerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Config.ModelNamespace));
            
            #line default
            #line hidden
            this.Write(";\r\n\r\nnamespace ");
            
            #line 14 "E:\zy\T4\T4\T4ProjectGenerator\T4ProjectGenerator\Template\DAL\ManagerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Config.ManagerNamespace));
            
            #line default
            #line hidden
            this.Write("\r\n{\r\n    /// <summary>\r\n    /// ");
            
            #line 17 "E:\zy\T4\T4\T4ProjectGenerator\T4ProjectGenerator\Template\DAL\ManagerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_ColumnList.FirstOrDefault().TableDescription));
            
            #line default
            #line hidden
            this.Write("\r\n    /// </summary>\r\n    public partial class ");
            
            #line 19 "E:\zy\T4\T4\T4ProjectGenerator\T4ProjectGenerator\Template\DAL\ManagerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_TableName));
            
            #line default
            #line hidden
            
            #line 19 "E:\zy\T4\T4\T4ProjectGenerator\T4ProjectGenerator\Template\DAL\ManagerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Config.ManagerClassSuffix));
            
            #line default
            #line hidden
            this.Write(" : MsSqlAccessBase\r\n    {\r\n        public ");
            
            #line 21 "E:\zy\T4\T4\T4ProjectGenerator\T4ProjectGenerator\Template\DAL\ManagerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_TableName));
            
            #line default
            #line hidden
            
            #line 21 "E:\zy\T4\T4\T4ProjectGenerator\T4ProjectGenerator\Template\DAL\ManagerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Config.ManagerClassSuffix));
            
            #line default
            #line hidden
            this.Write("(IDbContextComponent context)\r\n        {\r\n            this.Connection = context.C" +
                    "onnection;\r\n            this.Transaction = context.Transaction;\r\n            thi" +
                    "s.Config = new ");
            
            #line 25 "E:\zy\T4\T4\T4ProjectGenerator\T4ProjectGenerator\Template\DAL\ManagerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_TableName));
            
            #line default
            #line hidden
            this.Write("Config();\r\n        }\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n    }\r\n}");
            return this.GenerationEnvironment.ToString();
        }
    }
    
    #line default
    #line hidden
}
