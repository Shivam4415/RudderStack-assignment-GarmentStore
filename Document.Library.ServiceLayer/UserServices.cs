using Document.Library.Entity;
using Document.Library.Entity.Exceptions;
using Document.Library.Globals;
using Document.Library.Globals.Enum;
using Document.Library.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document.Library.ServiceLayer
{
    public static class UserServices
    {
        public static UserProfile Add(string name, string email, string phone, string password, string confirmPassword, string company)
        {
            try
            {

                bool isValidEmail = Validator.IsValidEmail(email);

                if (!isValidEmail)
                {
                    throw new RequestForbidden(new ErrorResponse(ErrorResponseCode.InvalidRequestError, Messages.InvalidEmail));
                }

                bool isValidPassword = Validator.IsValidPassword(password);
                if(!isValidPassword)
                {
                    throw new RequestForbidden(new ErrorResponse(ErrorResponseCode.UserAccessDenied, Messages.InvalidPassword));
                }

                if(IsUserExists(email))
                {
                    throw new RequestForbidden(new ErrorResponse(ErrorResponseCode.UserAccessDenied, Messages.RegisterIfEmailExists));
                }

                //User is verified by default. Need to add code to send verification link to user.
                UserProfile _user = new UserRepository().Add(name, email, phone, password, company);
                

                if(_user == null)
                {
                    throw new Exception(Messages.InternalServerError);
                }

                EmailServices.Send(email, "", "user registered successfully", "Please click this link to verify");


                //SharingServices.UpdateSharedObjectForNewUser(_user.Id, _user.Email);

                return _user;

            }
            catch
            {
                throw;
            }

        }



        public static bool IsUserExists(string email)
        {
            return new UserRepository().IsUserExists(email);
        }


    }
}
