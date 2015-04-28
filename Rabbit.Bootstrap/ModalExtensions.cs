using System;

namespace Rabbit.Bootstrap
{
    public static class ModalExtensions
    {
        public static ModalBuilder Modal(this BootstrapFactory factory, string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentNullException("id");

            return new ModalBuilder().Id(id);
        }

        public static ModalBuilder Modal(this BootstrapFactory factory)
        {
            return factory.Modal(Guid.NewGuid().ToString("N"));
        }
    }
}