using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Nop.Plugin.Misc.GoogleVisionProductTags.Api.Models.Request
{
    public class GoogleVisionApiRequests
    {
        #region Fields

        [JsonProperty("requests")]
        public List<GoogleVisionApiRequest> Requests { get; set; }

        #endregion

        #region Methods

        public void AddRequest(GoogleVisionApiRequest request)
        {
            if (Requests == null) Requests = new List<GoogleVisionApiRequest>();

            Requests.Add(request);
        }

        #endregion
    }
}
