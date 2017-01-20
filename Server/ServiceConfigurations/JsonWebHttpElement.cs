using System;
using System.ServiceModel.Configuration;

namespace Server.ServiceConfigurations
{
    /// <summary>
    ///     Configuration behaviour element
    /// </summary>
    public class JsonWebHttpElement : BehaviorExtensionElement
    {
        /// <summary>
        ///     Element type
        /// </summary>
        public override Type BehaviorType => typeof(JsonWebHttpBehaviour);

        protected override object CreateBehavior() => new JsonWebHttpBehaviour();
    }
}