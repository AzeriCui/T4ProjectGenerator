# T4ProjectGenerator

T4项目代码生成器，T4ProjectGenerator在经历多个正式项目的历练后做出了性能化的整改与Bug上的修复。

* 能同时生成多个项目
* 使用简单直接运行T4ProjectGenerator.exe
* 配置文件ProjectConfig.json

```javascript
[
  {
    "Name": "项目名字",
    "CodeGenAreaDir": "生成代码指向目录",
    "ConnectionString": "数据库连接字符串",
    "SelectWhere": " 查询条件 ",

    "CommonNamespace": "common 命名空间",
    "CommonDir": "common 相对目录",

    "ModelNamespace": "model 命名空间",
    "ModelDir": "model 相对目录",
    "ModelClassSuffix": "model 类后缀",
    "ModelFileSuffix": "model 文件后缀",

    "ManagerNamespace": "manager 命名空间",
    "ManagerDir": "manager 相对目录",
    "ManagerClassSuffix": "manager 类后缀",
    "ManagerFileSuffix": "manager 文件后缀",

    "ServiceNamespace": "service 命名空间",
    "ServiceDir": "service 相对目录",
    "ServiceClassSuffix": "service 类后缀",
    "ServiceFileSuffix": "service 文件后缀",

    "ContextNamespace": "context 命名空间",
    "ContextDir": "context 相对目录",
    "ContextClassPrefix": "context 类后缀",
    "ContextFilePrefix": "context 文件后缀",
    "ContextConnectionStringKey": "context 连接字符串KEY"
  }
]
```
