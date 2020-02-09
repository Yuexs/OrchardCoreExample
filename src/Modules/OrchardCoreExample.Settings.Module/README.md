# 示例设置模块
包含权限,导航,设置功能演示

## 1.添加nuget引用
```
Install-Package OrchardCore.Module.Targets -Version 1.0.0-rc1-10004
Install-Package OrchardCore.DisplayManagement -Version 1.0.0-rc1-10004
Install-Package OrchardCore.Navigation.Core -Version 1.0.0-rc1-10004
```
## 2.创建Manifest.cs文件定义模块
```
using OrchardCore.Modules.Manifest;

[assembly: Module(
    Name = "OrchardCoreExample.Settings.Module",
    Author = "Yuex.S",
    Website = "https://gitee.com/YuexS/OrchardCoreExample",
    Version = "1.0.0",
    Description = "示例菜单导航设置信息",
    Category = "Example"
)]
```

文档待完成

![image](https://gitee.com/YuexS/OrchardCoreExample/raw/master/imgs/WX20200204-155034%402x.png)

![image](https://gitee.com/YuexS/OrchardCoreExample/raw/master/imgs/WX20200204-155128@2x.png)

