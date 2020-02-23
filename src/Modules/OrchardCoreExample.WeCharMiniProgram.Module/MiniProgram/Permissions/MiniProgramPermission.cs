using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrchardCore.Security.Permissions;

namespace OrchardCoreExample.WeCharMiniProgram.Module.MiniProgram.Permissions
{
    public class MiniProgramPermission: IPermissionProvider
    {
        public static readonly Permission WeCharMiniProgramAccess =
            new Permission("WeCharMiniProgramAccess", "微信小程序接口权限");

        public Task<IEnumerable<Permission>> GetPermissionsAsync()
        {
            return Task.FromResult(new[] { WeCharMiniProgramAccess }.AsEnumerable());
        }

        public IEnumerable<PermissionStereotype> GetDefaultStereotypes()
        {
            return new[]
            {
                new PermissionStereotype
                {
                    Name = "WeCharMiniProgramAccess",
                    Permissions = new[] { WeCharMiniProgramAccess }
                }
            };
        }
    }
}
