using System.Collections.Generic;
using System.Threading.Tasks;
using Nop.Core.Domain.Catalog;
using Nop.Plugin.Misc.GoogleVisionProductTags.Api;

namespace Nop.Plugin.Misc.GoogleVisionProductTags.Services
{
    public interface IGoogleVisionProductTagsGenerator
    {
        public Task GenerateTagsForAllProducts();

        public Task GenerateTagsForProduct(Product product, IList<ProductTag> allProductTags, GoogleVisionApi googleVisionApi);
    }
}
