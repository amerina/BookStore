using Volo.Abp;

namespace BookStore.Authors
{
    /// <summary>
    /// BusinessException is a special exception type. 
    /// It is a good practice to throw domain related exceptions when needed. 
    /// It is automatically handled by the ABP Framework and can be easily localized. 
    /// WithData(...) method is used to provide additional data to the exception object that will later be used on the localization message or for some other purpose.
    /// </summary>
    public class AuthorAlreadyExistsException : BusinessException
    {
        public AuthorAlreadyExistsException(string name): base(BookStoreDomainErrorCodes.AuthorAlreadyExists)
        {
            WithData("name", name);
        }
    }
}
