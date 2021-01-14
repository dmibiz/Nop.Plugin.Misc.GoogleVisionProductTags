using System;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace Nop.Plugin.Misc.GoogleVisionProductTags.Models
{
    /// <summary>
    /// Represents plugin configuration model
    /// </summary>
    public class ConfigurationModel : BaseNopModel
    {
        [NopResourceDisplayName("Plugins.Misc.GoogleVisionProductTags.GoogleApiKey")]
        public string GoogleApiKey { get; set; }

        [NopResourceDisplayName("Plugins.Misc.GoogleVisionProductTags.MaxTagsPerImage")]
        public int MaxTagsPerImage { get; set; }

        [NopResourceDisplayName("Plugins.Misc.GoogleVisionProductTags.MinConfidenceScore")]
        [Range(0, 1)]
        public decimal MinConfidenceScore { get; set; }

        [NopResourceDisplayName("Plugins.Misc.GoogleVisionProductTags.MinTopicality")]
        [Range(0, 1)]
        public decimal MinTopicality { get; set; }
    }
}
