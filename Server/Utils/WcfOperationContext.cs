using System.Collections.Generic;
using System.ServiceModel;

namespace Server.Utils
{
    public class WcfOperationContext : IExtension<OperationContext>
    {
        public IDictionary<string, object> Items { get; } = new Dictionary<string, object>();

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

        public void Attach(OperationContext owner)
        {
        }

        public void Detach(OperationContext owner)
        {
        }
    }
}