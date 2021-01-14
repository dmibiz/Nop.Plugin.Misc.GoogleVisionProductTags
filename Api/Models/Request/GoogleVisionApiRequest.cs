using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nop.Plugin.Misc.GoogleVisionProductTags.Api.Models.Request
{
    public class GoogleVisionApiRequest
    {
        #region Fields

        [JsonProperty("image")]
        public GoogleVisionApiRequestImage Image { get; set; }

        [JsonProperty("features")]
        public List<GoogleVisionApiRequestFeature> Features { get; set; }

        public void SetImage(string content)
        {
            var image = new GoogleVisionApiRequestImage()
            {
                Content = content
            };
            Image = image;
        }

        #endregion

        #region Methods

        public void AddFeature(int maxResults, string type)
        {
            if (Features == null) Features = new List<GoogleVisionApiRequestFeature>();

            var feature = new GoogleVisionApiRequestFeature()
            {
                MaxResults = maxResults,
                Type = type
            };
            Features.Add(feature);
        }

        #endregion
    }

    public class GoogleVisionApiRequestImage
    {
        [JsonProperty("content")]
        public String Content { get; set; }
    }

    public class GoogleVisionApiRequestFeature
    {
        [JsonProperty("maxResults")]
        public int MaxResults { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
