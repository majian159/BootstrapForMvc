using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Rabbit.Bootstrap
{
    /// <summary>
    /// 按钮建设者。
    /// </summary>
    public sealed class ButtonBuilder : TagElementBuilder<ButtonBuilder>
    {
        #region Field

        private readonly TypeConfiguration _typeConfiguration;
        private readonly ButtonSizeConfiguration _buttonSizeConfiguration;
        private readonly ButtonStyleConfiguration _buttonStyleConfiguration;
        private string _href;

        #endregion Field

        #region Constructor

        public ButtonBuilder()
        {
            _typeConfiguration = new TypeConfiguration(this);
            _buttonSizeConfiguration = new ButtonSizeConfiguration(this);
            _buttonStyleConfiguration = new ButtonStyleConfiguration(this);
            AddClass("btn").Style().Default();
        }

        #endregion Constructor

        #region Public Method

        public ButtonBuilder Title(string title)
        {
            TagBuilder.SetInnerText(title);
            return this;
        }

        public TypeConfiguration Type()
        {
            return _typeConfiguration;
        }

        public ButtonSizeConfiguration Size()
        {
            return _buttonSizeConfiguration;
        }

        public ButtonBuilder Active(bool? active = null)
        {
            if (active.HasValue)
                return active.Value ? AddClass("active") : RemoveClass("active");

            return ToggleClass("active");
        }

        public ButtonBuilder Disabled(bool disabled = true)
        {
            if (disabled)
                return Attribute("disabled", "disabled", true);
            TagBuilder.Attributes.Remove("disabled");
            return this;
        }

        public ButtonBuilder Toggle()
        {
            return Attribute("data-toggle", "button", true);
        }

        public ButtonStyleConfiguration Style()
        {
            return _buttonStyleConfiguration;
        }

        public ButtonBuilder Href(string href)
        {
            _href = href;
            return this;
        }

        public ButtonBuilder Click(string click)
        {
            return Attribute("onclick", click, true);
        }

        #endregion Public Method

        #region Overrides of TagElementBuilder<ButtonBuilder>

        public override string TagName
        {
            get { return "button"; }
        }

        /// <summary>
        /// 返回 HTML 编码的字符串。
        /// </summary>
        /// <returns>
        /// HTML 编码的字符串。
        /// </returns>
        public override string ToHtmlString()
        {
            TagBuilder.AddCssClass(_buttonStyleConfiguration.ToString());
            if (string.IsNullOrWhiteSpace(_href))
                return TagBuilder.ToString();

            Attribute("href", _href, true);
            var builder = new StringBuilder();
            builder.AppendLine(TagBuilder.ToString(TagRenderMode.StartTag).Replace(TagName, "a"));
            builder.AppendLine(TagBuilder.InnerHtml);
            builder.AppendLine(TagBuilder.ToString(TagRenderMode.EndTag).Replace(TagName, "a"));

            return new HtmlString(builder.ToString()).ToHtmlString();
        }

        #endregion Overrides of TagElementBuilder<ButtonBuilder>

        #region Overrides of Object

        /// <summary>
        /// 返回表示当前 <see cref="T:System.Object"/> 的 <see cref="T:System.String"/>。
        /// </summary>
        /// <returns>
        /// <see cref="T:System.String"/>，表示当前的 <see cref="T:System.Object"/>。
        /// </returns>
        public override string ToString()
        {
            return ToHtmlString();
        }

        #endregion Overrides of Object

        #region Help Class

        public sealed class TypeConfiguration
        {
            private readonly ButtonBuilder _buttonBuilder;

            public TypeConfiguration(ButtonBuilder buttonBuilder)
            {
                _buttonBuilder = buttonBuilder;
                Button();
            }

            public ButtonBuilder Button()
            {
                return _buttonBuilder.Attribute("type", "button", true);
            }

            public ButtonBuilder Submit()
            {
                return _buttonBuilder.Attribute("type", "submit", true);
            }

            public ButtonBuilder Reset()
            {
                return _buttonBuilder.Attribute("type", "reset", true);
            }
        }

        public sealed class ButtonSizeConfiguration : SizeConfiguration<ButtonBuilder>
        {
            public ButtonSizeConfiguration(ButtonBuilder tagElementBuilder)
                : base("btn", tagElementBuilder)
            {
            }

            public ButtonBuilder ExtraSmall()
            {
                Value = "btn-xs";
                return TagElementBuilder;
            }
        }

        public sealed class ButtonStyleConfiguration : StyleConfiguration<ButtonBuilder>
        {
            public ButtonStyleConfiguration(ButtonBuilder instance)
                : base("btn", instance)
            {
            }

            public ButtonBuilder Link()
            {
                Value = "btn-link";

                return Instance;
            }
        }

        #endregion Help Class
    }
}