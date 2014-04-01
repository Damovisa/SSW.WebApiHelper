SSW.WebApiHelper
================

Helper package for Web API Service calls. Contains a WebApiService class intended to make calls to ASP.NET Web API
(and other RESTFUL HTTP services) easy to manage.

When instantiating, you're given the opportunity to supply *valid* HTTP Status Codes. All other statuses will result in a thrown `WebApiException`.

Default Settings
----------------
* HTTP Status codes in the 100 and 200 group are considered successful
* HTTP Status codes in the 300, 400, and 500 range will throw an exception
* Arguments passed to `GET` and `DELETE` methods will be included as URL parameters
* Arguments passed to `POST` and `PUT` methods will be serialized to JSON and included in the request body

*Note, the HTTP verb behaviours above are currently hard-coded. They will likely be configurable in later versions.*

Example Usage
-------------

### Super fun happy path - GET:
Get an object by specifying the type and passing an anonymous object as the argument.
```C#
var service = new WebApiService();
/*  GET http://localhost/api/Customer?Id=1  */
var response = service.Get<Customer>("http://localhost", "api/Customer", new { Id = 1 });
var myCustomer = response.Data;
```

### Super fun happy path - POST:
Post an object by passing the typed object as an argument
```C#
var service = new WebApiService();
/*  POST http://localhost/api/Customer  (body will include JSON-serialized myCustomer object)  */
var response = service.Post("http://localhost", "api/Customer", myCustomer);
var success = (response.StatusCode == HttpStatusCode.OK);
```

### Method Overloads:
Each of the Get, Post, Put, or Delete methods have overloads for the following:
* Pass a typed argument and return a `WebApiServiceResponse` with no data
* Pass a typed argument and return a `WebApiServiceResponse<>` with data of a particular type
* Pass an anonymous argument and return a `WebApiServiceResponse` with no data
* Pass an anonymous argument and return a `WebApiServiceResponse<>` with data of a particular type

