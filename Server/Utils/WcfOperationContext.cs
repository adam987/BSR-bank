using System.Collections.Generic;
using System.ServiceModel;

namespace Server.Utils
{
    /// <summary>
    ///     Web operation context to allow attach custom data to request context
    /// </summary>
    public class WcfOperationContext : IExtension<OperationContext>
    {
        /// <summary>
        ///     Data dictionary
        /// </summary>
        public IDictionary<string, object> Items { get; } = new Dictionary<string, object>();

        /// <summary>
        ///     Current web operation context
        /// </summary>
        public static WcfOperationContext Current
        {
            get
            {
                var context = OperationContext.Current.Extensions.Find<WcfOperationContext>();
                if (context != null)
                    return context;

                context = new WcfOperationContext();
                OperationContext.Current.Extensions.Add(context);
                return context;
            }
        }

        private WcfOperationContext()
        {
        }

        /// <summary>
        ///     Not used
        /// </summary>
        /// <param name="owner">owner</param>
        public void Attach(OperationContext owner)
        {
        }

        /// <summary>
        ///     Not used
        /// </summary>
        /// <param name="owner">owner</param>
        public void Detach(OperationContext owner)
        {
        }
    }
}