using BookStore.Authors;
using BookStore.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace BookStore.Books
{
    /// <summary>
    /// Added [Authorize(BookStorePermissions.Books.Default)] to authorize the methods 
    /// we've newly added/overrode (remember, authorize attribute is valid for all the methods of the class when it is declared for a class).
    /// </summary>
    [Authorize(BookStorePermissions.Books.Default)]
    public class BookAppService : CrudAppService<
            Book, //The Book entity
            BookDto, //Used to show books
            Guid, //Primary key of the book entity
            PagedAndSortedResultRequestDto, //Used for paging/sorting
            CreateUpdateBookDto>, //Used to create/update a book
            IBookAppService //implement the IBookAppService
    {
        private readonly IAuthorRepository _authorRepository;
        public BookAppService(IRepository<Book, Guid> repository, IAuthorRepository authorRepository) : base(repository)
        {
            _authorRepository = authorRepository;
            GetPolicyName = BookStorePermissions.Books.Default;
            GetListPolicyName = BookStorePermissions.Books.Default;
            CreatePolicyName = BookStorePermissions.Books.Create;
            UpdatePolicyName = BookStorePermissions.Books.Edit;
            DeletePolicyName = BookStorePermissions.Books.Delete;
        }

        public override async Task<BookDto> GetAsync(Guid id)
        {
            //Get the IQueryable<Book> from the repository
            var queryable = await Repository.GetQueryableAsync();

            //Prepare a query to join books and authors
            var query = from book in queryable
                        join author in _authorRepository on book.AuthorId equals author.Id
                        where book.Id == id
                        select new { book, author };

            //Execute the query and get the book with author
            var queryResult = await AsyncExecuter.FirstOrDefaultAsync(query);
            if (queryResult == null)
            {
                throw new EntityNotFoundException(typeof(Book), id);
            }

            var bookDto = ObjectMapper.Map<Book, BookDto>(queryResult.book);
            bookDto.AuthorName = queryResult.author.Name;
            return bookDto;
        }

        /// <summary>
        /// Overrode the GetListAsync method of the base CrudAppService, which returns a list of books
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override async Task<PagedResultDto<BookDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            //Set a default sorting, if not provided
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(Book.Name);
            }

            //Get the IQueryable<Book> from the repository
            var queryable = await Repository.GetQueryableAsync();

            //Prepare a query to join books and authors
            var query = from book in queryable
                        join author in _authorRepository on book.AuthorId equals author.Id
                        orderby input.Sorting //TODO: Can not sort like that!
                        select new { book, author };

            //Paging
            query = query
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount);

            //Execute the query and get a list
            var queryResult = await AsyncExecuter.ToListAsync(query);

            //Convert the query result to a list of BookDto objects
            var bookDtos = queryResult.Select(x =>
            {
                var bookDto = ObjectMapper.Map<Book, BookDto>(x.book);
                bookDto.AuthorName = x.author.Name;
                return bookDto;
            }).ToList();

            //Get the total count with another query
            var totalCount = await Repository.GetCountAsync();

            return new PagedResultDto<BookDto>(
                totalCount,
                bookDtos
            );
        }

        public async Task<ListResultDto<AuthorLookupDto>> GetAuthorLookupAsync()
        {
            var authors = await _authorRepository.GetListAsync();

            return new ListResultDto<AuthorLookupDto>(
                ObjectMapper.Map<List<Author>, List<AuthorLookupDto>>(authors)
            );
        }
    }
}
