using System;
using System.Net;

namespace SSW.WebApiHelper
{
    /// <summary>
    /// Exception thrown by the WebApiService.
    /// Usually because a resource returned an HTTP status code that was not configured to represent success
    /// </summary>
    public class WebApiException : Exception
    {
        /// <summary>
        /// The HTTP Status code returned by the request
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }
        
        /// <summary>
        /// The string content returned by the HTTP request
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// The URL that was hit by the request
        /// </summary>
        public string Url { get; set; }

        public WebApiException(HttpStatusCode statusCode, string content, string url, Exception innerException)
            : base(string.Format("Url: {0}, Status Code: {1}, Content: {2}", url, statusCode, content), innerException)
        {
            StatusCode = statusCode;
            Content = content;
            Url = url;
        }
    }
}
