using System;

namespace PersonDictionaryModel.Core.Model.Exceptions
{
    public class PersonDictionaryModelException : Exception
    {
        public string ErrorCode { get; set; }
        public string ModelName { get; set; }

        public PersonDictionaryModelException(string errorCode, string modelName)
        {
            ErrorCode = errorCode;
            ModelName = modelName;
        }
    }
}
