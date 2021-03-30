using System;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Authors
{
    /// <summary>
    /// Data annotation attributes can be used to validate the DTO
    /// </summary>
    public class CreateAuthorDto
    {
        [Required]
        [StringLength(AuthorConsts.MaxNameLength)]
        public string Name { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        public string ShortBio { get; set; }
    }
}