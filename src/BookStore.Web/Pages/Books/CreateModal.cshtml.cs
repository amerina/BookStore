using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Books;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookStore.Web.Pages.Books
{
    /// <summary>
    /// 该类派生于 BookStorePageModel 而非默认的 PageModel. 
    /// BookStorePageModel 继承了 PageModel 并且添加了一些可以被你的page model类使用的通用属性和方法.
    /// </summary>
    public class CreateModalModel : BookStorePageModel
    {
        /// <summary>
        /// [BindProperty] 特性将post请求提交上来的数据绑定到该属性上.
        /// </summary>
        [BindProperty]
        public CreateUpdateBookDto Book { get; set; }

        private readonly IBookAppService _bookAppService;

        public CreateModalModel(IBookAppService bookAppService)
        {
            _bookAppService = bookAppService;
        }

        /// <summary>
        /// 在 OnGet 方法中创建一个新的 CreateUpdateBookDto 对象。 
        /// ASP.NET Core不需要像这样创建一个新实例就可以正常工作. 
        /// 但是它不会为你创建实例,并且如果你的类在类构造函数中具有一些默认值分配或代码执行,它们将无法工作. 
        /// 对于这种情况,我们为某些 CreateUpdateBookDto 属性设置了默认值.
        /// </summary>
        public void OnGet()
        {
            Book = new CreateUpdateBookDto();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _bookAppService.CreateAsync(Book);
            return NoContent();
        }
    }
}
