using System;
using System.Collections.Generic;
using FluentAssertions;
using Microsoft.Owin.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;

namespace SSW.WebApiHelper.Test
{
    [TestClass]
    public class TestAllWebApiMethods
    {
        private const string Url = "http://localhost:9999";
        private IDisposable _webApp;
        private TypedArgument _argument;
        private TypedResponse _expectedResponse;
        private TypedArgumentSimple _simpleArgument;

        [TestInitialize]
        public void Initialize()
        {
            _webApp = WebApp.Start<Startup>(Url);
            _argument = new TypedArgument {Id = 1, FirstName = "John", LastName = "Smith", Collection = new[] {1, 2, 3}};
            _simpleArgument = new TypedArgumentSimple {Id = 1, FirstName = "John"};
            _expectedResponse = new TypedResponse {Id = 1, Success = true, Collection = new[] {"one","two"}};
        }

        [TestCleanup]
        public void Cleanup()
        {
            _webApp.Dispose();
        }


        /* GET */

        [TestMethod]
        public void TestVoidGet_WithTypedArgument()
        {
            // (string baseUrl, string resource, TArgument argument
            var service = new WebApiService();
            var response = service.Get(Url, "all/GetWithTypedArgument", _simpleArgument);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void TestVoidGetWithDynamicArgument()
        {
            // (string baseUrl, string resource, dynamic argument
            var service = new WebApiService();
            var response = service.Get(Url, "all/GetWithDynamicArgument", new {Id = 1});

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void TestResultGet_WithTypedResultAndArgument()
        {
            // (string baseUrl, string resource, TArgument argument
            var service = new WebApiService();
            var response = service.Get<TypedResponse, TypedArgumentSimple>(Url, "all/GetWithTypedResultAndArgument", _simpleArgument);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            response.Data.ShouldBeEquivalentTo(_expectedResponse);
        }

        [TestMethod]
        public void TestResultGet_WithDynamicArgumentAndTypedResult()
        {
            // (string baseUrl, string resource, dynamic argument = null
            var service = new WebApiService();
            var response = service.Get<TypedResponse>(Url, "all/GetWithTypedResultAndDynamicArgument", new {Id = 1});

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            response.Data.ShouldBeEquivalentTo(_expectedResponse);
        }


        /* POST */

        [TestMethod]
        public void TestVoidPost_WithTypedArgument()
        {
            // (string baseUrl, string resource, TArgument argument
            var service = new WebApiService();
            var response = service.Post(Url, "all/PostWithTypedArgument", _argument);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void TestVoidPostWithDynamicArgument()
        {
            // (string baseUrl, string resource, dynamic argument
            var service = new WebApiService();
            var response = service.Post(Url, "all/PostWithDynamicArgument", new { Id = 1 });

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void TestResultPost_WithTypedResultAndArgument()
        {
            // (string baseUrl, string resource, TArgument argument
            var service = new WebApiService();
            var response = service.Post<TypedResponse, TypedArgument>(Url, "all/PostWithTypedResultAndArgument", _argument);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            response.Data.ShouldBeEquivalentTo(_expectedResponse);
        }

        [TestMethod]
        public void TestResultPost_WithDynamicArgumentAndTypedResult()
        {
            // (string baseUrl, string resource, dynamic argument = null
            var service = new WebApiService();
            var response = service.Post<TypedResponse>(Url, "all/PostWithTypedResultAndDynamicArgument", new { Id = 1 });

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            response.Data.ShouldBeEquivalentTo(_expectedResponse);
        }

        
        /* PUT */

        [TestMethod]
        public void TestVoidPut_WithTypedArgument()
        {
            // (string baseUrl, string resource, TArgument argument
            var service = new WebApiService();
            var response = service.Put(Url, "all/PutWithTypedArgument", _argument);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void TestVoidPutWithDynamicArgument()
        {
            // (string baseUrl, string resource, dynamic argument
            var service = new WebApiService();
            var response = service.Put(Url, "all/PutWithDynamicArgument", new { Id = 1 });

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void TestResultPut_WithTypedResultAndArgument()
        {
            // (string baseUrl, string resource, TArgument argument
            var service = new WebApiService();
            var response = service.Put<TypedResponse, TypedArgument>(Url, "all/PutWithTypedResultAndArgument", _argument);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            response.Data.ShouldBeEquivalentTo(_expectedResponse);
        }

        [TestMethod]
        public void TestResultPut_WithDynamicArgumentAndTypedResult()
        {
            // (string baseUrl, string resource, dynamic argument = null
            var service = new WebApiService();
            var response = service.Put<TypedResponse>(Url, "all/PutWithTypedResultAndDynamicArgument", new { Id = 1 });

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            response.Data.ShouldBeEquivalentTo(_expectedResponse);
        }


        /* DELETE */

        [TestMethod]
        public void TestVoidDelete_WithTypedArgument()
        {
            // (string baseUrl, string resource, TArgument argument
            var service = new WebApiService();
            var response = service.Delete(Url, "all/DeleteWithTypedArgument", _simpleArgument);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void TestVoidDeleteWithDynamicArgument()
        {
            // (string baseUrl, string resource, dynamic argument
            var service = new WebApiService();
            var response = service.Delete(Url, "all/DeleteWithDynamicArgument", new { Id = 1 });

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void TestResultDelete_WithTypedResultAndArgument()
        {
            // (string baseUrl, string resource, TArgument argument
            var service = new WebApiService();
            var response = service.Delete<TypedResponse, TypedArgumentSimple>(Url, "all/DeleteWithTypedResultAndArgument", _simpleArgument);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            response.Data.ShouldBeEquivalentTo(_expectedResponse);
        }

        [TestMethod]
        public void TestResultDelete_WithDynamicArgumentAndTypedResult()
        {
            // (string baseUrl, string resource, dynamic argument = null
            var service = new WebApiService();
            var response = service.Delete<TypedResponse>(Url, "all/DeleteWithTypedResultAndDynamicArgument", new { Id = 1 });

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            response.Data.ShouldBeEquivalentTo(_expectedResponse);
        }
    }

    public class TypedArgument
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IEnumerable<int> Collection { get; set; }
    }

    public class TypedArgumentSimple
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
    }

    public class TypedResponse
    {
        public int Id { get; set; }
        public bool Success { get; set; }
        public IEnumerable<string> Collection { get; set; }
    }
}