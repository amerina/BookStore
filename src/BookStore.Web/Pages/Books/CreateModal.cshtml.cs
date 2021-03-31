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
    /// ���������� BookStorePageModel ����Ĭ�ϵ� PageModel. 
    /// BookStorePageModel �̳��� PageModel ���������һЩ���Ա����page model��ʹ�õ�ͨ�����Ժͷ���.
    /// </summary>
    public class CreateModalModel : BookStorePageModel
    {
        /// <summary>
        /// [BindProperty] ���Խ�post�����ύ���������ݰ󶨵���������.
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
        /// �� OnGet �����д���һ���µ� CreateUpdateBookDto ���� 
        /// ASP.NET Core����Ҫ����������һ����ʵ���Ϳ�����������. 
        /// ����������Ϊ�㴴��ʵ��,���������������๹�캯���о���һЩĬ��ֵ��������ִ��,���ǽ��޷�����. 
        /// �����������,����ΪĳЩ CreateUpdateBookDto ����������Ĭ��ֵ.
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
