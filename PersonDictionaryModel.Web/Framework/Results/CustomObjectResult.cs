using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace PersonDictionaryModel.Web.Framework.Results
{
    public class CustomObjectResult : ObjectResult
    {
        public CustomObjectResult(int statusCode, object data)
            : base(data)
        {
            StatusCode = statusCode;
        }

        public CustomObjectResult(HttpStatusCode httpStatusCode, object data)
            : base(data)
        {
            StatusCode = (int)httpStatusCode;
        }
    }
}
