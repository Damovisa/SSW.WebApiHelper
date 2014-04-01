using System;
using System.Net;
using Microsoft.Owin.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SSW.WebApiHelper.Test
{
    [TestClass]
    public class WebApiServiceTests
    {
        private const string Url = "http://localhost:9999";
        private IDisposable _webApp;

        [TestInitialize]
        public void Initialize()
        {
            _webApp = WebApp.Start<Startup>(Url);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _webApp.Dispose();
        }

        [TestMethod]
        public void TestWebApiUrlParameterPost()
        {
            var service = new WebApiService();
            var obj = new
            {
                FirstName = "Bob",
                LastName = "Smith",
                Address = new { Line1 = "1 Smith St", Line2 = "Brisbane" }
            };
            try
            {
                var response = service.Post(Url, "api/testUrlParameterPost/{FirstName}/{LastName}", obj);
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }
            catch (Exception ex)
            {
                Assert.Fail("Exception thrown by Post: {0}", ex);
            }
        }

        [TestMethod]
        public void TestWebApiQueryStringParameterGet()
        {
            var service = new WebApiService();
            var obj = new ObjectWithId
            {
                Id = "123"
            };
            try
            {
                var result = service.Get<ObjectWithId,ObjectWithId>(Url, "api/testQueryStringGet", obj);
                Assert.AreEqual(obj.Id, result.Data.Id);
            }
            catch (Exception ex)
            {
                Assert.Fail("Exception thrown by Get: {0}", ex);
            }
        }

        [TestMethod]
        public void TestSpecificAcceptableResponses()
        {
            var service = new WebApiService(HttpStatusCode.OK); // OK is the only acceptable code
            var obj = new ObjectWithId
            {
                Id = "123"
            };
            try
            {
                var response = service.Get<ObjectWithSuccess, ObjectWithId>(Url, "api/test200Get", obj);
                Assert.IsTrue(response.Data.Success);
            }
            catch (Exception ex)
            {
                Assert.Fail("Exception thrown by Get: {0}", ex);
            }

            try
            {
                service.Get<ObjectWithSuccess, ObjectWithId>(Url, "api/test202Get", obj);
                Assert.Fail("Object returned when exception should have been thrown from a 202");
            }
            catch (WebApiException ex)
            {
                Assert.AreEqual(HttpStatusCode.Accepted,ex.StatusCode);
            }

            try
            {
                service.Get<ObjectWithSuccess, ObjectWithId>(Url, "api/test300Get", obj);
                Assert.Fail("Object returned when exception should have been thrown from a 300");
            }
            catch (WebApiException ex)
            {
                Assert.AreEqual(HttpStatusCode.MultipleChoices, ex.StatusCode);
            }
        }

        [TestMethod]
        public void TestGroupAcceptableResponses()
        {
            var service = new WebApiService(false, true, false); // Any 200s are fine
            var obj = new ObjectWithId
            {
                Id = "123"
            };
            try
            {
                var response = service.Get<ObjectWithSuccess, ObjectWithId>(Url, "api/test200Get", obj);
                Assert.IsTrue(response.Data.Success);
            }
            catch (Exception ex)
            {
                Assert.Fail("Exception thrown by Get: {0}", ex);
            }

            try
            {
                var response = service.Get<ObjectWithSuccess, ObjectWithId>(Url, "api/test202Get", obj);
                Assert.IsTrue(response.Data.Success);
            }
            catch (WebApiException ex)
            {
                Assert.Fail("Exception thrown by Get 202: {0}", ex);
            }

            try
            {
                service.Get<ObjectWithSuccess, ObjectWithId>(Url, "api/test300Get", obj);
                Assert.Fail("Object returned when exception should have been thrown from a 300");
            }
            catch (WebApiException ex)
            {
                Assert.AreEqual(HttpStatusCode.MultipleChoices, ex.StatusCode);
            }
        }

        [TestMethod]
        public void TestNoArgumentGet()
        {
            var service = new WebApiService();

            try
            {
                var response = service.Get<ObjectWithSuccess>(Url, "api/testNoArgumentGet");
                Assert.IsTrue(response.Data.Success);
            }
            catch (WebApiException ex)
            {
                Assert.Fail("Exception thrown by Get with no argument: {0}", ex);
            }
        }

        [TestMethod]
        public void TestDynamicArgumentGet()
        {
            var service = new WebApiService();
            try
            {
                var response = service.Get<ObjectWithSuccess>(Url, "api/testObjectArgumentGet/{FirstName}", new {FirstName = "John", LastName="Smith"});
                Assert.IsTrue(response.Data.Success);
            }
            catch (WebApiException ex)
            {
                Assert.Fail("Exception thrown by Get with object argument: {0}", ex);
            }
        }

        [TestMethod]
        public void TestDynamicVoidArgumentGet()
        {
            var service = new WebApiService();
            try
            {
                var response = service.Get(Url, "api/testObjectArgumentGet/{FirstName}", new { FirstName = "John", LastName = "Smith" });
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }
            catch (WebApiException ex)
            {
                Assert.Fail("Exception thrown by Get with object argument: {0}", ex);
            }
        }

        [TestMethod]
        public void TestDynamicArgumentPost()
        {
            var service = new WebApiService();
            try
            {
                var response = service.Get<ObjectWithSuccess>(Url, "api/testObjectArgumentPost/{FirstName}", new { FirstName = "John", LastName = "Smith" });
                Assert.IsTrue(response.Data.Success);
            }
            catch (WebApiException ex)
            {
                Assert.Fail("Exception thrown by Post with object argument: {0}", ex);
            }
        }

        [TestMethod]
        public void TestDynamicVoidArgumentPost()
        {
            var service = new WebApiService();
            try
            {
                var response = service.Get(Url, "api/testObjectArgumentPost/{FirstName}", new { FirstName = "John", LastName = "Smith" });
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }
            catch (WebApiException ex)
            {
                Assert.Fail("Exception thrown by Post with object argument: {0}", ex);
            }
        }
    }

    public class ObjectWithId
    {
        public string Id { get; set; }
    }

    public class ObjectWithSuccess
    {
        public bool Success { get; set; }
    }

    public class ObjectWithNames
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
