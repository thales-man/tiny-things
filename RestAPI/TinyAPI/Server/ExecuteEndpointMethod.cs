//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using System;
using System.Composition;
using System.Linq;
using System.Threading.Tasks;
using Http.Service.Contract.Boundaries;
using Http.Service.Contract.Providers;
using Tiny.API.Contracts;
using Tiny.Framework.Utility;

namespace Tiny.API
{
    /// <summary>
    /// the requested method executor
    /// </summary>
    /// <seealso cref="IExecuteEndpointMethods" />
    [Shared]
    [Export(typeof(IExecuteEndpointMethods))]
    public class ExecuteEndpointMethod : IExecuteEndpointMethods
    {
        /// <summary>
        /// Gets or sets the content serialiser
        /// </summary>
        [Import]
        public INegotiateContentSerialization Content { get; set; }

        /// <summary>
        /// Gets or sets the service responder
        /// </summary>
        [Import]
        public IRespondWith RespondWith { get; set; }

        /// <summary>
        /// this is the public facing asynchronous routine
        /// Execute the..
        /// </summary>
        /// <param name="method">method</param>
        /// <param name="usingRequest">using the request</param>
        /// <returns></returns>
        public async Task<IServerResponse> Execute(IMethodMetadata method, IServerRequest usingRequest)
        {
            return await SafeActions.Try(() => ExecuteThis(method, usingRequest), x => HandleError());
        }

        /// <summary>
        /// Execute this...
        /// </summary>
        /// <param name="method">method</param>
        /// <param name="usingRequest">using request</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">missing content parameter</exception>
        public async Task<IServerResponse> ExecuteThis(IMethodMetadata method, IServerRequest usingRequest)
        {
            //TODO: review this, is this the second time we get the parameters? should we store them in the requset?... would that make for a mutating request?
            //TODO: consider ways of deserialising a partial parameter lists into a 'body' object type
            object requestBody = null;
            var parameters = method.GetParameterValuesFrom(usingRequest.URI);

            if (It.Has(usingRequest.Body))
            {
                if (!method.HasBodyParameter)
                {
                    // so we have a body but no 'body' parameter; which means the body is a 'simple' type
                    // so... how do we find it?
                    // i think it must be have to be a string; probably with some special characters in it, making it simpler to send in the body...
                    var item = parameters.FirstOrDefault(x => It.IsType<string>(x) && It.IsEmpty((string)x));

                    if (It.Has(item))
                    {
                        parameters.Remove(item);
                        requestBody = usingRequest.Body;
                    }

                    else
                    {
                        // this will 'convert' to a bad request
                        throw new ArgumentException("missing body parameter");
                    }
                }

                else
                {
                    requestBody = Content.FromString(usingRequest.Body, method.BodyParameterType, usingRequest.ContentMediaType);
                }

                // we may need to insert this item
                parameters.Add(requestBody);
            }

            return await method.InvokeUsing(parameters);
        }

        /// <summary>
        /// Handles the error.
        /// </summary>
        /// <returns>The error.</returns>
        public async Task<IServerResponse> HandleError()
        {
            return await RespondWith.BadRequest();
        }
    }
}
