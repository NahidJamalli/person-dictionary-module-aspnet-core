using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using PersonDictionaryModel.Core.Model.API;
using PersonDictionaryModel.Core.Model.Exceptions;
using PersonDictionaryModel.Resources;
using PersonDictionaryModel.Resources.Resources;
using PersonDictionaryModel.Web.Framework.Results;
using System.Net;

namespace PersonDictionaryModel.Web.Filters
{
    public class GlobalExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public IStringLocalizer<SharedResource> _localizer { get; }
        private ILogger<GlobalExceptionFilterAttribute> _logger;

        public GlobalExceptionFilterAttribute(
            IStringLocalizer<SharedResource> localizer,
            ILogger<GlobalExceptionFilterAttribute> logger)
        {
            _localizer = localizer;
            _logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            int statusCode = (int)HttpStatusCode.InternalServerError;
            var errorCode = ApiErrorCodeKeys.E10001;
            string technicalErrorMessage = context.Exception.Message.ToString();
            var response = new ErrorResponse();


            if (context.Exception is PersonDictionaryModelException)
            {
                var validationException = context.Exception as PersonDictionaryModelException;

                errorCode = validationException.ErrorCode;
                var modelName = validationException.ModelName;

                var errorDesc = _localizer.GetString(errorCode, modelName);

                response.UserMessage = errorDesc;
                response.ErrorCode = errorCode;
                response.AppMessage = technicalErrorMessage;
                statusCode = (int)GetStatusCode(errorCode);

                _logger.LogError("Validation error occured", context.Exception);
            }
            else
            {
                statusCode = (int)HttpStatusCode.InternalServerError;
                response.AppMessage = technicalErrorMessage;
                response.UserMessage = "Unhandled error occured";
                response.ErrorCode = errorCode;

                _logger.LogError(response.UserMessage, context.Exception);
            }


            context.Result = new CustomObjectResult(statusCode, response);
            context.ExceptionHandled = true;
        }


        private HttpStatusCode GetStatusCode(string errorCode)
        {
            switch (errorCode)
            {
                case ApiErrorCodeKeys.E10004:
                    return HttpStatusCode.NotFound;
                default:
                    return HttpStatusCode.InternalServerError;
            }
        }
    }
}
