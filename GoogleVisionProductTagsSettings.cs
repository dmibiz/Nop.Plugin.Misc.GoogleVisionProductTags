using Nop.Core.Configuration;

namespace Nop.Plugin.Misc.GoogleVisionProductTags
{
    public class GoogleVisionProductTagsSettings : ISettings
    {
        /// <summary>
        /// Gets or sets Erply account username
        /// </summary>
        public string GoogleApiKey { get; set; }

        /// <summary>
        /// Gets or sets maximum number of generated tags per image
        /// </summary>
        public int MaxTagsPerImage { get; set; }

        /// <summary>
        /// Gets or sets minimum confidence score for a tag to be saved
        /// </summary>
        public decimal MinConfidenceScore { get; set; }

        /// <summary>
        /// Gets or sets minimum topicality for a tag to be saved
        /// </summary>
        public decimal MinTopicality { get; set; }
    }
}
