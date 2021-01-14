using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nop.Plugin.Misc.GoogleVisionProductTags.Api.Models.Response
{
    public class GoogleVisionApiResponses
    {
        [JsonProperty("responses")]
        public List<GoogleVisionApiResponse> Responses { get; set; }
    }
}
