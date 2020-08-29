using Document.Library.Entity;
using Document.Library.Entity.Exceptions;
using Document.Library.Globals.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Document.App.Api.CustomResponse
{
    public static class ExceptionResponse
    {

        public static HttpResponseException ServerErrorResponse(HttpRequestMessage request)
        {
            return new HttpResponseException(request.CreateResponse(HttpStatusCode.InternalServerError, new ErrorResponse(ErrorResponseCode.ApiError, Messages.InternalServerError)));
        }

        public static HttpResponseException Forbidden(HttpRequestMessage request, string message, ErrorResponseCode type = ErrorResponseCode.UserAccessDenied)
        {
            return Forbidden(request, new ErrorResponse(type, message));
        }

        public static HttpResponseException Forbidden(HttpRequestMessage request, ErrorResponse errorResponse)
        {
            return new HttpResponseException(request.CreateResponse(HttpStatusCode.Forbidden, errorResponse));
        }

        public static HttpResponseException NotFound(HttpRequestMessage request, string message)
        {
            return new HttpResponseException(request.CreateResponse(HttpStatusCode.NotFound, new ErrorResponse(ErrorResponseCode.InvalidRequestError, message)));
        }

        public static HttpResponseException BadRequest(HttpRequestMessage request, string message, string param = null)
        {
            return new HttpResponseException(request.CreateResponse(HttpStatusCode.BadRequest, new ErrorResponse(ErrorResponseCode.InvalidRequestError, message, param)));
        }

        public static HttpResponseException UnAuthorized(HttpRequestMessage request, string message, string param = null)
        {
            return new HttpResponseException(request.CreateResponse(HttpStatusCode.Unauthorized, new ErrorResponse(ErrorResponseCode.AuthenticationError, message, param)));
        }

        //public static HttpResponseException Exception(HttpRequestMessage request, HttpStatusCode status, ErrorResponseCode type, string message)
        //{
        //    return new HttpResponseException(request.CreateResponse(status, new ErrorResponse(type, message)));
        //}
        public static HttpResponseException Exception(HttpRequestMessage request, HttpStatusCode status, ErrorResponse errorResponse)
        {
            return new HttpResponseException(request.CreateResponse(status, errorResponse));
        }

        //public static HttpResponseException Exception(HttpRequestMessage request, ValidationResult result)
        //{
        //    return new HttpResponseException(request.CreateResponse(result.HttpStatusCode, result.ErrorResponse));
        //}


        //public static HttpResponseException GetResponseException(HttpRequestMessage request, HttpStatusCode status, ErrorResponse errorResponse)
        //{
        //    return new HttpResponseException(request.CreateResponse(status, errorResponse));
        //}
    }

}