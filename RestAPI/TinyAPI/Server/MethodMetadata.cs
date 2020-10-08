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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Http.Service.Contract;
using Http.Service.Contract.Boundaries;
using Http.Service.Contract.Sets;
using Tiny.API.Contracts;
using Tiny.Framework.Diagnostic;
using Tiny.Framework.Flow;
using Tiny.Framework.Utility;

namespace Tiny.API
{
    /// <summary>
    /// the controller method info
    /// </summary>
    /// <seealso cref="IMethodMetadata" />
    [Export(typeof(IMethodMetadata))]
    public class MethodMetadata : IMethodMetadata
    {
        const string queryCollector = "{(.*?)}";

        /// <summary>
        /// The find parameter regex
        /// </summary>
        private static readonly Regex FindParameter = new Regex(queryCollector, RegexOptions.Compiled);

        /// <summary>
        /// The match parameter expression
        /// </summary>
        private const string MatchParameterExpression = "(?<$1>.+?)";

        /// <summary>
        /// The match URI expression
        /// </summary>
        private const string MatchURIExpression = @"\w+";

        /// <summary>
        /// The URI parameter types
        /// </summary>
        private static IEnumerable<Type> _uriParameterTypes = new[] {
                typeof(Guid),
                typeof(Guid?),
                typeof(string),
                typeof(decimal),
                typeof(decimal?),
                typeof(double),
                typeof(double?),
                typeof(float),
                typeof(float?),
                typeof(short),
                typeof(short?),
                typeof(int),
                typeof(int?),
                typeof(long),
                typeof(long?),
                typeof(byte),
                typeof(byte?),
                typeof(bool),
                typeof(bool?),
                typeof(DateTime),
                typeof(DateTime?),
                typeof(char),
                typeof(char?),
                typeof(sbyte),
                typeof(sbyte?),
                typeof(ushort),
                typeof(ushort?),
                typeof(uint),
                typeof(uint?),
                typeof(ulong),
                typeof(ulong?),
            };

        /// <summary>
        /// The _find parameter values
        /// </summary>
        private Regex _findParameterValues;

        /// <summary>
        /// The _match URI
        /// </summary>
        private Regex _matchUri;

        /// <summary>
        /// The _url prefix
        /// </summary>
        private string _servicePrefix;

        /// <summary>
        /// The _exporter
        /// </summary>
        private Func<Export<ITinyAPIController>> _exporter;

        /// <summary>
        /// Gets the parameters.
        /// </summary>
        public ICollection<IMethodParameter> Parameters { get; set; }

        /// <summary>
        /// Gets the method information.
        /// </summary>
        public MethodInfo MethodInfo { get; set; }

        /// <summary>
        /// Gets the type of the return.
        /// </summary>
        public Type ReturnType { get; set; }

        /// <summary>
        /// Gets the verb.
        /// </summary>
        public WebMethod Verb { get; set; }

        /// <summary>
        /// Gets or sets the mediator.
        /// </summary>
        [Import]
        public IManageMessageMediation Mediator { get; set; }

        /// <summary>
        /// Gets or sets the diagnostic message factory
        /// </summary>
        [Import]
        public ICreateDiagnosticMessages Message { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MethodMetadata"/> class.
        /// </summary>
        public MethodMetadata()
        {
            Parameters = Collection.Empty<IMethodParameter>();
        }

        /// <summary>
        /// Gets a value indicating whether this instance has content parameter.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance has content parameter; otherwise, <c>false</c>.
        /// </value>
        public bool HasBodyParameter => Parameters.Any(x => x.IsBody);

        /// <summary>
        /// Gets the type of body parameter.
        /// </summary>
        public Type BodyParameterType =>
            HasBodyParameter
                ? Parameters.FirstOrDefault(x => x.IsBody).Type
                : null;

        /// <summary>
        /// Composes the specified method information.
        /// </summary>
        /// <param name="methodInfo">The method information.</param>
        /// <param name="exporter">The exporter.</param>
        /// <param name="servicePrefix">The service prefix.</param>
        public void Compose(MethodInfo methodInfo, Func<Export<ITinyAPIController>> exporter, string servicePrefix)
        {
            _exporter = exporter;
            _servicePrefix = servicePrefix;
            MethodInfo = methodInfo;

            InitializeParameters();
            InitializeVerb();
            InitializeFindParameters();
            InitializeMatchUri();

            var detail = $"method name: {MethodInfo.Name}, verb: {Verb}, parameters: {string.Join(", ", Parameters.Select(x => x.Name))}";
            Mediator.Publish(Message.Create(detail));
        }

        /// <summary>
        /// Gets the parameter values from.
        /// </summary>
        /// <param name="requestURI">The request URI.</param>
        /// <returns></returns>
        public ICollection<object> GetParameterValuesFrom(IRequestDetail requestURI)
        {
            var parameters = Collection.Empty<object>();

            var matched = _findParameterValues.Match(requestURI.Path);
            var queryParameters = requestURI.QueryParameters;

            if (matched.Success || queryParameters.Any())
            {
                Parameters
                    .ForEach(parameter =>
                    {
                        // url matched parameter
                        var candidate = matched.Groups[parameter.Name].Value;

                        // query parameter
                        if (It.IsEmpty(candidate) && queryParameters.ContainsKey(parameter.Name))
                        {
                            candidate = queryParameters[parameter.Name];
                        }

                        if (It.Has(candidate))
                        {
                            parameters.Add(ChangeType(candidate, parameter.Type, parameter.IsOptional));
                            return; // we got there, move next
                        }

                        // 'complex' parameter
                        if (It.IsEmpty(candidate) && parameter.IsBody)
                        {
                            // build the complex type from the query parameters
                            var candidateType = parameter.Type;

                            if (candidateType.IsInterface)
                            {
                                var bodyContent = candidateType
                                    .GetCustomAttributes<ParameterisedBodyContentAttribute>()
                                    .FirstOrDefault();

                                candidateType = bodyContent.ToCreate;
                            }

                            var bodyParameters = candidateType
                                .GetProperties()
                                .SelectMany(x => x.GetCustomAttributes<ParameterMapAttribute>());

                            var complex = Activator.CreateInstance(candidateType);

                            queryParameters.ForEach(x =>
                            {
                                var bodyParameter = bodyParameters
                                    .FirstOrDefault(y => y.FromParameter.ComparesWith(x.Key));

                                var pi = candidateType.GetProperty(bodyParameter.ToProperty);

                                pi.SetValue(complex, ChangeType(x.Value, pi.PropertyType, false));
                            });

                            parameters.Add(complex);
                        }
                    });
            }

            return parameters;
        }

        /// <summary>
        /// Changes the type.
        /// copes with the short comings of guid conversions...
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="newType">The new type.</param>
        /// <returns></returns>
        public object ChangeType(string value, Type newType, bool isOptional)
        {
            if (It.IsEmpty(value))
            {
                if (isOptional)
                {
                    return Type.Missing;
                }

                if (newType.IsValueType)
                {
                    return Activator.CreateInstance(newType);
                }

                return null;
            }

            if (typeof(Guid).IsAssignableFrom(newType))
            {
                return new Guid(value);
            }

            if (newType.GetTypeInfo().IsEnum)
            {
                return Enum.Parse(newType, value);
            }

            return Convert.ChangeType(value, newType);
        }

        /// <summary>
        /// Initializes the parameters.
        /// </summary>
        public void InitializeParameters()
        {
            var methodParameters = MethodInfo.GetParameters();
            Parameters.Clear();

            var parms = methodParameters
                .Select(p => new MethodParameter
                {
                    Name = p.Name,
                    Type = p.ParameterType,
                    Position = p.Position,
                    IsBody = !IsURIType(p.ParameterType),
                    IsOptional = p.IsOptional
                });

            if (parms.Count(x => x.IsBody) > 1)
            {
                throw new ArgumentOutOfRangeException($"there can only be one body parameter '{MethodInfo.Name}'");
            }

            parms.ForEach(Parameters.Add);
        }

        /// <summary>
        /// Determines whether [is URI type] [the specified parameter type].
        /// </summary>
        /// <param name="candidateType">Type of the parameter.</param>
        /// <returns>true if it is a URI paramter</returns>
        public bool IsURIType(Type candidateType)
        {
            return _uriParameterTypes.Contains(candidateType) || candidateType.GetTypeInfo().IsEnum;
        }

        /// <summary>
        /// Initializes the match URI.
        /// </summary>
        public void InitializeMatchUri()
        {
            var expression = MakeFormatExpression(MatchURIExpression);
            _matchUri = new Regex(expression, RegexOptions.Compiled);
        }

        /// <summary>
        /// Initializes the find parameters.
        /// </summary>
        public void InitializeFindParameters()
        {
            string expression = MakeFormatExpression(MatchParameterExpression);
            _findParameterValues = new Regex(expression, RegexOptions.Compiled);
        }

        /// <summary>
        /// Makes the format expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>the expression string</returns>
        public string MakeFormatExpression(string expression)
        {
            var uriFormatter = MethodInfo.GetCustomAttribute<ResourceFormatAndVerbAttribute>();
            string uriFormatWithPrefix = CreateUriFormat(uriFormatter);
            return string.Format("^{0}$", FindParameter.Replace(uriFormatWithPrefix, expression));
        }

        /// <summary>
        /// Creates the URI format.
        /// </summary>
        /// <param name="uriFormatter">The URI formatter.</param>
        /// <returns>the formatted URI</returns>
        public string CreateUriFormat(ResourceFormatAndVerbAttribute uriFormatter)
        {
            var uriFormat = uriFormatter.URIFormat.Clean().ToRegexCompatible();
            return It.IsEmpty(_servicePrefix)
                ? $"/{uriFormat}"
                : $"{_servicePrefix}/{uriFormat}";
        }

        /// <summary>
        /// Initializes the verb.
        /// </summary>
        private void InitializeVerb()
        {
            Verb = MethodInfo.GetCustomAttribute<ResourceFormatAndVerbAttribute>().Verb;
        }

        /// <summary>
        /// Matches the specified URI.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <returns>true if matched</returns>
        public bool Match(IRequestDetail uri)
        {
            return _matchUri.IsMatch(uri.Path);
        }

        /// <summary>
        /// Invokes the using.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>a response from the method call</returns>
        public async Task<IServerResponse> InvokeUsing(ICollection<object> parameters)
        {
            using (var export = _exporter())
            {
                var controller = export.Value;
                var result = (Task<IServerResponse>)MethodInfo.Invoke(controller, parameters.ToArray());
                return await result;
            }
        }
    }
}
