using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using OrchardCore.DisplayManagement.Entities;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.Environment.Shell;
using OrchardCore.Settings;
using OrchardCoreExample.WeCharMiniProgram.Module.MiniProgram.Models;
using OrchardCoreExample.WeCharMiniProgram.Module.MiniProgram.Permissions;
using OrchardCoreExample.WeCharMiniProgram.Module.MiniProgram.ViewModels;

namespace OrchardCoreExample.WeCharMiniProgram.Module.MiniProgram.Drivers
{
    public class MiniProgramSettingDisplayDriver : SectionDisplayDriver<ISite, MiniProgramSettingModel>
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IHttpContextAccessor _hca;
        private readonly IShellHost _shellHost;
        private readonly ShellSettings _shellSettings;

        public MiniProgramSettingDisplayDriver(IAuthorizationService authorizationService,
            IHttpContextAccessor hca, IShellHost shellHost, ShellSettings shellSettings)
        {
            _authorizationService = authorizationService;
            _hca = hca;
            _shellHost = shellHost;
            _shellSettings = shellSettings;
        }

        public override async Task<IDisplayResult> EditAsync(MiniProgramSettingModel settings,
            BuildEditorContext context)
        {
            if (!await IsAuthorizedToManageDemoSettingsAsync()) return null;

            return Initialize<MiniProgramSettingViewModel>("MiniProgramSetting_Edit", model =>
                {
                    model.WxOpenAppId = settings.WxOpenAppId;
                    model.WxOpenAppSecret = settings.WxOpenAppSecret;
                    model.WxOpenEncodingAesKey = settings.WxOpenEncodingAesKey;
                    model.WxOpenToken = settings.WxOpenToken;
                })
                .Location("Content:5")
                .OnGroup(Features.WeCharMiniProgram);
        }

        public override async Task<IDisplayResult> UpdateAsync(MiniProgramSettingModel settings,
            BuildEditorContext context)
        {
            if (context.GroupId == Features.WeCharMiniProgram)
            {
                if (!await IsAuthorizedToManageDemoSettingsAsync()) return null;
                var model = new MiniProgramSettingViewModel();
                await context.Updater.TryUpdateModelAsync(model, Prefix);
                if (context.Updater.ModelState.IsValid)
                {
                    settings.WxOpenAppId = model.WxOpenAppId;
                    settings.WxOpenAppSecret = model.WxOpenAppSecret;
                    settings.WxOpenEncodingAesKey = model.WxOpenEncodingAesKey;
                    settings.WxOpenToken = model.WxOpenToken;
                    // 设置参数后重新加载租户
                    await _shellHost.ReloadShellContextAsync(_shellSettings);
                }
            }

            return await EditAsync(settings, context);
        }

        private async Task<bool> IsAuthorizedToManageDemoSettingsAsync()
        {
            var user = _hca.HttpContext?.User;
            return user != null &&
                   await _authorizationService.AuthorizeAsync(user,
                       MiniProgramPermission.WeCharMiniProgramAccess);
        }
    }
}