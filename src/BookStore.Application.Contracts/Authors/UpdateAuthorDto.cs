using System;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Authors
{
    /// <summary>
    /// We could share (re-use) the same DTO among the create and the update operations. 
    /// While you can do it, we prefer to create different DTOs for these operations since we see they generally be different by the time. 
    /// So, code duplication is reasonable here compared to a tightly coupled design.
    /// </summary>
    public class UpdateAuthorDto
    {
        [Required]
        [StringLength(AuthorConsts.MaxNameLength)]
        public string Name { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        public string ShortBio { get; set; }
    }
}