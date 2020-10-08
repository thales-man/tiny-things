using System.Collections.Generic;
using System.Web;
using Http.Service.Contract.Boundaries;

namespace Http.Service.Models.Boundaries
{
    public class RequestDetail :
        IRequestDetail
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Http.Service.Models.Boundaries.RequestDetail"/> class.
        /// </summary>
        /// <param name="candidate">Candidate.</param>
        public RequestDetail(string candidate)
        {
            Original = HttpUtility.UrlDecode(candidate);
            Path = candidate;

            var queryIndex = candidate.IndexOfAny(new[] { '?', '#' });
            if (queryIndex > 0)
            {
                Query = HttpUtility.UrlDecode(candidate.Substring(queryIndex));
                Path = candidate.Substring(0, queryIndex);
                QueryParameters = ParametersFromQuery.Get(Query);
            }
        }

        /// <summary>
        /// Gets the original.
        /// </summary>
        /// <value>The original.</value>
        public string Original { get; }

        /// <summary>
        /// Gets the path.
        /// </summary>
        /// <value>The path.</value>
        public string Path { get; }

        /// <summary>
        /// Gets the query.
        /// </summary>
        /// <value>The query.</value>
        public string Query { get; }

        /// <summary>
        /// Gets the query parameters.
        /// </summary>
        /// <value>The query parameters.</value>
        public IDictionary<string, string> QueryParameters { get; } = new Dictionary<string, string>();
    }
}
