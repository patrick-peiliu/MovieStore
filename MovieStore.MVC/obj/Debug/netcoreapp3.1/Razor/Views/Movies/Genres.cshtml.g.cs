#pragma checksum "/Users/patrick/Desktop/local/Projects/MovieStore/MovieStore.MVC/Views/Movies/Genres.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "c2a7685fa9727069b4a4a2723b1fcfaaa7953b0d"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Movies_Genres), @"mvc.1.0.view", @"/Views/Movies/Genres.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c2a7685fa9727069b4a4a2723b1fcfaaa7953b0d", @"/Views/Movies/Genres.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ba8e10bf9b95e33ede95742276f21d13171015fd", @"/Views/_ViewImports.cshtml")]
    public class Views_Movies_Genres : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<MovieStore.Core.Entities.Movie>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n<div>\r\n    <div class=\"container-fluid\">\r\n        <div class=\"row\">\r\n");
#nullable restore
#line 6 "/Users/patrick/Desktop/local/Projects/MovieStore/MovieStore.MVC/Views/Movies/Genres.cshtml"
             foreach (var movie in Model)
            {
                

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "/Users/patrick/Desktop/local/Projects/MovieStore/MovieStore.MVC/Views/Movies/Genres.cshtml"
           Write(await Html.PartialAsync("_MovieCard", movie));

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "/Users/patrick/Desktop/local/Projects/MovieStore/MovieStore.MVC/Views/Movies/Genres.cshtml"
                                                             
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<MovieStore.Core.Entities.Movie>> Html { get; private set; }
    }
}
#pragma warning restore 1591
