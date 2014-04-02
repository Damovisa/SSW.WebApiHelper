using System;
using System.Net;
using Newtonsoft.Json;
using RestSharp;

namespace SSW.WebApiHelper
{
    public class WebApiServiceResponse
    {
        public string Content { get; private set; }
        public HttpStatusCode StatusCode { get; private set; }
        public Uri Uri { get; set; }

        public WebApiServiceResponse(IRestResponse response, Uri uri)
        {
            Content = response.Content;
            StatusCode = response.StatusCode;
            Uri = uri;
        }
    }

    public class WebApiServiceResponse<T> : WebApiServiceResponse
    {
        public T Data { get; private set; }

        public WebApiServiceResponse(IRestResponse response, Uri uri) : base(response, uri)
        {
            Data = JsonConvert.DeserializeObject<T>(response.Content);
        }
    }
}