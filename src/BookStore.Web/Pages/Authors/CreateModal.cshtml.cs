using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Authors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace BookStore.Web.Pages.Authors
{
    public class CreateModalModel : BookStorePageModel
    {
        [BindProperty]
        public CreateAuthorViewModel Author { get; set; }

        private readonly IAuthorAppService _authorAppService;

        public CreateModalModel(IAuthorAppService authorAppService)
        {
            _authorAppService = authorAppService;
        }

        public void OnGet()
        {
            Author = new CreateAuthorViewModel();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var dto = ObjectMapper.Map<CreateAuthorViewModel, CreateAuthorDto>(Author);
            await _authorAppService.CreateAsync(dto);
            return NoContent();
        }

        /// <summary>
        /// CreateAuthorViewModel, for the view model instead of re-using the CreateAuthorDto
        /// 
        /// The main reason of this decision was to show you how to use a different model class inside the page. 
        /// But there is one more benefit: We added two attributes to the class members, which were not present in the CreateAuthorDto:
        /// Added [DataType(DataType.Date)] attribute to the BirthDate which shows a date picker on the UI for this property.
        /// Added [TextArea] attribute to the ShortBio which shows a multi-line text area instead of a standard textbox.
        /// 
        /// In this way, you can specialize the view model class based on your UI requirements without touching to the DTO. 
        /// As a result of this decision, we have used ObjectMapper to map CreateAuthorViewModel to CreateAuthorDto. 
        /// </summary>
        public class CreateAuthorViewModel
        {
            [Required]
            [StringLength(AuthorConsts.MaxNameLength)]
            public string Name { get; set; }

            [Required]
            [DataType(DataType.Date)]
            public DateTime BirthDate { get; set; }

            [TextArea]
            public string ShortBio { get; set; }
        }
    }
}
