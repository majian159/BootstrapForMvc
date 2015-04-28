namespace Rabbit.Bootstrap
{
    /// <summary>
    /// 按钮扩展方法。
    /// </summary>
    public static class ButtonExtensions
    {
        public static ButtonBuilder Button(this BootstrapFactory factory, string title)
        {
            return factory.Button().Title(title);
        }

        public static ButtonBuilder Button(this BootstrapFactory factory)
        {
            return new ButtonBuilder();
        }
    }
}