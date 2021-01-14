using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nop.Plugin.Misc.GoogleVisionProductTags.Api.Models.Response
{
    public class GoogleVisionApiResponse
    {
        [JsonProperty("labelAnnotations")]
        public List<GoogleVisionApiResponseLabelAnnotation> LabelAnnotations { get; set; }
    }

    public class GoogleVisionApiResponseLabelAnnotation
    {
        [JsonProperty("mid")]
        public string Mid { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("score")]
        public decimal Score { get; set; }

        [JsonProperty("topicality")]
        public decimal Topicality { get; set; }
    }
}
