using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Application.Common.Exceptions
{
    [Serializable]
    public class RequestValidationException : Exception
    {
        public IList<string> Failures { get; }

        public RequestValidationException() : base("One or more validation failures have occurred.")
        {
            Failures = new List<string>();
        }

        protected RequestValidationException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }

        public RequestValidationException(IReadOnlyCollection<ValidationFailure> failures)
            : this()
        {
            var propertyNames = failures
                .Select(e => e.PropertyName)
                .Distinct();
            foreach (var propertyName in propertyNames)
            {
                var propertyFailures = failures
                    .Where(e => e.PropertyName == propertyName)
                    .Select(e => e.ErrorMessage)
                    .ToArray();
                foreach (var error in propertyFailures)
                {
                    Failures.Add(error);
                }
            }
        }
    }
}
