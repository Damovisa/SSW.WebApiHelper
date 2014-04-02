using System.Collections;
using System.Collections.Generic;
using System.Net;

namespace SSW.WebApiHelper
{
    public interface IWebApiService
    {
        WebApiServiceResponse Get<TArgument>(string baseUrl, string resource, TArgument argument, IEnumerable<HttpStatusCode> overrideSuccessStatusCodes = null);
        WebApiServiceResponse Get(string baseUrl, string resource, dynamic argument = null, IEnumerable<HttpStatusCode> overrideSuccessStatusCodes = null);
        WebApiServiceResponse<TResult> Get<TResult>(string baseUrl, string resource, dynamic argument = null, IEnumerable<HttpStatusCode> overrideSuccessStatusCodes = null) where TResult : new();
        WebApiServiceResponse<TResult> Get<TResult, TArgument>(string baseUrl, string resource, TArgument argument, IEnumerable<HttpStatusCode> overrideSuccessStatusCodes = null) where TResult : new();

        WebApiServiceResponse Post<TArgument>(string baseUrl, string resource, TArgument argument, IEnumerable<HttpStatusCode> overrideSuccessStatusCodes = null);
        WebApiServiceResponse Post(string baseUrl, string resource, dynamic argument = null, IEnumerable<HttpStatusCode> overrideSuccessStatusCodes = null);
        WebApiServiceResponse<TResult> Post<TResult>(string baseUrl, string resource, dynamic argument = null, IEnumerable<HttpStatusCode> overrideSuccessStatusCodes = null) where TResult : new();
        WebApiServiceResponse<TResult> Post<TResult, TArgument>(string baseUrl, string resource, TArgument argument, IEnumerable<HttpStatusCode> overrideSuccessStatusCodes = null) where TResult : new();

        WebApiServiceResponse Put<TArgument>(string baseUrl, string resource, TArgument argument, IEnumerable<HttpStatusCode> overrideSuccessStatusCodes = null);
        WebApiServiceResponse Put(string baseUrl, string resource, dynamic argument = null, IEnumerable<HttpStatusCode> overrideSuccessStatusCodes = null);
        WebApiServiceResponse<TResult> Put<TResult>(string baseUrl, string resource, dynamic argument = null, IEnumerable<HttpStatusCode> overrideSuccessStatusCodes = null) where TResult : new();
        WebApiServiceResponse<TResult> Put<TResult, TArgument>(string baseUrl, string resource, TArgument argument, IEnumerable<HttpStatusCode> overrideSuccessStatusCodes = null) where TResult : new();

        WebApiServiceResponse Delete<TArgument>(string baseUrl, string resource, TArgument argument, IEnumerable<HttpStatusCode> overrideSuccessStatusCodes = null);
        WebApiServiceResponse Delete(string baseUrl, string resource, dynamic argument = null, IEnumerable<HttpStatusCode> overrideSuccessStatusCodes = null);
        WebApiServiceResponse<TResult> Delete<TResult>(string baseUrl, string resource, dynamic argument = null, IEnumerable<HttpStatusCode> overrideSuccessStatusCodes = null) where TResult : new();
        WebApiServiceResponse<TResult> Delete<TResult, TArgument>(string baseUrl, string resource, TArgument argument, IEnumerable<HttpStatusCode> overrideSuccessStatusCodes = null) where TResult : new();
    }
}
