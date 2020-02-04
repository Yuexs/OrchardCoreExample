using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using OrchardCore.DisplayManagement.Entities;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.Settings;
using OrchardCoreExample.Settings.Module.Permissions;
using OrchardCoreExample.Settings.Module.Settings;
using OrchardCoreExample.Settings.Module.ViewModels;

namespace OrchardCoreExample.Settings.Module.Drivers
{
    public class WeCharMiniProgramSettingDisplayDriver : SectionDisplayDriver<ISite, WeCharMiniProgramSetting>
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IHttpContextAccessor _hca;

        public WeCharMiniProgramSettingDisplayDriver(IAuthorizationService authorizationService, IHttpContextAccessor hca)
        {
            _authorizationService = authorizationService;
            _hca = hca;
        }

        public override async Task<IDisplayResult> EditAsync(WeCharMiniProgramSetting settings, BuildEditorContext context)
        {
            if (!await IsAuthorizedToManageDemoSettingsAsync())
            {
                return null;
            }

            return Initialize<WeCharMiniProgramSettingViewModel>("WeCharMiniProgramSetting_Edit", model =>
                {
                    model.WxOpenAppId = settings.WxOpenAppId;
                    model.WxOpenAppSecret = settings.WxOpenAppSecret;
                    model.WxOpenEncodingAesKey = settings.WxOpenEncodingAesKey;
                    model.WxOpenToken = settings.WxOpenToken;
                })
                .Location("Content:5")
                .OnGroup(Features.OrchardCoreExampleSettingsModule);
        }

        public override async Task<IDisplayResult> UpdateAsync(WeCharMiniProgramSetting settings, BuildEditorContext context)
        {
            if (context.GroupId == Features.OrchardCoreExampleSettingsModule)
            {
                if (!await IsAuthorizedToManageDemoSettingsAsync())
                {
                    return null;
                }
                var model = new WeCharMiniProgramSettingViewModel();
                await context.Updater.TryUpdateModelAsync(model, Prefix);
                settings.WxOpenAppId = model.WxOpenAppId;
                settings.WxOpenAppSecret = model.WxOpenAppSecret;
                settings.WxOpenEncodingAesKey = model.WxOpenEncodingAesKey;
                settings.WxOpenToken = model.WxOpenToken;
            }
            return await EditAsync(settings, context);
        }

        private async Task<bool> IsAuthorizedToManageDemoSettingsAsync()
        {
            var user = _hca.HttpContext?.User;
            return user != null && await _authorizationService.AuthorizeAsync(user, WeCharMiniProgramSettingPermission.WeCharMiniProgramAccess);
        }
    }
}
