using System.Net.Http;
using Nop.Core.Http;
using Nop.Plugin.Misc.GoogleVisionProductTags.Api;

namespace Nop.Plugin.Misc.GoogleVisionProductTags.Factories
{
    public class GoogleVisionApiFactory : IGoogleVisionApiFactory
    {
        #region Fields

        private readonly GoogleVisionProductTagsSettings _googleVisionProductTagsSettings;
        private readonly IHttpClientFactory _httpClientFactory;

        #endregion

        #region Ctor

        public GoogleVisionApiFactory(GoogleVisionProductTagsSettings googleVisionProductTagsSettings,
            IHttpClientFactory httpClientFactory)
        {
            _googleVisionProductTagsSettings = googleVisionProductTagsSettings;
            _httpClientFactory = httpClientFactory;
        }

        #endregion

        #region Methods

        public GoogleVisionApi GetApi()
        {
            return new GoogleVisionApi(_googleVisionProductTagsSettings, _httpClientFactory.CreateClient(NopHttpDefaults.DefaultHttpClient));
        }

        #endregion
    }
}
