using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Application.Common.Exceptions
{
    [Serializable]
    public class IdentityException : Exception
    {
       
        public IList<string> Failures { get; set; }

        public IdentityException() : base("One or more identity failures occurred.")
        {
            Failures = new List<string>();
        }

        protected IdentityException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }

        public IdentityException(IEnumerable<IdentityError> failures)
            : this()
        {
            var errors = failures
                .Select(e => e.Description)
                .Distinct()
                .ToArray();
            foreach (var error in errors)
            {
                Failures.Add(error);
            }
        }
        

    }
}
