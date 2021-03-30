using BookStore.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace BookStore.Authors
{
    /// <summary>
    /// https://docs.abp.io/en/abp/latest/Entity-Framework-Core
    /// Inherited from the EfCoreRepository, so it inherits the standard repository method implementations.
    /// 
    /// WhereIf is a shortcut extension method of the ABP Framework. 
    /// It adds the Where condition only if the first condition meets (it filters by name, only if the filter was provided). 
    /// You could do the same yourself, but these type of shortcut methods makes our life easier.
    /// 
    /// sorting can be a string like Name, Name ASC or Name DESC. 
    /// It is possible by using the System.Linq.Dynamic.Core NuGet package.
    /// </summary>
    public class EfCoreAuthorRepository: EfCoreRepository<BookStoreDbContext, Author, Guid>,IAuthorRepository
    {
        public EfCoreAuthorRepository(IDbContextProvider<BookStoreDbContext> dbContextProvider): base(dbContextProvider)
        {

        }

        public async Task<Author> FindByNameAsync(string name)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.FirstOrDefaultAsync(author => author.Name == name);
        }

        public async Task<List<Author>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string filter = null)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet
                .WhereIf(
                    !filter.IsNullOrWhiteSpace(),
                    author => author.Name.Contains(filter)
                 )
                .OrderBy(sorting)
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();
        }
    }
}
