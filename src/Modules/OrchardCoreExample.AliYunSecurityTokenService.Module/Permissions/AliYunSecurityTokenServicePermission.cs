using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrchardCore.Security.Permissions;

namespace OrchardCoreExample.AliYunSecurityTokenService.Module.Permissions
{
    public class AliYunSecurityTokenServicePermission : IPermissionProvider
    {
        public static readonly Permission AliYunSecurityTokenServiceAccess = new Permission("AliYunSecurityTokenServicePermission", "AliYunSecurityTokenServicePermission");

        public IEnumerable<PermissionStereotype> GetDefaultStereotypes()
        {
            return new[]
            {
                new PermissionStereotype
                {
                    Name = "Authenticated",
                    Permissions = new[] { AliYunSecurityTokenServiceAccess }
                }
            };
        }

        public Task<IEnumerable<Permission>> GetPermissionsAsync()
        {
            return Task.FromResult(new[] { AliYunSecurityTokenServiceAccess }.AsEnumerable());
        }
    }
}
