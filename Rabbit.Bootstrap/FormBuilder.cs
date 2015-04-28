using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Rabbit.Bootstrap
{
    public sealed class FormBuilder
    {
        #region Field

        private readonly HtmlHelper _htmlHelper;

        private readonly FormMethodConfiguration _formMethodConfiguration;
        private readonly FormEnctypeConfiguration _formEnctypeConfiguration;
        private readonly FormTypeConfiguration _formTypeConfiguration;
        private string _url;

        #endregion Field

        #region Constructor

        public FormBuilder(HtmlHelper htmlHelper)
        {
            _htmlHelper = htmlHelper;

            _formMethodConfiguration = new FormMethodConfiguration(this);
            _formEnctypeConfiguration = new FormEnctypeConfiguration(this);
            _formTypeConfiguration = new FormTypeConfiguration(this);
        }

        #endregion Constructor

        #region Public Method

        public FormBuilder Url(string url)
        {
            _url = url;

            return this;
        }

        public FormMethodConfiguration Method()
        {
            return _formMethodConfiguration;
        }

        public FormEnctypeConfiguration Enctype()
        {
            return _formEnctypeConfiguration;
        }

        public FormTypeConfiguration Type()
        {
            return _formTypeConfiguration;
        }

        internal MvcForm Build()
        {
            var url = _url;
            if (string.IsNullOrWhiteSpace(url))
                url = _htmlHelper.ViewContext.HttpContext.Request.RawUrl;

            var method = _formMethodConfiguration.ToString();
            var enctype = _formEnctypeConfiguration.ToString();
            var className = _formTypeConfiguration.ToString();

            var dictionary = new Dictionary<string, object>
            {
                {"role","form"},
                {"action",url},
                {"method",string.IsNullOrWhiteSpace(method)?"post":method}
            };

            if (!string.IsNullOrWhiteSpace(enctype))
                dictionary.Add("enctype", enctype);
            if (!string.IsNullOrWhiteSpace(className))
                dictionary.Add("class", className);

            return _htmlHelper.BeginForm(string.Empty, string.Empty, FormMethod.Post, dictionary);
        }

        #endregion Public Method

        #region Help Class

        public sealed class FormMethodConfiguration
        {
            #region Field

            private readonly FormBuilder _formBuilder;
            private string _method;

            #endregion Field

            #region Constructor

            public FormMethodConfiguration(FormBuilder formBuilder)
            {
                _formBuilder = formBuilder;
            }

            #endregion Constructor

            #region Public Method

            public FormBuilder Post()
            {
                _method = "post";
                return _formBuilder;
            }

            public FormBuilder Get()
            {
                _method = "get";
                return _formBuilder;
            }

            #endregion Public Method

            #region Overrides of Object

            /// <summary>
            /// 返回表示当前 <see cref="T:System.Object"/> 的 <see cref="T:System.String"/>。
            /// </summary>
            /// <returns>
            /// <see cref="T:System.String"/>，表示当前的 <see cref="T:System.Object"/>。
            /// </returns>
            public override string ToString()
            {
                return _method;
            }

            #endregion Overrides of Object
        }

        public sealed class FormEnctypeConfiguration
        {
            #region Field

            private readonly FormBuilder _formBuilder;
            private string _enctype;

            #endregion Field

            #region Constructor

            public FormEnctypeConfiguration(FormBuilder formBuilder)
            {
                _formBuilder = formBuilder;
            }

            #endregion Constructor

            #region Public Method

            /// <summary>
            /// application/x-www-form-urlencoded
            /// </summary>
            /// <returns></returns>
            public FormBuilder Default()
            {
                _enctype = "application/x-www-form-urlencoded";
                return _formBuilder;
            }

            /// <summary>
            /// multipart/form-data
            /// </summary>
            /// <returns></returns>
            public FormBuilder FormData()
            {
                _enctype = "multipart/form-data";
                return _formBuilder;
            }

            /// <summary>
            /// text/plain
            /// </summary>
            /// <returns></returns>
            public FormBuilder TextPlain()
            {
                _enctype = "text/plain";
                return _formBuilder;
            }

            #endregion Public Method

            #region Overrides of Object

            /// <summary>
            /// 返回表示当前 <see cref="T:System.Object"/> 的 <see cref="T:System.String"/>。
            /// </summary>
            /// <returns>
            /// <see cref="T:System.String"/>，表示当前的 <see cref="T:System.Object"/>。
            /// </returns>
            public override string ToString()
            {
                return _enctype;
            }

            #endregion Overrides of Object
        }

        public sealed class FormTypeConfiguration
        {
            #region Field

            private readonly FormBuilder _formBuilder;
            private string _className;

            #endregion Field

            #region Constructor

            public FormTypeConfiguration(FormBuilder formBuilder)
            {
                _formBuilder = formBuilder;
            }

            #endregion Constructor

            #region Public Method

            public FormBuilder Inline()
            {
                _className = "form-inline";

                return _formBuilder;
            }

            public FormBuilder Horizontal()
            {
                _className = "form-horizontal";

                return _formBuilder;
            }

            #endregion Public Method

            #region Overrides of Object

            /// <summary>
            /// 返回表示当前 <see cref="T:System.Object"/> 的 <see cref="T:System.String"/>。
            /// </summary>
            /// <returns>
            /// <see cref="T:System.String"/>，表示当前的 <see cref="T:System.Object"/>。
            /// </returns>
            public override string ToString()
            {
                return _className;
            }

            #endregion Overrides of Object
        }

        #endregion Help Class
    }
}