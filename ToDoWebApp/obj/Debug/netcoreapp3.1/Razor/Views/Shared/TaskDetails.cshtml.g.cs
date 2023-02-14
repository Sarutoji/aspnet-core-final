#pragma checksum "C:\Users\Sarutoji\source\repos\aspnet-core-final-project\ToDoWebApp\Views\Shared\TaskDetails.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "7109a5d7e50a01825d93302a7b735c6e440f2195"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_TaskDetails), @"mvc.1.0.view", @"/Views/Shared/TaskDetails.cshtml")]
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
#line 1 "C:\Users\Sarutoji\source\repos\aspnet-core-final-project\ToDoWebApp\Views\_ViewImports.cshtml"
using ToDoWebApp;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Sarutoji\source\repos\aspnet-core-final-project\ToDoWebApp\Views\_ViewImports.cshtml"
using ToDoWebApp.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\Sarutoji\source\repos\aspnet-core-final-project\ToDoWebApp\Views\_ViewImports.cshtml"
using ToDoWebApp.Models.ViewModels;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7109a5d7e50a01825d93302a7b735c6e440f2195", @"/Views/Shared/TaskDetails.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"387dd7a85518d99a66c251ffb787192717d93731", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Shared_TaskDetails : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<ToDoTask>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("<div class=\"card card-outline-primary m-1 p-1\">\r\n    <div class=\"bg-faded p-1\">\r\n        <h4>\r\n            ");
#nullable restore
#line 5 "C:\Users\Sarutoji\source\repos\aspnet-core-final-project\ToDoWebApp\Views\Shared\TaskDetails.cshtml"
       Write(Model.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </h4>\r\n        <span class=\"card-text p-1\">\r\n            <strong>Task ID: </strong> ");
#nullable restore
#line 8 "C:\Users\Sarutoji\source\repos\aspnet-core-final-project\ToDoWebApp\Views\Shared\TaskDetails.cshtml"
                                  Write(Model.Id);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </span>\r\n        <br />\r\n        <span class=\"card-text p-1\">\r\n            <strong>List ID: </strong> ");
#nullable restore
#line 12 "C:\Users\Sarutoji\source\repos\aspnet-core-final-project\ToDoWebApp\Views\Shared\TaskDetails.cshtml"
                                  Write(Model.ListId);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </span>\r\n        <br />\r\n        <span class=\"card-text p-1\">\r\n            <strong>Description: </strong> ");
#nullable restore
#line 16 "C:\Users\Sarutoji\source\repos\aspnet-core-final-project\ToDoWebApp\Views\Shared\TaskDetails.cshtml"
                                      Write(Model.Description);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </span>\r\n        <br />\r\n        <span class=\"card-text p-1\">\r\n            <strong>Additional info: </strong> ");
#nullable restore
#line 20 "C:\Users\Sarutoji\source\repos\aspnet-core-final-project\ToDoWebApp\Views\Shared\TaskDetails.cshtml"
                                          Write(Model.AdditionalInfo);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </span>\r\n        <br />\r\n        <span class=\"card-text p-1\">\r\n            <strong>Deadline: </strong> ");
#nullable restore
#line 24 "C:\Users\Sarutoji\source\repos\aspnet-core-final-project\ToDoWebApp\Views\Shared\TaskDetails.cshtml"
                                   Write(Model.DueDate.ToString("D"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </span>\r\n        <br />\r\n        <span class=\"card-text p-1\">\r\n            <strong>Creation Date: </strong> ");
#nullable restore
#line 28 "C:\Users\Sarutoji\source\repos\aspnet-core-final-project\ToDoWebApp\Views\Shared\TaskDetails.cshtml"
                                        Write(Model.CreationDate);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </span>\r\n        <br />\r\n");
#nullable restore
#line 31 "C:\Users\Sarutoji\source\repos\aspnet-core-final-project\ToDoWebApp\Views\Shared\TaskDetails.cshtml"
         if (Model.IsCompleted)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <span class=\"card-text p-1\">\r\n                <strong>Is finished: </strong> Yes\r\n            </span>\r\n            <br />\r\n");
#nullable restore
#line 37 "C:\Users\Sarutoji\source\repos\aspnet-core-final-project\ToDoWebApp\Views\Shared\TaskDetails.cshtml"
        }
        else
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <span class=\"card-text p-1\">\r\n                <strong>Is finished: </strong> No\r\n            </span>\r\n            <br />          \r\n");
#nullable restore
#line 44 "C:\Users\Sarutoji\source\repos\aspnet-core-final-project\ToDoWebApp\Views\Shared\TaskDetails.cshtml"
        }

#line default
#line hidden
#nullable disable
#nullable restore
#line 45 "C:\Users\Sarutoji\source\repos\aspnet-core-final-project\ToDoWebApp\Views\Shared\TaskDetails.cshtml"
         if (Model.IsInProgress)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <span class=\"card-text p-1\">\r\n                <strong>Is in progress: </strong> Yes\r\n            </span>\r\n            <br />\r\n");
#nullable restore
#line 51 "C:\Users\Sarutoji\source\repos\aspnet-core-final-project\ToDoWebApp\Views\Shared\TaskDetails.cshtml"
        }
        else
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <span class=\"card-text p-1\">\r\n                <strong>Is in progress: </strong> No\r\n            </span>\r\n            <br />                \r\n");
#nullable restore
#line 58 "C:\Users\Sarutoji\source\repos\aspnet-core-final-project\ToDoWebApp\Views\Shared\TaskDetails.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </div>\r\n</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ToDoTask> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
