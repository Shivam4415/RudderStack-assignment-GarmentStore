using Document.App.Api.CustomResponse;
using Document.App.App_Start;
using Document.Library.Entity;
using Document.Library.Entity.Exceptions;
using Document.Library.Globals.Enum;
using Document.Library.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Document.App.Api.v1
{
    [Route("api/v1/users")]
    public class UsersController : ApiController
    {

        public void Post([FromBody]UserProfile user)
        {
            try
            {
                UserProfile _user = AuthManager.CurrentUser;
                if(_user != null)
                    throw new RequestForbidden(new ErrorResponse(ErrorResponseCode.UserAccessDenied, Messages.RequestForbidden));

                if (user == null)
                    throw new RequestForbidden(new ErrorResponse(ErrorResponseCode.InvalidRequestError, Messages.InvalidRequest));

                UserServices.Add(user.Name, user.Email, user.Phone, user.Password, user.ConfirmPassword, user.Company);

            }
            catch
            {

            }
        }
    }
}