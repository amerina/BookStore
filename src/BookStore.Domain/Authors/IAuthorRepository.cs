using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace BookStore.Authors
{
    /// <summary>
    /// IAuthorRepository extends the standard IRepository<Author, Guid> interface, so all the standard repository methods will also be available for the IAuthorRepository.
    /// 
    /// FindByNameAsync was used in the AuthorManager to query an author by name. 
    /// 
    /// GetListAsync will be used in the application layer to get a listed, sorted and filtered list of authors to show on the UI. 
    /// </summary>
    public interface IAuthorRepository : IRepository<Author, Guid>
    {
        Task<Author> FindByNameAsync(string name);

        Task<List<Author>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string filter = null
        );
    }
}
