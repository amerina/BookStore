using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Books;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

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
        /// 
        /// Changed type of the Book property from CreateUpdateBookDto to the new CreateBookViewModel class defined in this file. 
        /// The main motivation of this change to customize the model class based on the User Interface (UI) requirements. 
        /// We didn't want to use UI-related [SelectItems(nameof(Authors))] and [DisplayName("Author")] attributes inside the CreateUpdateBookDto class.
        /// </summary>
        [BindProperty]
        public CreateBookViewModel Book { get; set; }

        public List<SelectListItem> Authors { get; set; }

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
        public async Task OnGetAsync()
        {
            Book = new CreateBookViewModel();

            var authorLookup = await _bookAppService.GetAuthorLookupAsync();
            Authors = authorLookup.Items
                .Select(x => new SelectListItem(x.Name, x.Id.ToString()))
                .ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _bookAppService.CreateAsync(ObjectMapper.Map<CreateBookViewModel, CreateUpdateBookDto>(Book));
            return NoContent();
        }

        public class CreateBookViewModel
        {
            [SelectItems(nameof(Authors))]
            [DisplayName("Author")]
            public Guid AuthorId { get; set; }

            [Required]
            [StringLength(128)]
            public string Name { get; set; }

            [Required]
            public BookType Type { get; set; } = BookType.Undefined;

            [Required]
            [DataType(DataType.Date)]
            public DateTime PublishDate { get; set; } = DateTime.Now;

            [Required]
            public float Price { get; set; }
        }
    }
}
