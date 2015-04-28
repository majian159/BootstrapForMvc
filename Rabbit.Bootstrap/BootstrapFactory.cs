using System.Web.Mvc;

namespace Rabbit.Bootstrap
{
    /// <summary>
    /// Bootstrap 工厂。
    /// </summary>
    public class BootstrapFactory
    {
        /// <summary>
        /// Html助手。
        /// </summary>
        internal HtmlHelper HtmlHelper { get; private set; }

        /// <summary>
        /// 初始化一个新的 Bootstrap 工厂。
        /// </summary>
        /// <param name="htmlHelper">Html 助手。</param>
        public BootstrapFactory(HtmlHelper htmlHelper)
        {
            HtmlHelper = htmlHelper;
        }
    }

    /// <summary>
    /// Bootstrap 泛型工厂。
    /// </summary>
    /// <typeparam name="TModel">模型类型。</typeparam>
    public sealed class BootstrapFactory<TModel> : BootstrapFactory
    {
        /// <summary>
        /// Html助手。
        /// </summary>
        new internal HtmlHelper<TModel> HtmlHelper { get; private set; }

        /// <summary>
        /// 初始化一个新的 Bootstrap 工厂。
        /// </summary>
        /// <param name="htmlHelper">Html 助手。</param>
        public BootstrapFactory(HtmlHelper<TModel> htmlHelper)
            : base(htmlHelper)
        {
            HtmlHelper = htmlHelper;
        }
    }

    /// <summary>
    /// Bootstrap 工厂扩展方法。
    /// </summary>
    public static class BootstrapFactoryExtensions
    {
        /// <summary>
        /// Bootstrap。
        /// </summary>
        /// <param name="htmlHelper">Html 助手。</param>
        /// <returns>Bootstrap 工厂。</returns>
        public static BootstrapFactory Bootstrap(this HtmlHelper htmlHelper)
        {
            return new BootstrapFactory(htmlHelper);
        }

        /// <summary>
        /// Bootstrap。
        /// </summary>
        /// <typeparam name="TModel">模型类型。</typeparam>
        /// <param name="htmlHelper">Html 助手。</param>
        /// <returns>Bootstrap 工厂。</returns>
        public static BootstrapFactory<TModel> Bootstrap<TModel>(this HtmlHelper<TModel> htmlHelper)
        {
            return new BootstrapFactory<TModel>(htmlHelper);
        }
    }
}