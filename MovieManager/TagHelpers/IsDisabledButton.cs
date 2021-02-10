using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieManager.TagHelpers
{
    [HtmlTargetElement("button")]
    public class IsDisabledButton : TagHelper
    {
        [HtmlAttributeName("asp-is-disabled")]
        public bool IsDisabled { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (IsDisabled)
            {
                var d = new TagHelperAttribute("disabled", "disabled");
                output.Attributes.Add(d);
            }

            base.Process(context, output);
        }
    }
}
