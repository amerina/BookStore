namespace BookStore
{
    public static class BookStoreDomainErrorCodes
    {
        /* You can add your business exception error codes here, as constants */

        /// <summary>
        /// This is a unique string represents the error code thrown by your application and can be handled by client applications. 
        /// For users, you probably want to localize it. 
        /// Open the Localization/BookStore/en.json inside the BookStore.Domain.Shared project and add the following entry:
        /// "BookStore:00001": "There is already an author with the same name: {name}"
        /// Whenever you throw an AuthorAlreadyExistsException, the end user will see a nice error message on the UI.
        /// </summary>
        public const string AuthorAlreadyExists = "BookStore:00001";
    }
}
