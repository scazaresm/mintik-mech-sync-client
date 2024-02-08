using Microsoft.VisualBasic.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Log = Serilog.Log;

namespace MechanicalSyncApp.Core.Services
{
    public class VerboseHandler : DelegatingHandler
    {
        public VerboseHandler(HttpMessageHandler innerHandler)
            : base(innerHandler)
        {
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            HttpResponseMessage response = null;

            try
            {
                response = await base.SendAsync(request, cancellationToken);
                if (response.IsSuccessStatusCode)
                {
                    Log.Information("{0} {1} HTTP Status Code = {2}",
                        request.Method,
                        request.RequestUri,
                        response.StatusCode
                    );
                    return response;
                }
                else
                {
                    Log.Information("{0} {1}  HTTP Status Code = {2}", 
                        request.Method, 
                        request.RequestUri, 
                        response.StatusCode
                    );
                }
            }
            catch (Exception ex)
            {
                Log.Error("{0} {1} -> Exception = {2}",
                        request.Method,
                        request.RequestUri,
                        ex
                );
            }
            return response;
        }
    }
}
