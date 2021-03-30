
#实体
#https://docs.abp.io/zh-Hans/abp/latest/Entities
#https://docs.abp.io/zh-Hans/abp/latest/Best-Practices/Entities
#https://www.dddcommunity.org/library/vernon_2011/

Guid主键的实体
如果你的实体Id类型为 Guid,有一些好的实践可以实现:
创建一个构造函数,获取ID作为参数传递给基类.
	*如果没有为GUID Id斌值,ABP框架会在保存时设置它,但是在将实体保存到数据库之前最好在实体上有一个有效的Id.
如果使用带参数的构造函数创建实体,那么还要创建一个 private 或 protected 构造函数. 当数据库提供程序从数据库读取你的实体时(反序列化时)将使用它.
不要使用 Guid.NewGuid() 来设置Id! 在创建实体的代码中**使用IGuidGenerator服务**传递Id参数. IGuidGenerator经过优化可以产生连续的GUID.这对于关系数据库中的聚集索引非常重要.

#Guid生成
#https://docs.abp.io/zh-Hans/abp/latest/Guid-Generation

#最佳实践
主构造函数
	推荐 定义一个 主构造函数 确保实体在创建时的有效性, 在代码中通过主构造函数创建实体的新实例.
	推荐 根据需求把主构造函数定义为 public,internal 或 protected internal . 如果它不是public的, 那么应该由领域服务来创建实体.
	推荐 总是在主构造函数中初始化子集合.
	不推荐 在主构造函数中生成 Guid 键, 应该将其做为参数获取, 在调用时推荐使用 IGuidGenerator 生成新的 Guid 值做为参数.
无参构造函数
	推荐 总是定义 protected 无参构造函数与ORM兼容.
引用
	推荐 总是通过 id 引用 其他聚合根, 不要将导航属性添加到其他聚合根中.
类的其他成员
	推荐 总是将属性与方法定义为 virtual (除了私有方法 ). 因为有些ORM和动态代理工具需要.
	推荐 保持实体在自身边界内始终 有效 和 一致.
	推荐 使用 private,protected,internal或protected internal setter定义属性, 保护实体的一致性和有效性.
	推荐 定义 public, internal 或 protected internal (virtual)方法在必要时更改属性值(使用非public setters时).

#聚合根--最佳实践
主键
	推荐 总是使用 Id 属性做为聚合根主键.
	不推荐 在聚合根中使用 复合主键.
	推荐 所有的聚合根都使用 Guid 类型 主键.
聚合边界
	推荐 聚合尽可能小. 大多数聚合只有原始属性, 不会有子集合. 把这些视为设计决策:
		*加载和保存聚合的 性能 与 内存 成本 (请记住,聚合通常是做为一个单独的单元被加载和保存的). 较大的聚合会消耗更多的CPU和内存.
	一致性 & 有效性 边界.


#聚合根
聚合根需要维护自身的完整性,所有的实体也是这样.但是聚合根也要维护子实体的完整性.所以,聚合根必须一直有效.
使用Id引用聚合根,而不使用导航属性
聚合根被视为一个单元.它是作为一个单元检索和更新的.它通常被认为是一个交易边界.
不单独修改聚合根中的子实体


#Entity Framework Core 集成
#https://docs.abp.io/zh-Hans/abp/latest/Entity-Framework-Core



#验证
#https://docs.abp.io/zh-Hans/abp/latest/Validation
#https://docs.microsoft.com/zh-cn/aspnet/core/mvc/models/validation?view=aspnetcore-5.0

ABP增加了以下优点:
	定义 IValidationEnabled 向任意类添加自动验证. 所有的应用服务都实现了该接口,所以它们会被自动验证.
	自动将数据注解属性的验证错误信息本地化.
	提供可扩展的服务来验证方法调用或对象的状态.
	提供FluentValidation的集成.



#仓储
#https://docs.abp.io/zh-Hans/abp/latest/Repositories
"在领域层和数据映射层之间进行中介,使用类似集合的接口来操作领域对象." (Martin Fowler).
实际上,仓储用于领域对象在数据库中的操作, 通常每个 聚合根 或不同的实体创建对应的仓储.


#对象映射
#https://docs.abp.io/zh-Hans/abp/latest/Object-To-Object-Mapping






