using System;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace Rabbit.Bootstrap
{
    /// <summary>
    /// 输入组件扩展程序。
    /// </summary>
    public static class InputExtensions
    {
        public static InputBuilder TextBox(this BootstrapFactory factory)
        {
            return new InputBuilder().Type().Text();
        }

        public static InputBuilder TextBoxFor<TModel, TProperty>(this BootstrapFactory<TModel> factory, Expression<Func<TModel, TProperty>> expression)
        {
            return factory.UnobtrusiveValidationAttributes(factory.TextBox(), expression);
        }

        public static InputBuilder Password(this BootstrapFactory factory)
        {
            return new InputBuilder().Type().Password();
        }

        public static InputBuilder PasswordFor<TModel, TProperty>(this BootstrapFactory<TModel> factory, Expression<Func<TModel, TProperty>> expression)
        {
            return factory.UnobtrusiveValidationAttributes(factory.Password(), expression);
        }

        #region Private Method

        private static InputBuilder UnobtrusiveValidationAttributes<TModel, TProperty>(this BootstrapFactory<TModel> factory, InputBuilder builder, Expression<Func<TModel, TProperty>> expression)
        {
            var htmlHelper = factory.HtmlHelper;

            var name = ExpressionHelper.GetExpressionText(expression);

            var fullHtmlFieldName = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);

            var modelMetadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var attributes = htmlHelper.GetUnobtrusiveValidationAttributes(name, modelMetadata);

            var value = htmlHelper.FormatValue(modelMetadata.Model, null);
            builder.Id(fullHtmlFieldName).Name(fullHtmlFieldName).Value(value);

            foreach (var attribute in attributes)
                builder.Attribute(attribute.Key, attribute.Value.ToString(), true);

            return builder;
        }

        #endregion Private Method
    }
}