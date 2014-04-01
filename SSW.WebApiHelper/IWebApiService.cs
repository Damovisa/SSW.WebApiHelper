namespace SSW.WebApiHelper
{
    public interface IWebApiService
    {
        WebApiServiceResponse Get<TArgument>(string baseUrl, string resource, TArgument argument);
        WebApiServiceResponse Get(string baseUrl, string resource, dynamic argument);
        WebApiServiceResponse<TResult> Get<TResult>(string baseUrl, string resource, dynamic argument = null) where TResult : new();
        WebApiServiceResponse<TResult> Get<TResult, TArgument>(string baseUrl, string resource, TArgument argument) where TResult : new();

        WebApiServiceResponse Post<TArgument>(string baseUrl, string resource, TArgument argument);
        WebApiServiceResponse Post(string baseUrl, string resource, dynamic argument);
        WebApiServiceResponse<TResult> Post<TResult>(string baseUrl, string resource, dynamic argument = null) where TResult : new();
        WebApiServiceResponse<TResult> Post<TResult, TArgument>(string baseUrl, string resource, TArgument argument) where TResult : new();

        WebApiServiceResponse Put<TArgument>(string baseUrl, string resource, TArgument argument);
        WebApiServiceResponse Put(string baseUrl, string resource, dynamic argument);
        WebApiServiceResponse<TResult> Put<TResult>(string baseUrl, string resource, dynamic argument = null) where TResult : new();
        WebApiServiceResponse<TResult> Put<TResult, TArgument>(string baseUrl, string resource, TArgument argument) where TResult : new();

        WebApiServiceResponse Delete<TArgument>(string baseUrl, string resource, TArgument argument);
        WebApiServiceResponse Delete(string baseUrl, string resource, dynamic argument);
        WebApiServiceResponse<TResult> Delete<TResult>(string baseUrl, string resource, dynamic argument = null) where TResult : new();
        WebApiServiceResponse<TResult> Delete<TResult, TArgument>(string baseUrl, string resource, TArgument argument) where TResult : new();
    }
}
