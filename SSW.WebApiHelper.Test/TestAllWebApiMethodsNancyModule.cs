using FluentAssertions;
using Nancy;
using Nancy.ModelBinding;

namespace SSW.WebApiHelper.Test
{
    public class TestAllWebApiMethodsNancyModule : NancyModule
    {
        public TestAllWebApiMethodsNancyModule()
        {
            /* GET */
            Get["all/GetWithTypedArgument"] = _ =>
            {
                CheckTypedQueryStringArgument(Request);
                return HttpStatusCode.OK;
            };
            Get["all/GetWithDynamicArgument"] = _ =>
            {
                CheckDynamicArgument(Request);
                return HttpStatusCode.OK;
            };
            Get["all/GetWithTypedResultAndArgument"] = _ =>
            {
                CheckTypedQueryStringArgument(Request);
                return GetTypedResponse();
            };
            Get["all/GetWithTypedResultAndDynamicArgument"] = _ =>
            {
                CheckDynamicArgument(Request);
                return GetTypedResponse();
            };

            /* POST */
            Post["all/PostWithTypedArgument"] = _ =>
            {
                CheckTypedJsonArgument(this.Bind<TypedArgument>());
                return HttpStatusCode.OK;
            };
            Post["all/PostWithDynamicArgument"] = _ =>
            {
                CheckTypedDynamicJsonArgument(this.Bind<TypedArgument>());
                return HttpStatusCode.OK;
            };
            Post["all/PostWithTypedResultAndArgument"] = _ =>
            {
                CheckTypedJsonArgument(this.Bind<TypedArgument>());
                return GetTypedResponse();
            };
            Post["all/PostWithTypedResultAndDynamicArgument"] = _ =>
            {
                CheckTypedDynamicJsonArgument(this.Bind<TypedArgument>());
                return GetTypedResponse();
            };

            /* PUT */
            Put["all/PutWithTypedArgument"] = _ =>
            {
                CheckTypedJsonArgument(this.Bind<TypedArgument>());
                return HttpStatusCode.OK;
            };
            Put["all/PutWithDynamicArgument"] = _ =>
            {
                CheckTypedDynamicJsonArgument(this.Bind<TypedArgument>());
                return HttpStatusCode.OK;
            };
            Put["all/PutWithTypedResultAndArgument"] = _ =>
            {
                CheckTypedJsonArgument(this.Bind<TypedArgument>());
                return GetTypedResponse();
            };
            Put["all/PutWithTypedResultAndDynamicArgument"] = _ =>
            {
                CheckTypedDynamicJsonArgument(this.Bind<TypedArgument>());
                return GetTypedResponse();
            };

            /* DELETE */
            Delete["all/DeleteWithTypedArgument"] = _ =>
            {
                CheckTypedQueryStringArgument(Request);
                return HttpStatusCode.OK;
            };
            Delete["all/DeleteWithDynamicArgument"] = _ =>
            {
                CheckDynamicArgument(Request);
                return HttpStatusCode.OK;
            };
            Delete["all/DeleteWithTypedResultAndArgument"] = _ =>
            {
                CheckTypedQueryStringArgument(Request);
                return GetTypedResponse();
            };
            Delete["all/DeleteWithTypedResultAndDynamicArgument"] = _ =>
            {
                CheckDynamicArgument(Request);
                return GetTypedResponse();
            };
        }

        private void CheckTypedQueryStringArgument(Request request)
        {
            int id = request.Query.Id;
            id.ShouldBeEquivalentTo(1);
            string firstName = request.Query.FirstName;
            firstName.ShouldBeEquivalentTo("John");
        }

        private void CheckDynamicArgument(Request request)
        {
            int id = request.Query.Id;
            id.ShouldBeEquivalentTo(1);
        }

        private void CheckTypedJsonArgument(TypedArgument argument)
        {
            argument.Id.ShouldBeEquivalentTo(1);
            argument.FirstName.ShouldBeEquivalentTo("John");
            argument.LastName.ShouldBeEquivalentTo("Smith");
            argument.Collection.ShouldBeEquivalentTo(new[] { 1, 2, 3 }); // not sure about this one
        }

        private void CheckTypedDynamicJsonArgument(TypedArgument argument)
        {
            argument.Id.ShouldBeEquivalentTo(1);
        }

        private TypedResponse GetTypedResponse()
        {
            return new TypedResponse { Id = 1, Success = true, Collection = new[] { "one", "two" } };
        }
    }
}
