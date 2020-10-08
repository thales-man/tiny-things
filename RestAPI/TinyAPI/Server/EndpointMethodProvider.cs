//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Http.Service.Contract;
using Http.Service.Contract.Boundaries;
using Http.Service.Contract.Sets;
using Tiny.API.Contracts;
using Tiny.Framework.Container;
using Tiny.Framework.Utility;

namespace Tiny.API
{
    /// <summary>
    /// the endpoint method provider
    /// </summary>
    /// <seealso cref="List{IMethodMetadata}" />
    /// <seealso cref="IProvideEndpointMethods" />
    [Shared]
    [Export(typeof(IProvideEndpointMethods))]
    public class EndpointMethodProvider : 
        List<IMethodMetadata>, 
        IProvideEndpointMethods
    {
        /// <summary>
        /// Gets or sets the container.
        /// </summary>
        [Import]
        public IResolveTypes Container { get; set; }

        /// <summary>
        /// Gets or sets the method factory.
        /// </summary>
        [Import]
        public ExportFactory<IMethodMetadata> MethodFactory { get; set; }

        /// <summary>
        /// Gets or sets the controller factories.
        /// </summary>
        [ImportMany]
        public IEnumerable<ExportFactory<ITinyAPIController>> ControllerFactories { get; set; }

        private string _servicePrefix;

        /// <summary>
        /// Composes the meta data provider.
        /// </summary>
        /// <param name="servicePrefix">The service prefix.</param>
        public void Compose(string servicePrefix)
        {
            Clear();
            _servicePrefix = servicePrefix.ToServicePrefix();
            ControllerFactories
                .ForEach(async x =>
                {
                    using (var export = x.CreateExport())
                    {
                        var controller = export.Value;
                        var methods = await Prepare(controller, x.CreateExport);
                        AddRange(methods);
                    }
                });
        }

        /// <summary>
        /// Gets the method for.
        /// </summary>
        /// <param name="thisURI">this URI.</param>
        /// <param name="usingWebMethod">The web method.</param>
        /// <returns></returns>
        public IMethodMetadata GetMethodFor(IRequestDetail thisURI, WebMethod usingWebMethod)
        {
            var methods = this.Where(r => r.Match(thisURI)).AsSafeList();
            return methods.SingleOrDefault(r => r.Verb == usingWebMethod);
        }

        /// <summary>
        /// Gets the supported web methods for.
        /// </summary>
        /// <param name="thisURI">The this URI.</param>
        /// <returns>the collection of web methods</returns>
        public ICollection<WebMethod> GetSupportedWebMethodsFor(IRequestDetail thisURI)
        {
            var methods = this.Where(r => r.Match(thisURI)).AsSafeList();
            return methods.Select(r => r.Verb).AsSafeList();
        }

        /// <summary>
        /// Prepares the specified controller.
        /// </summary>
        /// <param name="controller">The controller.</param>
        /// <param name="exporter">The exporter.</param>
        /// <returns>
        /// a collection of method metadata
        /// </returns>
        public async Task<ICollection<IMethodMetadata>> Prepare(ITinyAPIController controller, Func<Export<ITinyAPIController>> exporter)
        {
            return await Task.Run(() =>
            {
                var methodCollection = Collection.Empty<IMethodMetadata>();
                var mappedMethods = GetMappedMethods(controller);

                mappedMethods
                    .ForEach(x =>
                    {
                        if (!IsAsyncServerResponse(x))
                        {
                            throw new UnsupportedBehaviourException("Only async controller methods with server responses are supported..");
                        }

                        var metadata = MethodFactory.CreateExport().Value;

                        metadata.Compose(x, exporter, _servicePrefix);
                        methodCollection.Add(metadata);
                    });

                return methodCollection;
            });
        }

        /// <summary>
        /// Gets the mapped methods.
        /// </summary>
        /// <typeparam name="TTinyController">The type of the tiny controller.</typeparam>
        /// <param name="controller">The controller.</param>
        /// <returns>the mapped method info</returns>
        public IEnumerable<MethodInfo> GetMappedMethods<TTinyController>(TTinyController controller)
        {
            var controllerType = controller.GetType();
            return controllerType
                .GetRuntimeMethods()
                .Where(x => x.IsPublic && x.IsDefined(typeof(ResourceFormatAndVerbAttribute)));
        }

        /// <summary>
        /// Determines whether the specified method is asynchronous.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <returns>true if it is</returns>
        private static bool IsAsyncServerResponse(MethodInfo method)
        {
            return method.ReturnType == typeof(Task<IServerResponse>);
        }
    }
}
