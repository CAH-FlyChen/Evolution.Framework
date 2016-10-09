# Evolution.Framework   
##基础信息  
本Framework基于NFine修改制作  
基于 Asp.net MVC 6, EF Core 1.0, BootStrap制作  
##额外特性  
1. 跨平台  
2. EF底层，便于适配不同数据库（只需要替换数据库的Provider）  
3. 支持插件化开发  
4. 全异步操作
5. 资源级别的权限控制方便Restful API调用  

##测试信息  
已在 Ubuntu 14.4 + SqlServer 上测试通过  
已在 Windows + SqlServer 上测试通过  

##路线图  
1. 实现EF Core 1.0替换............................................<font color="green">[已完成]</font>  
1. 跨平台 Ubuntu 14.4上测试....................................<font color="green">[已完成]</font>  
1. 实现Conroller及相关方法的全异步执行.................<font color="green">[已完成]</font>  
1. 实现插件机制........................................................<font color="green">[已完成]</font>   
1. 实现Mysql数据库测试，将MySQL作为默认数据库.<font color="green">[已完成]</font> 
1. 增加Redis缓存（在config文件中进行配置是否使用，默认不使用）.....<font color="green">[已完成]</font>  
2. 支持模板工具创建  
2. 修改验证为JWT方便Restful API调用    
3. 修改日志数据库为MongoDB  
  
##联系信息  
QQ群：489791124