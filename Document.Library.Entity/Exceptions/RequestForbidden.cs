using Document.Library.Global.Enum;
using System;
using System.Net;
using System.Runtime.Serialization;

namespace Document.Library.Entity.Exceptions
{
    public class RequestForbidden : Exception
    {
        public HttpStatusCode _code = HttpStatusCode.Forbidden;
        public HttpStatusCode StatusCode
        {
            get
            {
                return _code;
            }
            set
            {
                _code = value;
            }
        }

        public ValidationStatusCode ValidationStatus { get; set; }

        public ErrorResponse ErrorResponse { get; set; }

        /// <summary>
        /// Just create the exception
        /// </summary>
        //    public RequestForbidden()
        //: base()
        //    {
        //    }

        /// <summary>
        /// Create the exception with description
        /// </summary>
        /// <param name="message">Exception description</param>
        public RequestForbidden(String message)
    : base(message)
        {
        }

        public RequestForbidden(ErrorResponse errorResponse)
  : base(errorResponse.Error.Message)
        {
            ErrorResponse = errorResponse;
        }
        /// <summary>
        /// Create the exception with description and inner cause
        /// </summary>
        /// <param name="message">Exception description</param>
        /// <param name="innerException">Exception inner cause</param>
        public RequestForbidden(String message, Exception innerException)
    : base(message, innerException)
        {
        }

        /// <summary>
        /// Create the exception from serialized data.
        /// Usual scenario is when exception is occured somewhere on the remote workstation
        /// and we have to re-create/re-throw the exception on the local machine
        /// </summary>
        /// <param name="info">Serialization info</param>
        /// <param name="context">Serialization context</param>
        protected RequestForbidden(SerializationInfo info, StreamingContext context)
    : base(info, context)
        {
        }
    }

}