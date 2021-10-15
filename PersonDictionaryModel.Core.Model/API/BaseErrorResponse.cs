namespace PersonDictionaryModel.Core.Model.API
{
    public class BaseErrorResponse
    {
        public string ErrorCode { get; set; }

        public string UserMessage { get; set; }
    }
}
