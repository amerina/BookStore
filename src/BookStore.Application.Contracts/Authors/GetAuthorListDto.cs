using Volo.Abp.Application.Dtos;

namespace BookStore.Authors
{
    /// <summary>
    /// Filter is used to search authors. It can be null (or empty string) to get all the authors.
    /// 
    /// PagedAndSortedResultRequestDto has the standard paging and sorting properties: 
    /// int MaxResultCount, int SkipCount and string Sorting.
    /// </summary>
    public class GetAuthorListDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}