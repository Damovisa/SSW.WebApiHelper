using System.Collections.Generic;
using System.Net;

namespace SSW.WebApiHelper
{
    /// <summary>
    /// Helper for hitting an ASP.NET Web API or other RESTful HTTP service
    /// </summary>
    public interface IWebApiService
    {
        /// <summary>
        /// Returns the result of a HTTP GET request for a given resource
        /// </summary>
        /// <typeparam name="TArgument">The type of the argument passed to the service</typeparam>
        /// <param name="baseUrl">The base URL of the request</param>
        /// <param name="resource">The resource to use for this request. You can include route attribute-style {tokens} in this resource.</param>
        /// <param name="argument">The argument to pass to the resource</param>
        /// <param name="overrideSuccessStatusCodes">Http Status codes that are considered a successful request. If omitted, the default will apply</param>
        /// <returns>The response from the resource</returns>
        WebApiServiceResponse Get<TArgument>(string baseUrl, string resource, TArgument argument, IEnumerable<HttpStatusCode> overrideSuccessStatusCodes = null);

        /// <summary>
        /// Returns the result of a HTTP GET request for a given resource
        /// </summary>
        /// <param name="baseUrl">The base URL of the request</param>
        /// <param name="resource">The resource to use for this request. You can include route attribute-style {tokens} in this resource.</param>
        /// <param name="argument">The optional argument to pass to the resource</param>
        /// <param name="overrideSuccessStatusCodes">Http Status codes that are considered a successful request. If omitted, the default will apply</param>
        /// <returns>The response from the resource</returns>
        WebApiServiceResponse Get(string baseUrl, string resource, dynamic argument = null, IEnumerable<HttpStatusCode> overrideSuccessStatusCodes = null);

        /// <summary>
        /// Returns the result of a HTTP GET request for a given resource
        /// </summary>
        /// <typeparam name="TResult">The type of object to deserialize and return</typeparam>
        /// <param name="baseUrl">The base URL of the request</param>
        /// <param name="resource">The resource to use for this request. You can include route attribute-style {tokens} in this resource.</param>
        /// <param name="argument">The optional argument to pass to the resource</param>
        /// <param name="overrideSuccessStatusCodes">Http Status codes that are considered a successful request. If omitted, the default will apply</param>
        /// <returns>The response from the resource with a strongly-typed Data field</returns>
        WebApiServiceResponse<TResult> Get<TResult>(string baseUrl, string resource, dynamic argument = null, IEnumerable<HttpStatusCode> overrideSuccessStatusCodes = null) where TResult : new();

        /// <summary>
        /// Returns the result of a HTTP GET request for a given resource
        /// </summary>
        /// <typeparam name="TResult">The type of object to deserialize and return</typeparam>
        /// <typeparam name="TArgument">The type of the argument passed to the service</typeparam>
        /// <param name="baseUrl">The base URL of the request</param>
        /// <param name="resource">The resource to use for this request. You can include route attribute-style {tokens} in this resource.</param>
        /// <param name="argument">The argument to pass to the resource</param>
        /// <param name="overrideSuccessStatusCodes">Http Status codes that are considered a successful request. If omitted, the default will apply</param>
        /// <returns>The response from the resource with a strongly-typed Data field</returns>
        WebApiServiceResponse<TResult> Get<TResult, TArgument>(string baseUrl, string resource, TArgument argument, IEnumerable<HttpStatusCode> overrideSuccessStatusCodes = null) where TResult : new();



        /// <summary>
        /// Returns the result of a HTTP POST request for a given resource
        /// </summary>
        /// <typeparam name="TArgument">The type of the argument passed to the service</typeparam>
        /// <param name="baseUrl">The base URL of the request</param>
        /// <param name="resource">The resource to use for this request. You can include route attribute-style {tokens} in this resource.</param>
        /// <param name="argument">The argument to pass to the resource</param>
        /// <param name="overrideSuccessStatusCodes">Http Status codes that are considered a successful request. If omitted, the default will apply</param>
        /// <returns>The response from the resource</returns>
        WebApiServiceResponse Post<TArgument>(string baseUrl, string resource, TArgument argument, IEnumerable<HttpStatusCode> overrideSuccessStatusCodes = null);

        /// <summary>
        /// Returns the result of a HTTP POST request for a given resource
        /// </summary>
        /// <param name="baseUrl">The base URL of the request</param>
        /// <param name="resource">The resource to use for this request. You can include route attribute-style {tokens} in this resource.</param>
        /// <param name="argument">The optional argument to pass to the resource</param>
        /// <param name="overrideSuccessStatusCodes">Http Status codes that are considered a successful request. If omitted, the default will apply</param>
        /// <returns>The response from the resource</returns>
        WebApiServiceResponse Post(string baseUrl, string resource, dynamic argument = null, IEnumerable<HttpStatusCode> overrideSuccessStatusCodes = null);

        /// <summary>
        /// Returns the result of a HTTP POST request for a given resource
        /// </summary>
        /// <typeparam name="TResult">The type of object to deserialize and return</typeparam>
        /// <param name="baseUrl">The base URL of the request</param>
        /// <param name="resource">The resource to use for this request. You can include route attribute-style {tokens} in this resource.</param>
        /// <param name="argument">The optional argument to pass to the resource</param>
        /// <param name="overrideSuccessStatusCodes">Http Status codes that are considered a successful request. If omitted, the default will apply</param>
        /// <returns>The response from the resource with a strongly-typed Data field</returns>
        WebApiServiceResponse<TResult> Post<TResult>(string baseUrl, string resource, dynamic argument = null, IEnumerable<HttpStatusCode> overrideSuccessStatusCodes = null) where TResult : new();

        /// <summary>
        /// Returns the result of a HTTP POST request for a given resource
        /// </summary>
        /// <typeparam name="TResult">The type of object to deserialize and return</typeparam>
        /// <typeparam name="TArgument">The type of the argument passed to the service</typeparam>
        /// <param name="baseUrl">The base URL of the request</param>
        /// <param name="resource">The resource to use for this request. You can include route attribute-style {tokens} in this resource.</param>
        /// <param name="argument">The argument to pass to the resource</param>
        /// <param name="overrideSuccessStatusCodes">Http Status codes that are considered a successful request. If omitted, the default will apply</param>
        /// <returns>The response from the resource with a strongly-typed Data field</returns>
        WebApiServiceResponse<TResult> Post<TResult, TArgument>(string baseUrl, string resource, TArgument argument, IEnumerable<HttpStatusCode> overrideSuccessStatusCodes = null) where TResult : new();



        /// <summary>
        /// Returns the result of a HTTP PUT request for a given resource
        /// </summary>
        /// <typeparam name="TArgument">The type of the argument passed to the service</typeparam>
        /// <param name="baseUrl">The base URL of the request</param>
        /// <param name="resource">The resource to use for this request. You can include route attribute-style {tokens} in this resource.</param>
        /// <param name="argument">The argument to pass to the resource</param>
        /// <param name="overrideSuccessStatusCodes">Http Status codes that are considered a successful request. If omitted, the default will apply</param>
        /// <returns>The response from the resource</returns>
        WebApiServiceResponse Put<TArgument>(string baseUrl, string resource, TArgument argument, IEnumerable<HttpStatusCode> overrideSuccessStatusCodes = null);

        /// <summary>
        /// Returns the result of a HTTP PUT request for a given resource
        /// </summary>
        /// <param name="baseUrl">The base URL of the request</param>
        /// <param name="resource">The resource to use for this request. You can include route attribute-style {tokens} in this resource.</param>
        /// <param name="argument">The optional argument to pass to the resource</param>
        /// <param name="overrideSuccessStatusCodes">Http Status codes that are considered a successful request. If omitted, the default will apply</param>
        /// <returns>The response from the resource</returns>
        WebApiServiceResponse Put(string baseUrl, string resource, dynamic argument = null, IEnumerable<HttpStatusCode> overrideSuccessStatusCodes = null);

        /// <summary>
        /// Returns the result of a HTTP PUT request for a given resource
        /// </summary>
        /// <typeparam name="TResult">The type of object to deserialize and return</typeparam>
        /// <param name="baseUrl">The base URL of the request</param>
        /// <param name="resource">The resource to use for this request. You can include route attribute-style {tokens} in this resource.</param>
        /// <param name="argument">The optional argument to pass to the resource</param>
        /// <param name="overrideSuccessStatusCodes">Http Status codes that are considered a successful request. If omitted, the default will apply</param>
        /// <returns>The response from the resource with a strongly-typed Data field</returns>
        WebApiServiceResponse<TResult> Put<TResult>(string baseUrl, string resource, dynamic argument = null, IEnumerable<HttpStatusCode> overrideSuccessStatusCodes = null) where TResult : new();

        /// <summary>
        /// Returns the result of a HTTP PUT request for a given resource
        /// </summary>
        /// <typeparam name="TResult">The type of object to deserialize and return</typeparam>
        /// <typeparam name="TArgument">The type of the argument passed to the service</typeparam>
        /// <param name="baseUrl">The base URL of the request</param>
        /// <param name="resource">The resource to use for this request. You can include route attribute-style {tokens} in this resource.</param>
        /// <param name="argument">The argument to pass to the resource</param>
        /// <param name="overrideSuccessStatusCodes">Http Status codes that are considered a successful request. If omitted, the default will apply</param>
        /// <returns>The response from the resource with a strongly-typed Data field</returns>
        WebApiServiceResponse<TResult> Put<TResult, TArgument>(string baseUrl, string resource, TArgument argument, IEnumerable<HttpStatusCode> overrideSuccessStatusCodes = null) where TResult : new();



        /// <summary>
        /// Returns the result of a HTTP DELETE request for a given resource
        /// </summary>
        /// <typeparam name="TArgument">The type of the argument passed to the service</typeparam>
        /// <param name="baseUrl">The base URL of the request</param>
        /// <param name="resource">The resource to use for this request. You can include route attribute-style {tokens} in this resource.</param>
        /// <param name="argument">The argument to pass to the resource</param>
        /// <param name="overrideSuccessStatusCodes">Http Status codes that are considered a successful request. If omitted, the default will apply</param>
        /// <returns>The response from the resource</returns>
        WebApiServiceResponse Delete<TArgument>(string baseUrl, string resource, TArgument argument, IEnumerable<HttpStatusCode> overrideSuccessStatusCodes = null);
 
        /// <summary>
        /// Returns the result of a HTTP DELETE request for a given resource
        /// </summary>
        /// <param name="baseUrl">The base URL of the request</param>
        /// <param name="resource">The resource to use for this request. You can include route attribute-style {tokens} in this resource.</param>
        /// <param name="argument">The optional argument to pass to the resource</param>
        /// <param name="overrideSuccessStatusCodes">Http Status codes that are considered a successful request. If omitted, the default will apply</param>
        /// <returns>The response from the resource</returns>
        WebApiServiceResponse Delete(string baseUrl, string resource, dynamic argument = null, IEnumerable<HttpStatusCode> overrideSuccessStatusCodes = null);

        /// <summary>
        /// Returns the result of a HTTP DELETE request for a given resource
        /// </summary>
        /// <typeparam name="TResult">The type of object to deserialize and return</typeparam>
        /// <param name="baseUrl">The base URL of the request</param>
        /// <param name="resource">The resource to use for this request. You can include route attribute-style {tokens} in this resource.</param>
        /// <param name="argument">The optional argument to pass to the resource</param>
        /// <param name="overrideSuccessStatusCodes">Http Status codes that are considered a successful request. If omitted, the default will apply</param>
        /// <returns>The response from the resource with a strongly-typed Data field</returns>
        WebApiServiceResponse<TResult> Delete<TResult>(string baseUrl, string resource, dynamic argument = null, IEnumerable<HttpStatusCode> overrideSuccessStatusCodes = null) where TResult : new();

        /// <summary>
        /// Returns the result of a HTTP DELETE request for a given resource
        /// </summary>
        /// <typeparam name="TResult">The type of object to deserialize and return</typeparam>
        /// <typeparam name="TArgument">The type of the argument passed to the service</typeparam>
        /// <param name="baseUrl">The base URL of the request</param>
        /// <param name="resource">The resource to use for this request. You can include route attribute-style {tokens} in this resource.</param>
        /// <param name="argument">The argument to pass to the resource</param>
        /// <param name="overrideSuccessStatusCodes">Http Status codes that are considered a successful request. If omitted, the default will apply</param>
        /// <returns>The response from the resource with a strongly-typed Data field</returns>
        WebApiServiceResponse<TResult> Delete<TResult, TArgument>(string baseUrl, string resource, TArgument argument, IEnumerable<HttpStatusCode> overrideSuccessStatusCodes = null) where TResult : new();
    }
}
