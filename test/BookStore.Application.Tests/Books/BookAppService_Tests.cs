using System;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Authors;
using Shouldly;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Validation;
using Xunit;

namespace BookStore.Books
{
    public class BookAppService_Tests : BookStoreApplicationTestBase
    {
        private readonly IBookAppService _bookAppService;
        private readonly IAuthorAppService _authorAppService;
        public BookAppService_Tests()
        {
            _bookAppService = GetRequiredService<IBookAppService>();
            _authorAppService = GetRequiredService<IAuthorAppService>();
        }

        /// <summary>
        /// 直接使用 BookAppService.GetListAsync 方法来获取用户列表,并执行检查.
        /// 
        /// 我们可以安全地检查 "1984" 这本书的名称,因为我们知道这本书可以在数据库中找到,我们已将其添加到种子数据中.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Should_Get_List_Of_Books()
        {
            //Act
            var result = await _bookAppService.GetListAsync(
                new PagedAndSortedResultRequestDto()
            );

            //Assert
            result.TotalCount.ShouldBeGreaterThan(0);
            result.Items.ShouldContain(b => b.Name == "1984" &&
                                       b.AuthorName == "George Orwell");
        }

        /// <summary>
        /// 测试创建一个合法book实体的场景:
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Should_Create_A_Valid_Book()
        {
            var authors = await _authorAppService.GetListAsync(new GetAuthorListDto());
            var firstAuthor = authors.Items.First();

            //Act
            var result = await _bookAppService.CreateAsync(
                new CreateUpdateBookDto
                {
                    AuthorId = firstAuthor.Id,
                    Name = "New test book 42",
                    Price = 10,
                    PublishDate = System.DateTime.Now,
                    Type = BookType.ScienceFiction
                }
            );

            //Assert
            result.Id.ShouldNotBe(Guid.Empty);
            result.Name.ShouldBe("New test book 42");
        }

        /// <summary>
        /// 测试创建一个非法book实体失败的场景:
        /// 
        /// 由于 Name 是空值, ABP 抛出一个 AbpValidationException 异常.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Should_Not_Create_A_Book_Without_Name()
        {
            var exception = await Assert.ThrowsAsync<AbpValidationException>(async () =>
            {
                await _bookAppService.CreateAsync(
                    new CreateUpdateBookDto
                    {
                        Name = "",
                        Price = 10,
                        PublishDate = DateTime.Now,
                        Type = BookType.ScienceFiction
                    }
                );
            });

            exception.ValidationErrors
                .ShouldContain(err => err.MemberNames.Any(m => m == "Name"));
        }
    }
}
