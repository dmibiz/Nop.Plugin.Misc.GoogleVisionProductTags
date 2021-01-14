using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Nop.Core.Domain.Catalog;
using Nop.Services.Catalog;
using Nop.Services.Media;
using Nop.Services.Logging;
using Nop.Plugin.Misc.GoogleVisionProductTags.Factories;
using Nop.Plugin.Misc.GoogleVisionProductTags.Api;

namespace Nop.Plugin.Misc.GoogleVisionProductTags.Services
{
    public class GoogleVisionProductTagsGenerator : IGoogleVisionProductTagsGenerator
    {
        #region Fields

        private readonly IProductTagService _productTagService;
        private readonly IProductService _productService;
        private readonly IPictureService _pictureService;
        private readonly ILogger _logger;
        private readonly IGoogleVisionApiFactory _googleVisionApiFactory;

        #endregion

        #region Ctor

        public GoogleVisionProductTagsGenerator(IProductTagService productTagService,
            IProductService productService,
            IPictureService pictureService,
            ILogger logger,
            IGoogleVisionApiFactory googleVisionApiFactory)
        {
            _productTagService = productTagService;
            _productService = productService;
            _pictureService = pictureService;
            _logger = logger;
            _googleVisionApiFactory = googleVisionApiFactory;
        }

        #endregion

        #region Methods

        public async Task GenerateTagsForAllProducts()
        {
            var allProductTags = _productTagService.GetAllProductTags();
            var allProducts = _productService.SearchProducts(showHidden: true);
            var googleVisionApi = _googleVisionApiFactory.GetApi();

            foreach (var product in allProducts)
            {
                await GenerateTagsForProduct(product, allProductTags, googleVisionApi);
            }
        }

        public async Task GenerateTagsForProduct(Product product, IList<ProductTag> allProductTags, GoogleVisionApi googleVisionApi)
        {
            var productPictures = _pictureService.GetPicturesByProductId(product.Id);

            foreach (var picture in productPictures)
            {
                var pictureBinary = _pictureService.LoadPictureBinary(picture);
                var pictureBase64 = Convert.ToBase64String(pictureBinary);

                try
                {
                    var imageLabelsResponse = await googleVisionApi.GetImageLabels(pictureBase64);
                    foreach (var labelAnnotation in imageLabelsResponse.LabelAnnotations)
                    {
                        string tagName = labelAnnotation.Description.ToLower();
                        ProductTag productTag = allProductTags.FirstOrDefault(tag => tag.Name == tagName);
                        bool isNewProductTag = productTag == null;

                        if (productTag == null)
                        {
                            productTag = new ProductTag()
                            {
                                Name = tagName
                            };

                            _productTagService.InsertProductTag(productTag);
                            allProductTags.Add(productTag);
                        }

                        if (isNewProductTag || !_productTagService.ProductTagExists(product, productTag.Id))
                        {
                            ProductProductTagMapping productTagMapping = new ProductProductTagMapping()
                            {
                                ProductId = product.Id,
                                ProductTagId = productTag.Id
                            };

                            _productTagService.InsertProductProductTagMapping(productTagMapping);
                        }


                    }
                }
                catch (Exception exc)
                {
                    _logger.Error($"Cannot generate tags for product {product.Id} picture {picture.Id}", exc);
                }
            }
        }

        #endregion
    }
}
