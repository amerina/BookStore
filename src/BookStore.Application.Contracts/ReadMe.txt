#https://docs.abp.io/zh-Hans/abp/latest/Tutorials/Part-1?UI=MVC&DB=EF

#DTO
#https://docs.abp.io/zh-Hans/abp/latest/Data-Transfer-Objects
#https://docs.abp.io/zh-Hans/abp/latest/Object-To-Object-Mapping
#DTO类被用来在 表示层 和 应用层 传递数据


#为了在页面上展示书籍信息,BookDto被用来将书籍数据传递到表示层.
#BookDto继承自 AuditedEntityDto<Guid>.跟上面定义的 Book 实体一样具有一些审计属性.

在将书籍返回到表示层时,需要将Book实体转换为BookDto对象. 
AutoMapper库可以在定义了正确的映射时自动执行此转换. 
启动模板配置了AutoMapper,因此你只需在BookStore.Application项目的BookStoreApplicationAutoMapperProfile类中定义映射:


#CreateUpdateBookDto
	这个DTO类被用于在创建或更新书籍的时候从用户界面获取图书信息.
	它定义了数据注释属性(如[Required])来定义属性的验证.
#https://docs.abp.io/zh-Hans/abp/latest/Validation



#IBookAppService
#为应用程序定义接口,在BookStore.Application.Contracts项目中定义一个名为IBookAppService的接口


#https://docs.abp.io/zh-Hans/abp/latest/API/Auto-API-Controllers


#Authorization授权
#https://docs.abp.io/en/abp/latest/Tutorials/Part-5?UI=MVC&DB=EF
#https://docs.abp.io/en/abp/latest/Authorization
#https://docs.microsoft.com/en-us/aspnet/core/security/authorization/introduction?view=aspnetcore-5.0


#https://docs.abp.io/en/abp/latest/Data-Transfer-Objects


