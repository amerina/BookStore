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
    public class EditModalModel : BookStorePageModel
    {
        ///// <summary>
        ///// [HiddenInput] 和 [BindProperty] 是标准的 ASP.NET Core MVC 特性.
        ///// 这里启用 SupportsGet 从Http请求的查询字符串中获取Id的值.
        ///// </summary>
        //[HiddenInput]
        //[BindProperty(SupportsGet = true)]
        //public Guid Id { get; set; }

        //[BindProperty]
        //public CreateUpdateBookDto Book { get; set; }

        [BindProperty]
        public EditBookViewModel Book { get; set; }

        public List<SelectListItem> Authors { get; set; }


        private readonly IBookAppService _bookAppService;

        public EditModalModel(IBookAppService bookAppService)
        {
            _bookAppService = bookAppService;
        }

        /// <summary>
        /// 将 BookAppService.GetAsync 方法返回的 BookDto 映射成 CreateUpdateBookDto 并赋值给Book属性
        /// </summary>
        /// <returns></returns>
        public async Task OnGetAsync(Guid id)
        {
            var bookDto = await _bookAppService.GetAsync(id);
            Book = ObjectMapper.Map<BookDto, EditBookViewModel>(bookDto);

            var authorLookup = await _bookAppService.GetAuthorLookupAsync();
            Authors = authorLookup.Items
                .Select(x => new SelectListItem(x.Name, x.Id.ToString()))
                .ToList();
        }

        /// <summary>
        /// 直接使用 BookAppService.UpdateAsync 来更新实体
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> OnPostAsync()
        {
            await _bookAppService.UpdateAsync(
                Book.Id,
                ObjectMapper.Map<EditBookViewModel, CreateUpdateBookDto>(Book)
            );

            return NoContent();
        }

        public class EditBookViewModel
        {
            [HiddenInput]
            public Guid Id { get; set; }

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
