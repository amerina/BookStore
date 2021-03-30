using BookStore.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace BookStore.Authors
{
    /// <summary>
    /// https://docs.abp.io/en/abp/latest/Authorization
    /// [Authorize(BookStorePermissions.Authors.Default)] is a declarative way to check a permission (policy) to authorize the current user.
    /// 
    /// Derived from the BookStoreAppService, which is a simple base class comes with the startup template. 
    /// It is derived from the standard ApplicationService class.
    /// 
    /// Implemented the IAuthorAppService which was defined above.
    /// 
    /// Injected the IAuthorRepository and AuthorManager to use in the service methods.
    /// 
    /// DDD tip: Some developers may find useful to insert the new entity inside the _authorManager.CreateAsync. 
    /// We think it is a better design to leave it to the application layer since it better knows when to insert it to the database 
    /// (maybe it requires additional works on the entity before insert, which would require to an additional update if we perform the insert in the domain service). 
    /// However, it is completely up to you.
    /// </summary>
    [Authorize(BookStorePermissions.Authors.Default)]
    public class AuthorAppService : BookStoreAppService, IAuthorAppService
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly AuthorManager _authorManager;

        public AuthorAppService(
            IAuthorRepository authorRepository,
            AuthorManager authorManager)
        {
            _authorRepository = authorRepository;
            _authorManager = authorManager;
        }

        /// <summary>
        /// This method simply gets the Author entity by its Id, converts to the AuthorDto using the object to object mapper. 
        /// This requires to configure the AutoMapper
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<AuthorDto> GetAsync(Guid id)
        {
            var author = await _authorRepository.GetAsync(id);
            return ObjectMapper.Map<Author, AuthorDto>(author);
        }

        /// <summary>
        /// it actually was not needed to create such a method since we could directly query over the repository, 
        /// but wanted to demonstrate how to create custom repository methods.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<AuthorDto>> GetListAsync(GetAuthorListDto input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(Author.Name);
            }

            var authors = await _authorRepository.GetListAsync(
                input.SkipCount,
                input.MaxResultCount,
                input.Sorting,
                input.Filter
            );

            var totalCount = input.Filter == null
                ? await _authorRepository.CountAsync()
                : await _authorRepository.CountAsync(
                    author => author.Name.Contains(input.Filter));

            return new PagedResultDto<AuthorDto>(
                totalCount,
                ObjectMapper.Map<List<Author>, List<AuthorDto>>(authors)
            );
        }

        [Authorize(BookStorePermissions.Authors.Create)]
        public async Task<AuthorDto> CreateAsync(CreateAuthorDto input)
        {
            var author = await _authorManager.CreateAsync(
                input.Name,
                input.BirthDate,
                input.ShortBio
            );

            await _authorRepository.InsertAsync(author);

            return ObjectMapper.Map<Author, AuthorDto>(author);
        }


        /// <summary>
        /// Used the IAuthorRepository.GetAsync to get the author entity from the database. 
        /// GetAsync throws EntityNotFoundException if there is no author with the given id, which results a 404 HTTP status code in a web application. 
        /// It is a good practice to always bring the entity on an update operation.
        /// 
        /// Used the AuthorManager.ChangeNameAsync (domain service method) to change the author name if it was requested to change by the client.
        /// 
        /// Directly updated the BirthDate and ShortBio since there is not any business rule to change these properties, they accept any value.
        /// 
        /// EF Core Tip: Entity Framework Core has a change tracking system and automatically saves any change to an entity at the end of the unit of work 
        /// (You can simply think that the ABP Framework automatically calls SaveChanges at the end of the method). 
        /// So, it will work as expected even if you don't call the _authorRepository.UpdateAsync(...) in the end of the method. 
        /// If you don't consider to change the EF Core later, you can just remove this line.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize(BookStorePermissions.Authors.Edit)]
        public async Task UpdateAsync(Guid id, UpdateAuthorDto input)
        {
            var author = await _authorRepository.GetAsync(id);

            if (author.Name != input.Name)
            {
                await _authorManager.ChangeNameAsync(author, input.Name);
            }

            author.BirthDate = input.BirthDate;
            author.ShortBio = input.ShortBio;

            await _authorRepository.UpdateAsync(author);
        }

        [Authorize(BookStorePermissions.Authors.Delete)]
        public async Task DeleteAsync(Guid id)
        {
            await _authorRepository.DeleteAsync(id);
        }
    }
}
