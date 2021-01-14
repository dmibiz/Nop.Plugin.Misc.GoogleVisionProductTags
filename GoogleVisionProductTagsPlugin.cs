using System.Collections.Generic;
using Nop.Core;
using Nop.Services.Plugins;
using Nop.Services.Common;
using Nop.Services.Localization;
using Nop.Services.Cms;
using Nop.Web.Framework.Infrastructure;

namespace Nop.Plugin.Misc.GoogleVisionProductTags
{
    public class GoogleVisionProductTagsPlugin : BasePlugin, IMiscPlugin, IWidgetPlugin
    {
        #region Fields

        private readonly ILocalizationService _localizationService;
        private readonly IWebHelper _webHelper;

        #endregion

        #region Ctor

        public GoogleVisionProductTagsPlugin(ILocalizationService localizationService,
            IWebHelper webHelper)
        {
            _localizationService = localizationService;
            _webHelper = webHelper;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets a configuration page URL
        /// </summary>
        public override string GetConfigurationPageUrl()
        {
            return $"{_webHelper.GetStoreLocation()}Admin/GoogleVisionProductTags/Configure";
        }


        /// <summary>
        /// Install the plugin
        /// </summary>
        public override void Install()
        {
            //locales
            _localizationService.AddPluginLocaleResource(new Dictionary<string, string>
            {
                ["Plugins.Misc.GoogleVisionProductTags.GoogleApiKey"] = "Google API key",
                ["Plugins.Misc.GoogleVisionProductTags.MaxTagsPerImage"] = "Maximum tags per image",
                ["Plugins.Misc.GoogleVisionProductTags.MinConfidenceScore"] = "Minimal label confidence score",
                ["Plugins.Misc.GoogleVisionProductTags.MinTopicality"] = "Minimum label topicality",
                ["Plugins.Misc.GoogleVisionProductTags.MaxTagsPerImage.Hint"] = "Maximal amount of labels per API response for one image",
                ["Plugins.Misc.GoogleVisionProductTags.MinConfidenceScore.Hint"] = "Minimal label confidence score for a product tag to be created from it. " +
                "Ranges from 0 (no confidence) to 1 (very high confidence)",
                ["Plugins.Misc.GoogleVisionProductTags.MinTopicality.Hint"] = "Minimal label topicality for a product tag to be created from it. " +
                "Topicality is the relevancy of the ICA (Image Content Annotation) label to the image. It measures how important/central a label is to the overall context of a page.",
                ["Plugins.Misc.GoogleVisionProductTags.Cancel"] = "Cancel",
                ["Plugins.Misc.GoogleVisionProductTags.GenerateProductTags"] = "Generate product tags",
                ["Plugins.Misc.GoogleVisionProductTags.GenerateAllProductTagsWarning"] = "This will generate tags for all products from their images using Google Cloud Vision",
                ["Plugins.Misc.GoogleVisionProductTags.TagsGenerated"] = "Product tags have been generated successfully."
            });

            base.Install();
        }

        /// <summary>
        /// Uninstall the plugin
        /// </summary>
        public override void Uninstall()
        {
            //locales
            _localizationService.DeletePluginLocaleResources("Plugins.Misc.GoogleVisionProductTags");

            base.Uninstall();
        }

        public IList<string> GetWidgetZones()
        {
            return new List<string> { AdminWidgetZones.ProductTagListButtons };
        }

        public string GetWidgetViewComponentName(string widgetZone)
        {
            return "GenerateAllProductTags";
        }

        #endregion

        /// <summary>
        /// Gets a value indicating whether to hide this plugin on the widget list page in the admin area
        /// </summary>
        public bool HideInWidgetList => false;
    }
}
