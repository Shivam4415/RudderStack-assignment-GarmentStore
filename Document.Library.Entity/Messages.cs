using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document.Library.Entity
{
    public static class Messages
    {

        public static string RequestForbidden { get { return "Fobidden"; } }

        public static string InvalidCredentials { get { return "Invalid Credentials"; } }

        public static string InvalidRequest { get { return "Invalid Request Body. Please check body parameter"; } }

        public static string InvalidEmail { get { return "Invalid Email Combination"; } }

        public static string InvalidPassword { get { return "Invalid Password Combination"; } }

        public static string RegisterIfEmailExists { get { return "This email is already registered."; } }

        public static string InternalServerError { get { return "Internal Server Error. Please contact support if you encounter this error continuously."; } }

        public static string NotFound { get { return "Resource Not Found."; } }

        //////////////////////////////////////
        ///

    }


}
