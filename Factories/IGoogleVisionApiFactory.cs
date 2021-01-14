using System;
using System.Collections.Generic;
using System.Text;
using Nop.Plugin.Misc.GoogleVisionProductTags.Api;

namespace Nop.Plugin.Misc.GoogleVisionProductTags.Factories
{
    public interface IGoogleVisionApiFactory
    {
        public GoogleVisionApi GetApi();
    }
}
