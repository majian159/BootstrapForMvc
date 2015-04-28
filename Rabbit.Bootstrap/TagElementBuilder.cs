using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rabbit.Bootstrap
{
    public abstract class TagElementBuilder<T> : IHtmlString where T : class
    {
        #region Field

        private TagBuilder _tagBuilder;

        #endregion Field

        protected TagBuilder TagBuilder
        {
            get
            {
                if (_tagBuilder != null)
                    return _tagBuilder;
                return _tagBuilder = new TagBuilder(TagName);
            }
        }

        public abstract string TagName { get; }

        public T Id(string id)
        {
            TagBuilder.GenerateId(id);

            return this as T;
        }

        public T Name(string name)
        {
            TagBuilder.MergeAttribute("name", name, true);

            return this as T;
        }

        public T Attribute(string key, string value, bool replaceExisting = false)
        {
            TagBuilder.MergeAttribute(key, value, replaceExisting);

            return this as T;
        }

        public T AddClass(string className)
        {
            if (string.IsNullOrWhiteSpace(className))
                return this as T;

            if (ContainsClass(className))
                return this as T;

            TagBuilder.AddCssClass(className);

            return this as T;
        }

        public T RemoveClass(string className)
        {
            if (string.IsNullOrWhiteSpace(className))
                return this as T;

            if (!ContainsClass(className))
                return this as T;

            var classs = TagBuilder.Attributes["class"];

            var classArray = classs.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Where(i => string.Equals(i, className, StringComparison.OrdinalIgnoreCase));

            TagBuilder.Attributes["class"] = string.Join(" ", classArray);

            return this as T;
        }

        public T ToggleClass(string className)
        {
            if (string.IsNullOrWhiteSpace(className))
                return this as T;

            return ContainsClass(className) ? RemoveClass(className) : AddClass(className);
        }

        #region Private Method

        private bool ContainsClass(string className)
        {
            string classs;
            if (!TagBuilder.Attributes.TryGetValue("class", out classs))
                return false;

            var classArray = classs.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToArray();
            return classArray.Any(i => string.Equals(i, className, StringComparison.OrdinalIgnoreCase));
        }

        #endregion Private Method

        #region Implementation of IHtmlString

        /// <summary>
        /// ·µ»Ø HTML ±àÂëµÄ×Ö·û´®¡£
        /// </summary>
        /// <returns>
        /// HTML ±àÂëµÄ×Ö·û´®¡£
        /// </returns>
        public abstract string ToHtmlString();

        #endregion Implementation of IHtmlString
    }
}