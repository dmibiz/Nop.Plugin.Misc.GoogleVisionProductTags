using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Nop.Plugin.Misc.GoogleVisionProductTags.Api.Models.Request;
using Nop.Plugin.Misc.GoogleVisionProductTags.Api.Models.Response;

namespace Nop.Plugin.Misc.GoogleVisionProductTags.Api
{
    public class GoogleVisionApi
    {
        #region Fields

        private const string Url = "https://vision.googleapis.com/v1/images:annotate";
        private const string LabelDetectionRequestType = "LABEL_DETECTION";

        private readonly GoogleVisionProductTagsSettings _settings;
        private readonly HttpClient _httpClient;

        #endregion

        #region Ctor

        public GoogleVisionApi(GoogleVisionProductTagsSettings settings,
            HttpClient httpClient)
        {
            _settings = settings;
            _httpClient = httpClient;
        }

        #endregion

        #region Methods

        public async Task<GoogleVisionApiResponses> SendRequest(GoogleVisionApiRequests requests)
        {
            var jsonRequest = JsonConvert.SerializeObject(requests);
            var stringContent = new StringContent(jsonRequest, UnicodeEncoding.UTF8, "application/json");
            var response = (await _httpClient.PostAsync($"{Url}?key={_settings.GoogleApiKey}", stringContent)).EnsureSuccessStatusCode();
            var responseContentString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<GoogleVisionApiResponses>(responseContentString);
        }

        public async Task<GoogleVisionApiResponse> GetImageLabels(string imageAsBase64)
        {
            var requests = new GoogleVisionApiRequests();
            var request = new GoogleVisionApiRequest();
            request.SetImage(imageAsBase64);
            request.AddFeature(_settings.MaxTagsPerImage, LabelDetectionRequestType);
            requests.AddRequest(request);
            return (await SendRequest(requests))?.Responses?[0];
        }

        #endregion

    }
}
