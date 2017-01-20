using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace Server.ServiceConfigurations
{
    /// <summary>
    ///     Operation behaviour attribute for parameters validation
    /// </summary>
    public abstract class OperationBehaviourAttribute : Attribute, IOperationBehavior, IParameterInspector
    {
        /// <summary>
        ///     Not used
        /// </summary>
        /// <param name="operationDescription">operation description</param>
        public void Validate(OperationDescription operationDescription)
        {
        }

        /// <summary>
        ///     Add custom parameter inspector
        /// </summary>
        /// <param name="operationDescription">operation description</param>
        /// <param name="dispatchOperation">dispatch operation</param>
        public void ApplyDispatchBehavior(OperationDescription operationDescription, DispatchOperation dispatchOperation)
        {
            dispatchOperation.ParameterInspectors.Add(this);
        }

        /// <summary>
        ///     Not used
        /// </summary>
        /// <param name="operationDescription">operation description</param>
        /// <param name="clientOperation">client operation</param>
        public void ApplyClientBehavior(OperationDescription operationDescription, ClientOperation clientOperation)
        {
        }

        /// <summary>
        ///     Not used
        /// </summary>
        /// <param name="operationDescription">operation description</param>
        /// <param name="bindingParameters">binding parameters</param>
        public void AddBindingParameters(OperationDescription operationDescription,
            BindingParameterCollection bindingParameters)
        {
        }

        /// <summary>
        ///     Called before request is passed to service
        /// </summary>
        /// <param name="operationName">operation name</param>
        /// <param name="inputs">request parameters</param>
        /// <returns>correlation state</returns>
        public virtual object BeforeCall(string operationName, object[] inputs)
        {
            return null;
        }

        /// <summary>
        ///     Called befor request is passed to client
        /// </summary>
        /// <param name="operationName">operation name</param>
        /// <param name="outputs">output parameters</param>
        /// <param name="returnValue">return value</param>
        /// <param name="correlationState">correlation state</param>
        public virtual void AfterCall(string operationName, object[] outputs, object returnValue,
            object correlationState)
        {
        }
    }
}