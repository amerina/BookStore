using BookStore.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace BookStore.Permissions
{
    public class BookStorePermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            //var myGroup = context.AddGroup(BookStorePermissions.GroupName);

            //Define your own permissions here. Example:
            //myGroup.AddPermission(BookStorePermissions.MyPermission1, L("Permission:MyPermission1"));

            //This class defines a permission group (to group permissions on the UI, will be seen below) and 4 permissions inside this group. 
            //Also, Create, Edit and Delete are children of the BookStorePermissions.Books.Default permission. 
            //A child permission can be selected only if the parent was selected.
            var bookStoreGroup = context.AddGroup(BookStorePermissions.GroupName, L("Permission:BookStore"));

            var booksPermission = bookStoreGroup.AddPermission(BookStorePermissions.Books.Default, L("Permission:Books"));
            booksPermission.AddChild(BookStorePermissions.Books.Create, L("Permission:Books.Create"));
            booksPermission.AddChild(BookStorePermissions.Books.Edit, L("Permission:Books.Edit"));
            booksPermission.AddChild(BookStorePermissions.Books.Delete, L("Permission:Books.Delete"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<BookStoreResource>(name);
        }
    }
}
