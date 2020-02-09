using OrchardCore.Security.Permissions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrchardCoreExample.WebApi.Module.Permissions
{
    public class WebApiPermission : IPermissionProvider
    {
        public static readonly Permission WebApiPermissionAccess = new Permission("WebApiPermissionAccess", "WebApi权限");

        public IEnumerable<PermissionStereotype> GetDefaultStereotypes()
        {
            return new[]
            {
                new PermissionStereotype
                {
                    Name = "Authenticated",
                    Permissions = new[] { WebApiPermissionAccess }
                }
            };
        }

        public Task<IEnumerable<Permission>> GetPermissionsAsync()
        {
            return Task.FromResult(new[] { WebApiPermissionAccess }.AsEnumerable());
        }
    }
}
