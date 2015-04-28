namespace Rabbit.Bootstrap
{
    public abstract class SizeConfiguration<T> where T : class
    {
        private readonly string _prefix;
        protected readonly T TagElementBuilder;

        protected string Value;

        protected SizeConfiguration(string prefix, T tagElementBuilder)
        {
            _prefix = prefix;
            TagElementBuilder = tagElementBuilder;
        }

        public T Large()
        {
            Value = _prefix + "-lg";
            return TagElementBuilder;
        }

        public T Default()
        {
            Value = null;
            return TagElementBuilder;
        }

        public T Small()
        {
            Value = _prefix + "-sm";
            return TagElementBuilder;
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
            return Value;
        }

        #endregion Overrides of Object
    }
}