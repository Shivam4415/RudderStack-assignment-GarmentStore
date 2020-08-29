using Document.Library.Entity;
using Document.Library.Globals.Enum;
using System;
using System.Net;
using System.Runtime.Serialization;

namespace Document.Library.Entity.Exceptions
{
    public class NotFound : Exception
    {
        public HttpStatusCode _code = HttpStatusCode.NotFound;
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
        public ErrorResponse ErrorResponse { get; set; }
        /// <summary>
        /// Just create the exception
        /// </summary>
        public NotFound()
    : base(Messages.NotFound)
        {
            ErrorResponse = new ErrorResponse(ErrorResponseCode.InvalidRequestError, Messages.NotFound);
        }

        /// <summary>
        /// Create the exception with description
        /// </summary>
        /// <param name="message">Exception description</param>
        public NotFound(String message)
    : base(message)
        {
            ErrorResponse = new ErrorResponse(ErrorResponseCode.InvalidRequestError, message);
        }

        /// <summary>
        /// Create the exception with description and inner cause
        /// </summary>
        /// <param name="message">Exception description</param>
        /// <param name="innerException">Exception inner cause</param>
        public NotFound(String message, Exception innerException)
        : base(message, innerException)
        {
            ErrorResponse = new ErrorResponse(ErrorResponseCode.InvalidRequestError, message);
        }

        /// <summary>
        /// Create the exception from serialized data.
        /// Usual scenario is when exception is occured somewhere on the remote workstation
        /// and we have to re-create/re-throw the exception on the local machine
        /// </summary>
        /// <param name="info">Serialization info</param>
        /// <param name="context">Serialization context</param>
        protected NotFound(SerializationInfo info, StreamingContext context)
        : base(info, context)
        {
        }
    }

}