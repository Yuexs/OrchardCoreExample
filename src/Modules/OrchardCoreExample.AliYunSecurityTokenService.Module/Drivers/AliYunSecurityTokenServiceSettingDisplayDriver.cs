using System.Threading.Tasks;
using OrchardCoreExample.AliYunSecurityTokenService.Module.Permissions;
using OrchardCoreExample.AliYunSecurityTokenService.Module.Settings;
using OrchardCoreExample.AliYunSecurityTokenService.Module.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using OrchardCore.DisplayManagement.Entities;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.Environment.Shell;
using OrchardCore.Settings;

namespace OrchardCoreExample.AliYunSecurityTokenService.Module.Drivers
{
    public class AliYunSecurityTokenServiceSettingDisplayDriver : SectionDisplayDriver<ISite, AliYunSecurityTokenServiceSetting>
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IHttpContextAccessor _hca;
        private readonly IShellHost _shellHost;
        private readonly ShellSettings _shellSettings;

        public AliYunSecurityTokenServiceSettingDisplayDriver(IAuthorizationService authorizationService, IHttpContextAccessor hca,
            IShellHost shellHost,
            ShellSettings shellSettings)
        {
            _authorizationService = authorizationService;
            _hca = hca;
            _shellHost = shellHost;
            _shellSettings = shellSettings;
        }

        public override async Task<IDisplayResult> EditAsync(AliYunSecurityTokenServiceSetting settings, BuildEditorContext context)
        {
            if (!await IsAuthorizedToManageDemoSettingsAsync())
            {
                return null;
            }

            return Initialize<AliYunSecurityTokenServiceSettingViewModel>("AliYunSecurityTokenServiceSetting_Edit", model =>
                {
                    model.AccessKeyId = settings.AccessKeyId;
                    model.AccessKeySecret = settings.AccessKeySecret;
                    model.BucketName = settings.BucketName;
                    model.RegionName = settings.RegionName;
                    model.RoleArn = settings.RoleArn;
                    model.Policy = settings.Policy;
                })
                .Location("Content:6")
                .OnGroup(Features.AliYunSecurityTokenServiceAuthentication);
        }

        public override async Task<IDisplayResult> UpdateAsync(AliYunSecurityTokenServiceSetting settings, BuildEditorContext context)
        {
            if (context.GroupId == Features.AliYunSecurityTokenServiceAuthentication)
            {
                if (!await IsAuthorizedToManageDemoSettingsAsync())
                {
                    return null;
                }
                var model = new AliYunSecurityTokenServiceSettingViewModel();
                await context.Updater.TryUpdateModelAsync(model, Prefix);
                if (context.Updater.ModelState.IsValid)
                {
                    settings.AccessKeyId = model.AccessKeyId;
                    settings.AccessKeySecret = model.AccessKeySecret;
                    settings.BucketName = model.BucketName;
                    settings.RegionName = model.RegionName;
                    settings.RoleArn = model.RoleArn;
                    settings.Policy = model.Policy;
                    await _shellHost.ReloadShellContextAsync(_shellSettings);
                }
            }
            return await EditAsync(settings, context);
        }

        private async Task<bool> IsAuthorizedToManageDemoSettingsAsync()
        {
            var user = _hca.HttpContext?.User;
            return user != null &&
                   await _authorizationService.AuthorizeAsync(user, AliYunSecurityTokenServicePermission.AliYunSecurityTokenServiceAccess);
        }
    }
}
