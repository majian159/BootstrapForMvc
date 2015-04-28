namespace Rabbit.Bootstrap
{
    public abstract class StyleConfiguration<T> where T : class
    {
        private readonly string _prefix;
        protected T Instance;
        protected string Value;

        protected StyleConfiguration(string prefix, T instance)
        {
            _prefix = prefix;
            Instance = instance;
        }

        public T Default()
        {
            Value = _prefix + "-default";
            return Instance;
        }

        public T Primary()
        {
            Value = _prefix + "-primary";
            return Instance;
        }

        public T Success()
        {
            Value = _prefix + "-success";
            return Instance;
        }

        public T Info()
        {
            Value = _prefix + "-info";
            return Instance;
        }

        public T Warning()
        {
            Value = _prefix + "-warning";
            return Instance;
        }

        public T Danger()
        {
            Value = _prefix + "-danger";
            return Instance;
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