#pragma checksum "/Users/patrick/Desktop/local/Projects/MovieStore/MovieStore.MVC/Views/Movies/Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "66c42249374b1e12dfa6269b5f0a05dff0dd42e0"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Movies_Index), @"mvc.1.0.view", @"/Views/Movies/Index.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "/Users/patrick/Desktop/local/Projects/MovieStore/MovieStore.MVC/Views/_ViewImports.cshtml"
using MovieStore.MVC;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "/Users/patrick/Desktop/local/Projects/MovieStore/MovieStore.MVC/Views/_ViewImports.cshtml"
using MovieStore.MVC.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"66c42249374b1e12dfa6269b5f0a05dff0dd42e0", @"/Views/Movies/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ba8e10bf9b95e33ede95742276f21d13171015fd", @"/Views/_ViewImports.cshtml")]
    public class Views_Movies_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<MovieStore.Core.Entities.Review>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n<div>\r\n    <div class=\"container-fluid\">\r\n        <div class=\"row\">\r\n\r\n");
#nullable restore
#line 7 "/Users/patrick/Desktop/local/Projects/MovieStore/MovieStore.MVC/Views/Movies/Index.cshtml"
             foreach (var review in Model)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <ul class=\"list-group\">\r\n                    <li class=\"list-group-item active\">");
#nullable restore
#line 10 "/Users/patrick/Desktop/local/Projects/MovieStore/MovieStore.MVC/Views/Movies/Index.cshtml"
                                                  Write(review.MovieId);

#line default
#line hidden
#nullable disable
            WriteLiteral("</li>\r\n                    <li class=\"list-group-item\">");
#nullable restore
#line 11 "/Users/patrick/Desktop/local/Projects/MovieStore/MovieStore.MVC/Views/Movies/Index.cshtml"
                                           Write(review.Rating);

#line default
#line hidden
#nullable disable
            WriteLiteral("</li>\r\n                    <li class=\"list-group-item\">");
#nullable restore
#line 12 "/Users/patrick/Desktop/local/Projects/MovieStore/MovieStore.MVC/Views/Movies/Index.cshtml"
                                           Write(review.ReviewText);

#line default
#line hidden
#nullable disable
            WriteLiteral("</li>\r\n                </ul>\r\n");
#nullable restore
#line 14 "/Users/patrick/Desktop/local/Projects/MovieStore/MovieStore.MVC/Views/Movies/Index.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </div>\r\n    </div>\r\n</div>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<MovieStore.Core.Entities.Review>> Html { get; private set; }
    }
}
#pragma warning restore 1591
