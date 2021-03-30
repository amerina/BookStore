using System;
using Volo.Abp.Application.Dtos;

namespace BookStore.Authors
{
    /// <summary>
    /// EntityDto<T> simply has an Id property with the given generic argument. 
    /// You could create an Id property yourself instead of inheriting the EntityDto<T>.
    /// </summary>
    public class AuthorDto : EntityDto<Guid>
    {
        public string Name { get; set; }

        public DateTime BirthDate { get; set; }

        public string ShortBio { get; set; }
    }
}