using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Books;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookStore.Web.Pages.Books
{
    public class EditModalModel : BookStorePageModel
    {
        /// <summary>
        /// [HiddenInput] �� [BindProperty] �Ǳ�׼�� ASP.NET Core MVC ����.
        /// �������� SupportsGet ��Http����Ĳ�ѯ�ַ����л�ȡId��ֵ.
        /// </summary>
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public CreateUpdateBookDto Book { get; set; }

        private readonly IBookAppService _bookAppService;

        public EditModalModel(IBookAppService bookAppService)
        {
            _bookAppService = bookAppService;
        }

        /// <summary>
        /// �� BookAppService.GetAsync �������ص� BookDto ӳ��� CreateUpdateBookDto ����ֵ��Book����
        /// </summary>
        /// <returns></returns>
        public async Task OnGetAsync()
        {
            var bookDto = await _bookAppService.GetAsync(Id);
            Book = ObjectMapper.Map<BookDto, CreateUpdateBookDto>(bookDto);
        }

        /// <summary>
        /// ֱ��ʹ�� BookAppService.UpdateAsync ������ʵ��
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> OnPostAsync()
        {
            await _bookAppService.UpdateAsync(Id, Book);
            return NoContent();
        }
    }
}
