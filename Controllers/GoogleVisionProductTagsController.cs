using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;
using Nop.Services.Configuration;
using Nop.Services.Security;
using Nop.Services.Localization;
using Nop.Services.Messages;
using Nop.Plugin.Misc.GoogleVisionProductTags.Models;
using Nop.Plugin.Misc.GoogleVisionProductTags.Services;

namespace Nop.Plugin.Misc.GoogleVisionProductTags.Controllers
{
    public class GoogleVisionProductTagsController : BasePluginController
    {
        #region Fields

        private readonly GoogleVisionProductTagsSettings _googleVisionProductTagsSettings;
        private readonly IPermissionService _permissionService;
        private readonly ISettingService _settingService;
        private readonly ILocalizationService _localizationService;
        private readonly INotificationService _notificationService;
        private readonly IGoogleVisionProductTagsGenerator _googleVisionProductTagsGenerator;

        #endregion

        #region Ctor

        public GoogleVisionProductTagsController(GoogleVisionProductTagsSettings googleVisionProductTagsSettings,
            IPermissionService permissionService,
            ISettingService settingService,
            ILocalizationService localizationService,
            INotificationService notificationService,
            IGoogleVisionProductTagsGenerator googleVisionProductTagsGenerator)
        {
            _googleVisionProductTagsSettings = googleVisionProductTagsSettings;
            _permissionService = permissionService;
            _settingService = settingService;
            _localizationService = localizationService;
            _notificationService = notificationService;
            _googleVisionProductTagsGenerator = googleVisionProductTagsGenerator;
        }

        #endregion

        #region Methods

        [AuthorizeAdmin]
        [Area(AreaNames.Admin)]
        public IActionResult Configure()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
                return AccessDeniedView();

            var model = new ConfigurationModel()
            {
                GoogleApiKey = _googleVisionProductTagsSettings.GoogleApiKey,
                MaxTagsPerImage = _googleVisionProductTagsSettings.MaxTagsPerImage,
                MinConfidenceScore = _googleVisionProductTagsSettings.MinConfidenceScore,
                MinTopicality = _googleVisionProductTagsSettings.MinTopicality
            };

            return View("~/Plugins/Misc.GoogleVisionProductTags/Views/Configure.cshtml", model);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [AuthorizeAdmin]
        [Area(AreaNames.Admin)]
        public IActionResult Configure(ConfigurationModel model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
                return AccessDeniedView();

            if (!ModelState.IsValid)
                return Configure();

            // save settings
            _googleVisionProductTagsSettings.GoogleApiKey = model.GoogleApiKey;
            _googleVisionProductTagsSettings.MaxTagsPerImage = model.MaxTagsPerImage;
            _googleVisionProductTagsSettings.MinConfidenceScore = model.MinConfidenceScore;
            _googleVisionProductTagsSettings.MinTopicality = model.MinTopicality;
            _settingService.SaveSetting(_googleVisionProductTagsSettings);

            _notificationService.SuccessNotification(_localizationService.GetResource("Admin.Plugins.Saved"));

            return Configure();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [AuthorizeAdmin]
        [Area(AreaNames.Admin)]
        public async Task<IActionResult> GenerateTagsForAllProducts()
        {
            try
            {
                await _googleVisionProductTagsGenerator.GenerateTagsForAllProducts();
                _notificationService.SuccessNotification(_localizationService.GetResource("Plugins.Misc.GoogleVisionProductTags.TagsGenerated"));
            } catch (Exception exc)
            {
                _notificationService.ErrorNotification(exc);
            }
            return RedirectToAction("ProductTags", "Product");
        }

        #endregion
    }
}
