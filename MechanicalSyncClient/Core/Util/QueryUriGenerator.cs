using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MechanicalSyncApp.Core.Util
{
    public class QueryUriGenerator
    {
        private readonly string _endpoint;
        private Dictionary<string, string> _queryParameters;

        public QueryUriGenerator(string endpoint, Dictionary<string, string> queryParameters)
        {
            _endpoint = endpoint;
            _queryParameters = queryParameters ?? throw new ArgumentNullException(nameof(queryParameters));
        }

        public string Generate()
        {
            return _endpoint + "?" + string.Join("&",
                _queryParameters.Select(kvp =>
                    $"{HttpUtility.UrlEncode(kvp.Key)}={HttpUtility.UrlEncode(kvp.Value)}"
                )
            );
        }
    }
}
