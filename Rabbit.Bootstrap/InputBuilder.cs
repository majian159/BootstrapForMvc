using System;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Rabbit.Bootstrap
{
    /// <summary>
    /// 输入组件建造者。
    /// </summary>
    public class InputBuilder : TagElementBuilder<InputBuilder>
    {
        #region Field

        private readonly TypeConfiguration _typeConfiguration;
        private readonly SizeConfiguration<InputBuilder> _sizeConfiguration;
        private readonly GroupConfiguration _groupConfiguration;

        #endregion Field

        #region Constructor

        public InputBuilder()
        {
            _typeConfiguration = new TypeConfiguration(this);
            _sizeConfiguration = new InputSizeConfiguration(this);
            _groupConfiguration = new GroupConfiguration();
        }

        #endregion Constructor

        #region Public Method

        public InputBuilder Placeholder(string placeholder)
        {
            return Attribute("placeholder", placeholder, true);
        }

        public TypeConfiguration Type()
        {
            return _typeConfiguration;
        }

        public InputBuilder Disabled(bool disabled = true)
        {
            if (disabled)
                return Attribute("disabled", "disabled", true);
            TagBuilder.Attributes.Remove("disabled");
            return this;
        }

        public SizeConfiguration<InputBuilder> Size()
        {
            return _sizeConfiguration;
        }

        public InputBuilder Group(Action<GroupConfiguration> configuration)
        {
            configuration(_groupConfiguration);
            return this;
        }

        public InputBuilder Value(string value)
        {
            switch (_typeConfiguration.ToString())
            {
                case "checkbox":
                    bool result;
                    Attribute("checked", (bool.TryParse(value, out result) && result) ? "checked" : null);
                    break;

                default:
                    Attribute("value", value);
                    break;
            }
            return this;
        }

        #endregion Public Method

        #region Overrides of TagElementBuilder<InputBuilder>

        public override string TagName
        {
            get { return "input"; }
        }

        /// <summary>
        /// 返回 HTML 编码的字符串。
        /// </summary>
        /// <returns>
        /// HTML 编码的字符串。
        /// </returns>
        public override string ToHtmlString()
        {
            Attribute("type", _typeConfiguration.ToString(), true).AddClass("form-control").AddClass(_sizeConfiguration.ToString());

            var tagHtml = TagBuilder.ToString(TagRenderMode.SelfClosing);

            return _groupConfiguration.Build(tagHtml);
        }

        #endregion Overrides of TagElementBuilder<InputBuilder>

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
            private readonly InputBuilder _inputBuilder;
            private string _type;

            public TypeConfiguration(InputBuilder inputBuilder)
            {
                if (inputBuilder == null)
                    throw new ArgumentNullException("inputBuilder");

                _inputBuilder = inputBuilder;
                Text();
            }

            public InputBuilder Button()
            {
                _type = "button";

                return _inputBuilder;
            }

            public InputBuilder Checkbox()
            {
                _type = "checkbox";

                return _inputBuilder;
            }

            public InputBuilder Color()
            {
                _type = "color";

                return _inputBuilder;
            }

            public InputBuilder Date()
            {
                _type = "date";

                return _inputBuilder;
            }

            public InputBuilder DateTime()
            {
                _type = "datetime";

                return _inputBuilder;
            }

            public InputBuilder DateTimeLocal()
            {
                _type = "datetime-local";

                return _inputBuilder;
            }

            public InputBuilder Email()
            {
                _type = "Email";

                return _inputBuilder;
            }

            public InputBuilder File()
            {
                _type = "file";

                return _inputBuilder;
            }

            public InputBuilder Hidden()
            {
                _type = "hidden";

                return _inputBuilder;
            }

            public InputBuilder Image()
            {
                _type = "image";

                return _inputBuilder;
            }

            public InputBuilder Month()
            {
                _type = "month";

                return _inputBuilder;
            }

            public InputBuilder Number()
            {
                _type = "number";

                return _inputBuilder;
            }

            public InputBuilder Password()
            {
                _type = "password";

                return _inputBuilder;
            }

            public InputBuilder Radio()
            {
                _type = "radio";

                return _inputBuilder;
            }

            public InputBuilder Range()
            {
                _type = "range";

                return _inputBuilder;
            }

            public InputBuilder Reset()
            {
                _type = "reset";

                return _inputBuilder;
            }

            public InputBuilder Search()
            {
                _type = "search";

                return _inputBuilder;
            }

            public InputBuilder Submit()
            {
                _type = "submit";

                return _inputBuilder;
            }

            public InputBuilder Tel()
            {
                _type = "tel";

                return _inputBuilder;
            }

            public InputBuilder Text()
            {
                _type = "text";

                return _inputBuilder;
            }

            public InputBuilder Time()
            {
                _type = "time";

                return _inputBuilder;
            }

            public InputBuilder Url()
            {
                _type = "url";

                return _inputBuilder;
            }

            public InputBuilder Week()
            {
                _type = "week";

                return _inputBuilder;
            }

            public InputBuilder Custom(string type)
            {
                _type = type;

                return _inputBuilder;
            }

            #region Overrides of Object

            /// <summary>
            /// 返回表示当前 <see cref="T:System.Object"/> 的 <see cref="T:System.String"/>。
            /// </summary>
            /// <returns>
            /// <see cref="T:System.String"/>，表示当前的 <see cref="T:System.Object"/>。
            /// </returns>
            public override string ToString()
            {
                return _type;
            }

            #endregion Overrides of Object
        }

        public sealed class InputSizeConfiguration : SizeConfiguration<InputBuilder>
        {
            public InputSizeConfiguration(InputBuilder tagElementBuilder)
                : base("input", tagElementBuilder)
            {
            }
        }

        public sealed class GroupConfiguration
        {
            private readonly GroupSizeConfiguration _groupSizeConfiguration;
            private string _html;
            private bool _isLeft = true;

            public GroupConfiguration()
            {
                _groupSizeConfiguration = new GroupSizeConfiguration(this);
            }

            public GroupConfiguration Html(string html)
            {
                _html = html;
                return this;
            }

            public GroupConfiguration Left()
            {
                _isLeft = true;
                return this;
            }

            public GroupConfiguration Right()
            {
                _isLeft = false;
                return this;
            }

            public GroupSizeConfiguration Size()
            {
                return _groupSizeConfiguration;
            }

            internal string Build(string html)
            {
                if (string.IsNullOrWhiteSpace(_html))
                    return html;

                var builder = new StringBuilder();
                var size = _groupSizeConfiguration.ToString();
                builder.AppendLine(string.Format("<div class=\"input-group{0}\">", string.IsNullOrWhiteSpace(size) ? string.Empty : " " + size));
                if (_isLeft)
                {
                    builder.AppendLine(string.Format("<span class=\"input-group-addon\">{0}</span>", _html));
                    builder.AppendLine(html);
                }
                else
                {
                    builder.AppendLine(html);
                    builder.AppendLine(string.Format("<span class=\"input-group-addon\">{0}</span>", _html));
                }
                builder.AppendLine("</div>");

                return new HtmlString(builder.ToString()).ToHtmlString();
            }

            public sealed class GroupSizeConfiguration : SizeConfiguration<GroupConfiguration>
            {
                public GroupSizeConfiguration(GroupConfiguration tagElementBuilder)
                    : base("input-group", tagElementBuilder)
                {
                }
            }
        }

        #endregion Help Class
    }
}