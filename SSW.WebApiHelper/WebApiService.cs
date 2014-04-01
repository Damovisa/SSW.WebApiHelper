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

        public WebApiService() : this(true, true, false) { }    // by default we include only 100s and 200s

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
        public WebApiServiceResponse Get<TArgument>(string baseUrl, string resource, TArgument argument)
        {
            return ExecuteRestRequest(baseUrl, resource, Method.GET, argument);
        }

        public WebApiServiceResponse Get(string baseUrl, string resource, dynamic argument)
        {
            return ExecuteRestRequestDynamicVoid(baseUrl, resource, Method.GET, argument);
        }

        public WebApiServiceResponse<TResult> Get<TResult>(string baseUrl, string resource, dynamic argument = null) where TResult : new()
        {
            return ExecuteRestRequestDynamicWithResult<TResult>(baseUrl, resource, Method.GET, argument);
        }

        public WebApiServiceResponse<TResult> Get<TResult, TArgument>(string baseUrl, string resource, TArgument argument) where TResult : new()
        {
            return ExecuteRestRequestWithResult<TResult, TArgument>(baseUrl, resource, Method.GET, argument);
        }


        /* POST */
        public WebApiServiceResponse Post<TArgument>(string baseUrl, string resource, TArgument argument)
        {
            return ExecuteRestRequest(baseUrl, resource, Method.POST, argument);
        }

        public WebApiServiceResponse Post(string baseUrl, string resource, dynamic argument)
        {
            return ExecuteRestRequestDynamicVoid(baseUrl, resource, Method.POST, argument);
        }

        public WebApiServiceResponse<TResult> Post<TResult>(string baseUrl, string resource, dynamic argument = null) where TResult : new()
        {
            return ExecuteRestRequestDynamicWithResult<TResult>(baseUrl, resource, Method.POST, argument);
        }

        public WebApiServiceResponse<TResult> Post<TResult, TArgument>(string baseUrl, string resource, TArgument argument) where TResult : new()
        {
            return ExecuteRestRequestWithResult<TResult, TArgument>(baseUrl, resource, Method.POST, argument);
        }


        /* PUT */
        public WebApiServiceResponse Put<TArgument>(string baseUrl, string resource, TArgument argument)
        {
            return ExecuteRestRequest(baseUrl, resource, Method.PUT, argument);
        }

        public WebApiServiceResponse Put(string baseUrl, string resource, dynamic argument)
        {
            return ExecuteRestRequestDynamicVoid(baseUrl, resource, Method.PUT, argument);
        }

        public WebApiServiceResponse<TResult> Put<TResult>(string baseUrl, string resource, dynamic argument = null) where TResult : new()
        {
            return ExecuteRestRequestDynamicWithResult<TResult>(baseUrl, resource, Method.PUT, argument);
        }

        public WebApiServiceResponse<TResult> Put<TResult, TArgument>(string baseUrl, string resource, TArgument argument) where TResult : new()
        {
            return ExecuteRestRequestWithResult<TResult, TArgument>(baseUrl, resource, Method.PUT, argument);
        }


        /* DELETE */
        public WebApiServiceResponse Delete<TArgument>(string baseUrl, string resource, TArgument argument)
        {
            return ExecuteRestRequest(baseUrl, resource, Method.DELETE, argument);
        }

        public WebApiServiceResponse Delete(string baseUrl, string resource, dynamic argument)
        {
            return ExecuteRestRequestDynamicVoid(baseUrl, resource, Method.DELETE, argument);
        }

        public WebApiServiceResponse<TResult> Delete<TResult>(string baseUrl, string resource, dynamic argument = null) where TResult : new()
        {
            return ExecuteRestRequestDynamicWithResult<TResult>(baseUrl, resource, Method.DELETE, argument);
        }

        public WebApiServiceResponse<TResult> Delete<TResult, TArgument>(string baseUrl, string resource, TArgument argument) where TResult : new()
        {
            return ExecuteRestRequestWithResult<TResult, TArgument>(baseUrl, resource, Method.DELETE, argument);
        }


        /* REST Requests */
        private WebApiServiceResponse ExecuteRestRequest<TArgument>(string baseUrl, string resource, Method method, TArgument argument)
        {
            var client = new RestClient(baseUrl);
            RestRequest request;
            if (new[] { Method.GET, Method.DELETE, Method.HEAD, Method.OPTIONS }.Contains(method))
            {
                request = CreateRequestWithProperties(resource, argument, method);
            }
            else
            {
                request = CreateRequestWithJsonBody(resource, argument, method);
            }

            var response = client.Execute(request);

            // error if anything other than a success status code has been returned
            if (response.ErrorException == null && _successStatusCodes.Contains(response.StatusCode))
            {
                return new WebApiServiceResponse(response);
            }

            var message = response.ErrorException == null
                ? response.StatusDescription
                : response.ErrorException.Message;

            throw new WebApiException(
                response.StatusCode,
                message,
                baseUrl + "/" + resource,
                response.ErrorException);
        }

        private WebApiServiceResponse ExecuteRestRequestDynamicVoid(string baseUrl, string resource, Method method, dynamic argument)
        {
            var client = new RestClient(baseUrl);
            RestRequest request;
            if (new[] { Method.GET, Method.DELETE, Method.HEAD, Method.OPTIONS }.Contains(method))
            {
                request = CreateRequestWithPropertiesDynamic(resource, argument, method);
            }
            else
            {
                request = CreateRequestWithJsonBodyDynamic(resource, argument, method);
            }

            var response = client.Execute(request);

            // error if anything other than a success status code has been returned
            if (response.ErrorException == null && _successStatusCodes.Contains(response.StatusCode))
            {
                return new WebApiServiceResponse(response);
            }

            var message = response.ErrorException == null
                ? response.StatusDescription
                : response.ErrorException.Message;

            throw new WebApiException(
                response.StatusCode,
                message,
                baseUrl + "/" + resource,
                response.ErrorException);
        }

        private WebApiServiceResponse<TResult> ExecuteRestRequestWithResult<TResult, TArgument>(string baseUrl, string resource, Method method, TArgument argument) where TResult : new()
        {
            var client = new RestClient(baseUrl);
            RestRequest request;
            if (new[] { Method.GET, Method.DELETE, Method.HEAD, Method.OPTIONS }.Contains(method))
            {
                request = CreateRequestWithProperties(resource, argument, method);
            }
            else
            {
                request = CreateRequestWithJsonBody(resource, argument, method);
            }

            var response = client.Execute(request);

            // error if anything other than a success status code has been returned
            if (response.ResponseStatus == ResponseStatus.Completed
                && response.ErrorException == null
                && _successStatusCodes.Contains(response.StatusCode))
            {
                return new WebApiServiceResponse<TResult>(response);
            }

            var message = response.ErrorException == null
                ? response.StatusDescription
                : response.ErrorException.Message;

            throw new WebApiException(
                response.StatusCode,
                message,
                baseUrl + "/" + resource,
                response.ErrorException);
        }

        private WebApiServiceResponse<TResult> ExecuteRestRequestDynamicWithResult<TResult>(string baseUrl, string resource, Method method, dynamic argument) where TResult : new()
        {
            var client = new RestClient(baseUrl);
            RestRequest request;
            if (new[] { Method.GET, Method.DELETE, Method.HEAD, Method.OPTIONS }.Contains(method))
            {
                request = CreateRequestWithPropertiesDynamic(resource, argument, method);
            }
            else
            {
                request = CreateRequestWithJsonBodyDynamic(resource, argument, method);
            }

            var response = client.Execute(request);

            // error if anything other than a success status code has been returned
            if (response.ResponseStatus == ResponseStatus.Completed
                && response.ErrorException == null
                && Enumerable.Contains(_successStatusCodes, response.StatusCode))
            {
                return new WebApiServiceResponse<TResult>(response);
            }

            var message = response.ErrorException == null
                ? response.StatusDescription
                : response.ErrorException.Message;

            throw new WebApiException(
                response.StatusCode,
                message,
                baseUrl + "/" + resource,
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
    }
}
