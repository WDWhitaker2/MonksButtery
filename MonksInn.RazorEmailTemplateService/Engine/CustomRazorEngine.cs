// https://scottsauber.com/2018/07/07/walkthrough-creating-an-html-email-template-with-razor-and-razor-class-libraries-and-rendering-it-from-a-net-standard-class-library/

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using MonksInn.Domain.Interfaces;
using MonksInn.RazorEmailTemplateService.Models;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MonksInn.RazorEmailTemplateService.Engine
{
    public class CustomRazorEngine
    {
        private readonly IRazorViewEngine RazorViewEngine;
        private readonly ITempDataProvider TempDataProvider;
        private readonly IServiceProvider ServiceProvider;

        public CustomRazorEngine(
            IRazorViewEngine razorViewEngine,
            ITempDataProvider tempDataProvider,
            IServiceProvider serviceProvider
            )
        {
            this.RazorViewEngine = razorViewEngine;
            this.TempDataProvider = tempDataProvider;
            this.ServiceProvider = serviceProvider;
        }

        public async Task<string> RenderViewToStringAsync<TModel>(string viewName, TModel model, string BaseUrl)
        {
            var fullViewName = $"/Views/EmailTemplates/{viewName}.cshtml";
            var actionContext = GetActionContext();
            var view = FindView(actionContext, fullViewName);

            using (var output = new StringWriter())
            {
                var viewContext = new ViewContext(
                    actionContext,
                    view,
                    new ViewDataDictionary<TModel>(
                        metadataProvider: new EmptyModelMetadataProvider(),
                        modelState: new ModelStateDictionary())
                    {
                        Model = model
                    },
                    new TempDataDictionary(
                        actionContext.HttpContext,
                        TempDataProvider),
                    output,
                    new HtmlHelperOptions());
                viewContext.TempData["BaseUrl"] = BaseUrl;
                await view.RenderAsync(viewContext);

                return output.ToString();
            }
        }

        private IView FindView(ActionContext actionContext, string viewName)
        {
            var getViewResult = RazorViewEngine.GetView(executingFilePath: null, viewPath: viewName, isMainPage: true);
            if (getViewResult.Success)
            {
                return getViewResult.View;
            }

            var findViewResult = RazorViewEngine.FindView(actionContext, viewName, isMainPage: true);
            if (findViewResult.Success)
            {
                return findViewResult.View;
            }

            var searchedLocations = getViewResult.SearchedLocations.Concat(findViewResult.SearchedLocations);
            var errorMessage = string.Join(
                Environment.NewLine,
                new[] { $"Unable to find view '{viewName}'. The following locations were searched:" }.Concat(searchedLocations)); ;

            throw new InvalidOperationException(errorMessage);
        }

        private ActionContext GetActionContext()
        {
            var httpContext = new DefaultHttpContext();
            httpContext.RequestServices = ServiceProvider;
            return new ActionContext(httpContext, new RouteData(), new ActionDescriptor());
        }

    }
   
}
