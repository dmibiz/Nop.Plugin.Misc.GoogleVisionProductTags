using Microsoft.AspNetCore.Mvc;
using Nop.Web.Framework.Components;
using System;

namespace Nop.Plugin.Misc.GoogleVisionProductTags.Components
{
    [ViewComponent(Name = "GenerateAllProductTags")]
    public class GenerateAllProductTagsViewComponent : NopViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("~/Plugins/Misc.GoogleVisionProductTags/Views/TagsGeneration/GenerateAllProductTags.cshtml");
        }
    }
}
