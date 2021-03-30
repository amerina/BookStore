using AutoMapper;
using BookStore.Books;

namespace BookStore.Web
{
    public class BookStoreWebAutoMapperProfile : Profile
    {
        public BookStoreWebAutoMapperProfile()
        {
            //Define your AutoMapper configuration here for the Web project.

            //请注意,我们在Web层中进行映射定义是一种最佳实践,因为仅在该层中需要它.
            CreateMap<BookDto, CreateUpdateBookDto>();
        }
    }
}
