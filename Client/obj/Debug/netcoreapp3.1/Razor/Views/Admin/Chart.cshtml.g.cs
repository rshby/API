#pragma checksum "C:\Users\ASUS\Documents\Metrodata\API\Client\Views\Admin\Chart.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "1ae785b5430f668e8c2b9919a0e6742077c43a98"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Admin_Chart), @"mvc.1.0.view", @"/Views/Admin/Chart.cshtml")]
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
#line 1 "C:\Users\ASUS\Documents\Metrodata\API\Client\Views\_ViewImports.cshtml"
using Client;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\ASUS\Documents\Metrodata\API\Client\Views\_ViewImports.cshtml"
using Client.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1ae785b5430f668e8c2b9919a0e6742077c43a98", @"/Views/Admin/Chart.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3332004e6f18ccbec22253d7e177fe1fd5f40969", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Admin_Chart : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "C:\Users\ASUS\Documents\Metrodata\API\Client\Views\Admin\Chart.cshtml"
  
    ViewData["Title"] = "Chart Admin";
    Layout = "_LayoutAdmin";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<div class=""row d-felx justify-content-center"">
    <div class=""col mb-4"">
        <div>
            <h2 class=""text-center"">Cek Chart</h2>
        </div>
    </div>
</div>
<div class=""row d-felx justify-content-center"">
    <div class=""col-5"">
        <h3 class=""text-center"">Chart Univ</h3>
        <div id=""barChartUniv"">
        </div>
    </div>
    <div class=""col-5"">
        <h3 class=""text-center"">Chart Gender</h3>
        <div id=""pieChartGender"">
        </div>
    </div>
</div>");
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
