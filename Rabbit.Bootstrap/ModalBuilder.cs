using System;
using System.Text;
using System.Web.Mvc;

namespace Rabbit.Bootstrap
{
    /// <summary>
    /// 模态框建设者。
    /// </summary>
    public sealed class ModalBuilder : TagElementBuilder<ModalBuilder>
    {
        #region Field

        private string _headerHtml;
        private string _bodyHtml;
        private string _footerHtml;

        #endregion Field

        #region Field

        internal TagBuilder ModalTagBuilder { get { return TagBuilder; } }

        #endregion Field

        #region Constructor

        public ModalBuilder()
        {
            AddClass("modal").AddClass("fade").Attribute("role", "dalog").Attribute("aria-hidden", "true");
        }

        #endregion Constructor

        #region Public Method

        public ModalBuilder Header(string html)
        {
            _headerHtml = html;
            return this;
        }

        public ModalBuilder Header(Action<ModalHeaderConfiguration> configuration)
        {
            if (configuration == null)
                throw new ArgumentNullException("configuration");

            var modalHeaderConfiguration = new ModalHeaderConfiguration(this);
            configuration((modalHeaderConfiguration));
            Header(modalHeaderConfiguration.ToString());
            return this;
        }

        public ModalBuilder Body(string html)
        {
            _bodyHtml = html;
            return this;
        }

        public ModalBuilder Footer(string html)
        {
            _footerHtml = html;
            return this;
        }

        #endregion Public Method

        #region Overrides of TagElementBuilder<ButtonBuilder>

        public override string TagName
        {
            get { return "div"; }
        }

        /// <summary>
        /// 返回 HTML 编码的字符串。
        /// </summary>
        /// <returns>
        /// HTML 编码的字符串。
        /// </returns>
        public override string ToHtmlString()
        {
            var builder = new StringBuilder();

            var labelId = TagBuilder.Attributes["id"] + "Label";
            Attribute("aria-labelledby", labelId);

            builder.AppendLine(TagBuilder.ToString(TagRenderMode.StartTag));
            builder.AppendLine("<div class=\"modal-dialog\">");
            builder.AppendLine("  <div class=\"modal-content\">");
            builder.AppendLine("    <div class=\"modal-header\">");
            //Header
            builder.AppendLine(MvcHtmlString.Create(_headerHtml).ToHtmlString());
            builder.AppendLine("    </div>");
            builder.AppendLine("    <div class=\"modal-body\">");
            //Body
            builder.AppendLine(MvcHtmlString.Create(_bodyHtml).ToHtmlString());
            builder.AppendLine("    </div>");
            builder.AppendLine("    <div class=\"modal-footer\">");
            builder.AppendLine(MvcHtmlString.Create(_footerHtml).ToHtmlString());
            //Fotter
            builder.AppendLine("    </div>");
            builder.AppendLine("  </div>");
            builder.AppendLine("</div>");

            builder.AppendLine(TagBuilder.ToString(TagRenderMode.EndTag));

            return MvcHtmlString.Create(builder.ToString()).ToHtmlString();
        }

        #endregion Overrides of TagElementBuilder<ButtonBuilder>

        #region Help Class

        public sealed class ModalHeaderConfiguration
        {
            private readonly ModalBuilder _modalBuilder;
            private string _titleHtml;
            private bool _showClose;

            public ModalHeaderConfiguration(ModalBuilder modalBuilder)
            {
                _modalBuilder = modalBuilder;
            }

            public ModalHeaderConfiguration Title(string html, string format = "<h4 class=\"modal-title\">{0}</h4>")
            {
                _titleHtml = string.IsNullOrWhiteSpace(format) ? html : string.Format(html, format);
                return this;
            }

            public ModalHeaderConfiguration ShowClose(bool value = true)
            {
                _showClose = value;
                return this;
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
                var builder = new StringBuilder();

                if (_showClose)
                    builder.AppendLine("<button type=\"button\" class=\"close\" data-dismiss=\"modal\" aria-hidden=\"true\">&times;</button>");
                if (!string.IsNullOrWhiteSpace(_titleHtml))
                    builder.AppendLine(string.Format("<h4 class=\"modal-title\" id=\"{0}\">{1}</h4>", _modalBuilder.TagBuilder.Attributes["id"] + "Label", _titleHtml));

                return builder.ToString();
            }

            #endregion Overrides of Object
        }

        #endregion Help Class
    }
}