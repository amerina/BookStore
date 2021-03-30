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
    /// ���������� BookStorePageModel ����Ĭ�ϵ� PageModel. 
    /// BookStorePageModel �̳��� PageModel ���������һЩ���Ա����page model��ʹ�õ�ͨ�����Ժͷ���.
    /// </summary>
    public class CreateModalModel : BookStorePageModel
    {
        /// <summary>
        /// [BindProperty] ���Խ�post�����ύ���������ݰ󶨵���������.
        /// </summary>
        [BindProperty]
        public CreateUpdateBookDto Book { get; set; }

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
