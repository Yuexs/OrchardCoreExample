using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrchardCore.Security.Permissions;

namespace OrchardCoreExample.DataTable.Module
{
    public class Permissions: IPermissionProvider
    {
        public static readonly Permission BookAccess = new Permission("BookAccess", "BookAccess");
        public Task<IEnumerable<Permission>> GetPermissionsAsync()
        {
            return Task.FromResult(new[] { BookAccess }.AsEnumerable());
        }

        public IEnumerable<PermissionStereotype> GetDefaultStereotypes()
        {
            return new[]
            {
                new PermissionStereotype
                {
                    Name = "Authenticated",
                    Permissions = new[] { BookAccess }
                }
            };
        }
    }
}
