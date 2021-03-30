


#添加数据迁移
#https://docs.abp.io/zh-Hans/abp/latest/Tutorials/Part-1?UI=MVC&DB=EF


1.Add-Migration "Created_Book_Entity"

#添加种子数据
#https://docs.abp.io/zh-Hans/abp/latest/Data-Seeding
#在 *.Domain 项目下创建派生 IDataSeedContributor 的类

#Update-Database

#更新数据库
#运行 BookStore.DbMigrator 应用程序来更新数据库:

.DbMigrator 是一个控制台使用程序,可以在开发和生产环境迁移数据库架构和初始化种子数据.



