
#项目启动
#https://docs.abp.io/zh-Hans/abp/latest/Getting-Started?UI=MVC&DB=EF&Tiered=No

#1.Update-Database
#2.Set BookStore.DbMigrator as 启动项目,运行项目
#3.Set BookStore.Web as 启动项目
#登录admin 密码1q2w3E*




#服务端创建以后创建前端页面




#JS动态代理
#https://docs.abp.io/en/abp/latest/UI/AspNetCore/Dynamic-JavaScript-Proxies

通常在 JavaScript 端通过AJAX调用HTTP API端点. 你可以使用 $.ajax 或其他工具来调用端点. 但是ABP提供了更好的方法.
ABP动态为所有API端点创建 JavaScript代理. 所以你可以像调用Javascript本地方法一样使用任何端点
bookStore.books.book.getList({}).done(function (result) { console.log(result); });
	bookStore.books 是BookAppService的命令空间转换成小驼峰形式.
	book是 BookAppService 的约定名称(删除AppService后缀并且转换为小驼峰).
	getList是 CrudAppService 基类定义的 GetListAsync 方法的约定名称(删除Async后缀并且转换为小驼峰).
	{} 参数将空对象发送到 GetListAsync 方法,该方法通常需要一个类型为 PagedAndSortedResultRequestDto 的对象,该对象用于将分页和排序选项发送到服务器(所有属性都是可选的,具有默认值. 因此你可以发送一个空对象).
	getList 函数返回一个 promise. 你可以传递一个回调到 then(或done)函数来获取从服务器返回的结果.

创建:
bookStore.books.book.create({ 
        name: 'Foundation', 
        type: 7, 
        publishDate: '1951-05-24', 
        price: 21.5 
    }).then(function (result) { 
        console.log('successfully created the book with id: ' + result.id); 
    });
尝试使用 get, update 和 delete 函数.
#利用这些动态代理功能与服务器通信.
