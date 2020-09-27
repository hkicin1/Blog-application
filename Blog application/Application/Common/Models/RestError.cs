using System.Collections.Generic;

namespace Application.Common.Models
{
    public class RestError
    {
        public string Error { get; }
        public IList<string> ValidationErrors { get; }
        public RestError(string error)
        {
            Error = error;
            ValidationErrors = new List<string>();
        }
        public RestError(string error, IList<string> errors)
        {
            Error = error;
            ValidationErrors = errors;
        }
    }
}
