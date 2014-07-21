using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using Dynamitey;
using Microsoft.CSharp.RuntimeBinder;
using Newtonsoft.Json;
using RestSharp;

namespace SSW.WebApiHelper
{
    using System.Security.Cryptography.X509Certificates;

    public class WebApiService : IWebApiService
    {
        #region HttpStatus Groups
        private static readonly HttpStatusCode[] HttpStatus100s =
        {
            HttpStatusCode.Continue, HttpStatusCode.SwitchingProtocols
        };

        private static readonly HttpStatusCode[] HttpStatus200s =
        {
            HttpStatusCode.OK, HttpStatusCode.Created, HttpStatusCode.Accepted,
            HttpStatusCode.NonAuthoritativeInformation, HttpStatusCode.NoContent, HttpStatusCode.ResetContent,
            HttpStatusCode.PartialContent
        };

        private static readonly HttpStatusCode[] HttpStatus300s =
        {
            HttpStatusCode.MultipleChoices, HttpStatusCode.Moved, HttpStatusCode.MovedPermanently, HttpStatusCode.Found,
            HttpStatusCode.Redirect, HttpStatusCode.RedirectMethod, HttpStatusCode.SeeOther, HttpStatusCode.NotModified,
            HttpStatusCode.UseProxy, HttpStatusCode.Unused, HttpStatusCode.RedirectKeepVerb,
            HttpStatusCode.TemporaryRedirect
        };

        private static readonly HttpStatusCode[] HttpStatus400s =
        {
            HttpStatusCode.BadRequest, HttpStatusCode.Unauthorized, HttpStatusCode.PaymentRequired,
            HttpStatusCode.Forbidden, HttpStatusCode.NotFound, HttpStatusCode.MethodNotAllowed,
            HttpStatusCode.NotAcceptable, HttpStatusCode.ProxyAuthenticationRequired, HttpStatusCode.RequestTimeout,
            HttpStatusCode.Conflict, HttpStatusCode.Gone, HttpStatusCode.LengthRequired,
            HttpStatusCode.PreconditionFailed, HttpStatusCode.RequestEntityTooLarge, HttpStatusCode.RequestUriTooLong,
            HttpStatusCode.UnsupportedMediaType, HttpStatusCode.RequestedRangeNotSatisfiable,
            HttpStatusCode.ExpectationFailed, HttpStatusCode.UpgradeRequired
        };

        private static readonly HttpStatusCode[] HttpStatus500s =
        {
            HttpStatusCode.InternalServerError, HttpStatusCode.NotImplemented, HttpStatusCode.BadGateway,
            HttpStatusCode.ServiceUnavailable, HttpStatusCode.GatewayTimeout, HttpStatusCode.HttpVersionNotSupported
        };

        #endregion

        private readonly IEnumerable<HttpStatusCode> _successStatusCodes;

        // Include: 100, 200, 201, 202, 203, 204, 205
        public WebApiService() :
            this(HttpStatusCode.Continue, HttpStatusCode.OK, HttpStatusCode.Created, HttpStatusCode.Accepted,
                HttpStatusCode.NonAuthoritativeInformation, HttpStatusCode.NoContent, HttpStatusCode.ResetContent) { }

        public WebApiService(bool succeedOn100s, bool succeedOn200s, bool succeedOn300s, bool succeedOn400s = false, bool succeedOn500s = false)
        {
            var codes = new List<HttpStatusCode>();
            if (succeedOn100s)
                codes.AddRange(HttpStatus100s);
            if (succeedOn200s)
                codes.AddRange(HttpStatus200s);
            if (succeedOn300s)
                codes.AddRange(HttpStatus300s);
            if (succeedOn400s)
                codes.AddRange(HttpStatus400s);
            if (succeedOn500s)
                codes.AddRange(HttpStatus500s);

            _successStatusCodes = codes;
        }

        public WebApiService(params HttpStatusCode[] successStatusCodes)
        {
            _successStatusCodes = successStatusCodes;
        }

        /* GET */
        public WebApiServiceResponse Get<TArgument>(string baseUrl, string resource, TArgument argument, IEnumerable<HttpStatusCode> overrideSuccessStatusCodes = null)
        {
            return ExecuteRestRequest(baseUrl, resource, Method.GET, argument, overrideSuccessStatusCodes);
        }

        public WebApiServiceResponse Get(string baseUrl, string resource, dynamic argument = null, IEnumerable<HttpStatusCode> overrideSuccessStatusCodes = null)
        {
            return ExecuteRestRequestDynamicVoid(baseUrl, resource, Method.GET, argument, overrideSuccessStatusCodes);
        }

        public WebApiServiceResponse<TResult> Get<TResult>(string baseUrl, string resource, dynamic argument = null, IEnumerable<HttpStatusCode> overrideSuccessStatusCodes = null) where TResult : new()
        {
            return ExecuteRestRequestDynamicWithResult<TResult>(baseUrl, resource, Method.GET, argument, overrideSuccessStatusCodes);
        }

        public WebApiServiceResponse<TResult> Get<TResult, TArgument>(string baseUrl, string resource, TArgument argument, IEnumerable<HttpStatusCode> overrideSuccessStatusCodes = null) where TResult : new()
        {
            return ExecuteRestRequestWithResult<TResult, TArgument>(baseUrl, resource, Method.GET, argument, overrideSuccessStatusCodes);
        }

        /* POST */
        public WebApiServiceResponse Post<TArgument>(string baseUrl, string resource, TArgument argument, IEnumerable<HttpStatusCode> overrideSuccessStatusCodes = null)
        {
            return ExecuteRestRequest(baseUrl, resource, Method.POST, argument, overrideSuccessStatusCodes);
        }

        public WebApiServiceResponse Post(string baseUrl, string resource, dynamic argument = null, IEnumerable<HttpStatusCode> overrideSuccessStatusCodes = null)
        {
            return ExecuteRestRequestDynamicVoid(baseUrl, resource, Method.POST, argument, overrideSuccessStatusCodes);
        }

        public WebApiServiceResponse<TResult> Post<TResult>(string baseUrl, string resource, dynamic argument = null, IEnumerable<HttpStatusCode> overrideSuccessStatusCodes = null) where TResult : new()
        {
            return ExecuteRestRequestDynamicWithResult<TResult>(baseUrl, resource, Method.POST, argument, overrideSuccessStatusCodes);
        }

        public WebApiServiceResponse<TResult> Post<TResult, TArgument>(string baseUrl, string resource, TArgument argument, IEnumerable<HttpStatusCode> overrideSuccessStatusCodes = null) where TResult : new()
        {
            return ExecuteRestRequestWithResult<TResult, TArgument>(baseUrl, resource, Method.POST, argument, overrideSuccessStatusCodes);
        }

        /* PUT */
        public WebApiServiceResponse Put<TArgument>(string baseUrl, string resource, TArgument argument, IEnumerable<HttpStatusCode> overrideSuccessStatusCodes = null)
        {
            return ExecuteRestRequest(baseUrl, resource, Method.PUT, argument, overrideSuccessStatusCodes);
        }

        public WebApiServiceResponse Put(string baseUrl, string resource, dynamic argument = null, IEnumerable<HttpStatusCode> overrideSuccessStatusCodes = null)
        {
            return ExecuteRestRequestDynamicVoid(baseUrl, resource, Method.PUT, argument, overrideSuccessStatusCodes);
        }

        public WebApiServiceResponse<TResult> Put<TResult>(string baseUrl, string resource, dynamic argument = null, IEnumerable<HttpStatusCode> overrideSuccessStatusCodes = null) where TResult : new()
        {
            return ExecuteRestRequestDynamicWithResult<TResult>(baseUrl, resource, Method.PUT, argument, overrideSuccessStatusCodes);
        }

        public WebApiServiceResponse<TResult> Put<TResult, TArgument>(string baseUrl, string resource, TArgument argument, IEnumerable<HttpStatusCode> overrideSuccessStatusCodes = null) where TResult : new()
        {
            return ExecuteRestRequestWithResult<TResult, TArgument>(baseUrl, resource, Method.PUT, argument, overrideSuccessStatusCodes);
        }

        /* DELETE */
        public WebApiServiceResponse Delete<TArgument>(string baseUrl, string resource, TArgument argument, IEnumerable<HttpStatusCode> overrideSuccessStatusCodes = null)
        {
            return ExecuteRestRequest(baseUrl, resource, Method.DELETE, argument, overrideSuccessStatusCodes);
        }

        public WebApiServiceResponse Delete(string baseUrl, string resource, dynamic argument = null, IEnumerable<HttpStatusCode> overrideSuccessStatusCodes = null)
        {
            return ExecuteRestRequestDynamicVoid(baseUrl, resource, Method.DELETE, argument, overrideSuccessStatusCodes);
        }

        public WebApiServiceResponse<TResult> Delete<TResult>(string baseUrl, string resource, dynamic argument = null, IEnumerable<HttpStatusCode> overrideSuccessStatusCodes = null) where TResult : new()
        {
            return ExecuteRestRequestDynamicWithResult<TResult>(baseUrl, resource, Method.DELETE, argument, overrideSuccessStatusCodes);
        }

        public WebApiServiceResponse<TResult> Delete<TResult, TArgument>(string baseUrl, string resource, TArgument argument, IEnumerable<HttpStatusCode> overrideSuccessStatusCodes = null) where TResult : new()
        {
            return ExecuteRestRequestWithResult<TResult, TArgument>(baseUrl, resource, Method.DELETE, argument, overrideSuccessStatusCodes);
        }

        /* REST Requests */
        private WebApiServiceResponse ExecuteRestRequest<TArgument>(string baseUrl, string resource, Method method, TArgument argument, IEnumerable<HttpStatusCode> overrideSuccessStatusCodes = null)
        {
            var client = new RestClient(baseUrl);

            this.SetCertificate(client);
            this.SetTimeout(client);
            RestRequest request;
            if (new[] { Method.GET, Method.DELETE, Method.HEAD, Method.OPTIONS }.Contains(method))
            {
                request = CreateRequestWithProperties(resource, argument, method);
            }
            else
            {
                request = CreateRequestWithJsonBody(resource, argument, method);
            }
            var successCodes = overrideSuccessStatusCodes ?? _successStatusCodes;

            var uri = client.BuildUri(request);
            var response = client.Execute(request);

            // error if anything other than a success status code has been returned
            if (response.ErrorException == null && successCodes.Contains(response.StatusCode))
            {
                return new WebApiServiceResponse(response, uri);
            }

            var message = response.ErrorException == null
                ? response.StatusDescription
                : response.ErrorException.Message;

            throw new WebApiException(
                response.StatusCode,
                message,
                uri.AbsoluteUri,
                response.ErrorException);
        }

        private WebApiServiceResponse ExecuteRestRequestDynamicVoid(string baseUrl, string resource, Method method, dynamic argument, IEnumerable<HttpStatusCode> overrideSuccessStatusCodes = null)
        {
            var client = new RestClient(baseUrl);
            this.SetCertificate(client);
            this.SetTimeout(client);
            
            RestRequest request;
            
            if (new[] { Method.GET, Method.DELETE, Method.HEAD, Method.OPTIONS }.Contains(method))
            {
                request = CreateRequestWithPropertiesDynamic(resource, argument, method);
            }
            else
            {
                request = CreateRequestWithJsonBodyDynamic(resource, argument, method);
            }
            var successCodes = overrideSuccessStatusCodes ?? _successStatusCodes;

            var uri = client.BuildUri(request);
            var response = client.Execute(request);

            // error if anything other than a success status code has been returned
            if (response.ErrorException == null && successCodes.Contains(response.StatusCode))
            {
                return new WebApiServiceResponse(response, uri);
            }

            var message = response.ErrorException == null
                ? response.StatusDescription
                : response.ErrorException.Message;

            throw new WebApiException(
                response.StatusCode,
                message,
                uri.AbsoluteUri,
                response.ErrorException);
        }

        private WebApiServiceResponse<TResult> ExecuteRestRequestWithResult<TResult, TArgument>(string baseUrl, string resource, Method method, TArgument argument, IEnumerable<HttpStatusCode> overrideSuccessStatusCodes = null) where TResult : new()
        {
            var client = new RestClient(baseUrl);
            this.SetCertificate(client);
            this.SetTimeout(client);
            RestRequest request;
            if (new[] { Method.GET, Method.DELETE, Method.HEAD, Method.OPTIONS }.Contains(method))
            {
                request = CreateRequestWithProperties(resource, argument, method);
            }
            else
            {
                request = CreateRequestWithJsonBody(resource, argument, method);
            }
            var successCodes = overrideSuccessStatusCodes ?? _successStatusCodes;

            var uri = client.BuildUri(request);
            var response = client.Execute(request);

            // error if anything other than a success status code has been returned
            if (response.ResponseStatus == ResponseStatus.Completed
                && response.ErrorException == null
                && successCodes.Contains(response.StatusCode))
            {
                return new WebApiServiceResponse<TResult>(response, uri);
            }

            var message = response.ErrorException == null
                ? response.StatusDescription
                : response.ErrorException.Message;

            throw new WebApiException(
                response.StatusCode,
                message,
                uri.AbsoluteUri,
                response.ErrorException);
        }

        private WebApiServiceResponse<TResult> ExecuteRestRequestDynamicWithResult<TResult>(string baseUrl, string resource, Method method, dynamic argument, IEnumerable<HttpStatusCode> overrideSuccessStatusCodes = null) where TResult : new()
        {
            var client = new RestClient(baseUrl);
            this.SetCertificate(client);
            this.SetTimeout(client);
            RestRequest request;
            if (new[] { Method.GET, Method.DELETE, Method.HEAD, Method.OPTIONS }.Contains(method))
            {
                request = CreateRequestWithPropertiesDynamic(resource, argument, method);
            }
            else
            {
                request = CreateRequestWithJsonBodyDynamic(resource, argument, method);
            }
            var successCodes = overrideSuccessStatusCodes ?? _successStatusCodes;

            var uri = client.BuildUri(request);
            var response = client.Execute(request);

            // error if anything other than a success status code has been returned
            if (response.ResponseStatus == ResponseStatus.Completed
                && response.ErrorException == null
                && successCodes.Contains(response.StatusCode))
            {
                return new WebApiServiceResponse<TResult>(response, uri);
            }

            var message = response.ErrorException == null
                ? response.StatusDescription
                : response.ErrorException.Message;

            throw new WebApiException(
                response.StatusCode,
                message,
                uri.AbsoluteUri,
                response.ErrorException);
        }
        
        /* Shared REST Request Creation */
        private static RestRequest CreateRequestWithPropertiesDynamic(string resource, dynamic argument, Method method)
        {
            var request = new RestRequest(resource, method);

            if (argument != null)
            {
                var properties = Dynamic.GetMemberNames(argument);
                
                foreach (var prop in properties)
                {
                    object value = "";
                    try { value = Dynamic.InvokeGet(argument, prop); }
                    catch (RuntimeBinderException) { }
                    var isSegment = resource.Contains("{" + prop + "}");

                    request.AddParameter(
                        prop, value,
                        isSegment ? ParameterType.UrlSegment : ParameterType.GetOrPost);
                }
            }
            return request;
        }

        private static RestRequest CreateRequestWithProperties<TArgument>(string resource, TArgument argument, Method method)
        {
            var request = new RestRequest(resource, method);

            if (argument != null)
            {
                var properties = typeof (TArgument).GetProperties()
                    .Where(p => p.GetValue(argument) != null)
                    .Select(p => new
                            {
                                p.Name,
                                Value = p.GetValue(argument),
                                IsSegment = resource.Contains("{" + p.Name + "}")
                            });

                foreach (var prop in properties)
                {
                    request.AddParameter(
                        prop.Name, prop.Value,
                        prop.IsSegment ? ParameterType.UrlSegment : ParameterType.GetOrPost);
                }
            }
            return request;
        }

        private static RestRequest CreateRequestWithJsonBody<TArgument>(string resource, TArgument argument, Method method)
        {
            var request = new RestRequest(resource, method) { RequestFormat = DataFormat.Json };

            if (argument != null)
            {
                request.AddBody(argument);

                // Put the URL segments in the URL
                var matcher = new Regex(@"{(\w*)}");
                foreach (Match m in matcher.Matches(resource))
                {
                    var param = m.Value.Substring(1, m.Value.Length - 2);
                    var property = typeof (TArgument).GetProperty(param);
                    if (property != null)
                    {
                        var value = property.GetValue(argument).ToString();
                        request.AddParameter(param, value, ParameterType.UrlSegment);
                    }
                    else
                    {
                        request.AddParameter(param, "", ParameterType.UrlSegment);
                    }
                }
            }
            return request;
        }

        private static RestRequest CreateRequestWithJsonBodyDynamic(string resource, dynamic argument, Method method)
        {
            var request = new RestRequest(resource, method) { RequestFormat = DataFormat.Json };

            if (argument != null)
            {
                request.AddBody(argument);

                // Put the URL segments in the URL
                var matcher = new Regex(@"{(\w*)}");
                foreach (Match m in matcher.Matches(resource))
                {
                    var param = m.Value.Substring(1, m.Value.Length - 2);
                    var property = ((IDictionary<string, object>)argument)[param];
                    if (property != null)
                    {
                        request.AddParameter(param, property, ParameterType.UrlSegment);
                    }
                    else
                    {
                        request.AddParameter(param, "", ParameterType.UrlSegment);
                    }
                }
            }
            return request;
        }

        /// <summary>
        /// Sets the certificate for authentication.
        /// </summary>
        /// <param name="client">The rest client.</param>
        private void SetTimeout(RestClient client)
        {
            if (!this.Timeout.HasValue)
            {
                return;
            }

            client.Timeout = this.Timeout.Value;
        }

        /// <summary>
        /// Sets the timeout.
        /// </summary>
        /// <param name="client">The rest client.</param>
        private void SetCertificate(RestClient client)
        {
            if (this.Certificate == null)
            {
                return;
            }

            if (client.ClientCertificates == null)
            {
                client.ClientCertificates = new X509Certificate2Collection();
            }

            client.ClientCertificates.Add(this.Certificate);
        }

        /// <summary>
        /// Gets or sets the service timeout.
        /// </summary>
        /// <value>The timeout value in milliseconds.</value>
        public int? Timeout { get; set; }

        /// <summary>
        /// Gets or sets the service timeout.
        /// </summary>
        /// <value>The timeout value in milliseconds.</value>
        public X509Certificate2 Certificate { get; set; }
    }
}
