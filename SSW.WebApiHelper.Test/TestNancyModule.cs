using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nancy;
using Nancy.ModelBinding;

namespace SSW.WebApiHelper.Test
{
    public class TestNancyModule : NancyModule
    {
        public TestNancyModule()
        {
            Post["/api/testUrlParameterPost/{FirstName}/{LastName}"] = _ =>
            {
                if (_.FirstName == null || _.LastName == null)
                {
                    Assert.Fail("No FirstName or LastName received");
                }
                return new {_.FirstName, Success = true};
            };

            Get["/api/testQueryStringGet"] = _ =>
            {
                if (Request.Query.Id == null)
                {
                    Assert.Fail("No Id received");
                }
                return new {Request.Query.Id, Success = true};
            };

            Get["/api/Test200Get"] = _ => Response.AsJson(new {Success = true}, Nancy.HttpStatusCode.OK);

            Get["/api/Test202Get"] = _ => Response.AsJson(new {Success = true}, Nancy.HttpStatusCode.Accepted);

            Get["/api/Test300Get"] = _ => Response.AsJson(new {Success = true}, Nancy.HttpStatusCode.MultipleChoices);

            Get["api/testNoArgumentGet"] = _ => new {Success = true};

            Get["api/testObjectArgumentGet/{FirstName}"] = _ =>
            {
                if (_.FirstName == null || Request.Query.LastName == null)
                {
                    Assert.Fail("No FirstName or LastName provided");
                }
                return new {Success = true};
            };

            Get["api/testObjectArgumentPost/{FirstName}"] = _ =>
            {
                var obj = this.Bind<ObjectWithNames>();
                if (_.FirstName == null || string.IsNullOrEmpty(obj.FirstName) || string.IsNullOrEmpty(obj.LastName))
                {
                    Assert.Fail("No FirstName or LastName provided");
                }
                return new { Success = true };
            };

            Get["api/testNullDynamicWithStringResponse"] = _ => "String response";
        }
    }
}