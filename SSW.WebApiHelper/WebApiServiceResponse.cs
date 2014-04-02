using System;
using System.Net;
using Newtonsoft.Json;
using RestSharp;

namespace SSW.WebApiHelper
{
    /// <summary>
    /// Class that represents a service response with no deserialized data
    /// </summary>
    public class WebApiServiceResponse
    {
        /// <summary>
        /// The string content returned in the body of the HTTP response
        /// </summary>
        public string Content { get; private set; }
        
        /// <summary>
        /// The HTTP Status Code returned
        /// </summary>
        public HttpStatusCode StatusCode { get; private set; }
        
        /// <summary>
        /// The URI that produced the response
        /// </summary>
        public Uri Uri { get; set; }

        public WebApiServiceResponse(IRestResponse response, Uri uri)
        {
            Content = response.Content;
            StatusCode = response.StatusCode;
            Uri = uri;
        }
    }

    /// <summary>
    /// Class that represents a service response with strongly-typed deserialized data
    /// </summary>
    public class WebApiServiceResponse<T> : WebApiServiceResponse
    {
        /// <summary>
        /// The strongly-typed object deserialized from the HTTP request
        /// </summary>
        public T Data { get; private set; }

        public WebApiServiceResponse(IRestResponse response, Uri uri) : base(response, uri)
        {
            Data = JsonConvert.DeserializeObject<T>(response.Content);
        }
    }
}