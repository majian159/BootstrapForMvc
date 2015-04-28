using System;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace Rabbit.Bootstrap
{
    public static class FormExtensions
    {
        public static MvcForm BeginForm(this BootstrapFactory factory,
            string actionName, string controllerName, Action<FormBuilder> configuration)
        {
            return factory.BeginForm(actionName, controllerName, new RouteValueDictionary(), configuration);
        }

        public static MvcForm BeginForm(this BootstrapFactory factory,
            string actionName, string controllerName, object routeValues, Action<FormBuilder> configuration)
        {
            return factory.BeginForm(actionName, controllerName, new RouteValueDictionary(routeValues), configuration);
        }

        public static MvcForm BeginForm(this BootstrapFactory factory, string actionName, string controllerName, RouteValueDictionary routeValues, Action<FormBuilder> configuration)
        {
            return factory.BeginForm(c =>
            {
                var url = UrlHelper.GenerateUrl(null, actionName, controllerName, routeValues, factory.HtmlHelper.RouteCollection,
                    factory.HtmlHelper.ViewContext.RequestContext, true);
                c.Url(url);
                if (configuration != null)
                    configuration(c);
            });
        }

        public static MvcForm BeginForm(this BootstrapFactory factory, Action<FormBuilder> configuration)
        {
            var builder = new FormBuilder(factory.HtmlHelper);
            if (configuration != null)
                configuration(builder);
            return builder.Build();
        }
    }
}