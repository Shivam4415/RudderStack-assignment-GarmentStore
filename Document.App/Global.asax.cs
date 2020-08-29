using Document.App.App_Start;
using Document.Library.Entity;
using Document.Library.Globals.Global;
using Document.Library.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace Document.App
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            GlobalConnection.Editor = ConfigurationManager.ConnectionStrings["Editor"].ToString();

            RouteConfig.RegisterRoutes(RouteTable.Routes);

        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected bool ByPassFormsAuthentication(string absolutePath, string query)
        {
            if (absolutePath.Equals("/404") || absolutePath.Equals("/404.aspx"))
                return true;

            if ((absolutePath.Equals("/register.aspx") || absolutePath.Equals("/register") || absolutePath.Equals("/confirm")))
            {
                //Response.Redirect(FormsAuthentication.LoginUrl + "?" + Request.QueryString, false);
                return true;
            }

            if (absolutePath.Equals(FormsAuthentication.LoginUrl) || absolutePath.Equals(FormsAuthentication.LoginUrl + ".aspx"))
                return true;

            return false;
        }


        protected void FormsAuthentication_OnAuthenticate(Object sender, FormsAuthenticationEventArgs e)
        {
            try
            {
                
                string absolutePath = Request.Url.AbsolutePath.ToLower();

                if (absolutePath.Equals("/"))
                {
                    Response.Redirect(FormsAuthentication.LoginUrl, false);
                    return;
                }
                bool ajaxRequest = false;

                if (absolutePath.ToLower().Contains("/api/"))
                {
                    ajaxRequest = true;
                }

                string query = Request.Url.Query.ToLower();

                if (Request.Cookies[FormsAuthentication.FormsCookieName] == null)
                {
                    if (ajaxRequest == true)
                    {
                        Response.SuppressFormsAuthenticationRedirect = true;
                    }
                    else
                    {
                        if (ByPassFormsAuthentication(absolutePath, query))
                            return;
                    }

                    Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    Context.ApplicationInstance.CompleteRequest();
                    return;

                }

                bool SuppressFormsAuthenticationRedirect = ajaxRequest;

                FormsAuthenticationTicket authTicket = null;
                try
                {
                    authTicket = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value);
                }
                catch
                {
                    AuthManager.SignOut(string.Empty, ajaxRequest, "AuthTicketException");
                    return;
                }

                if (authTicket.Expired)
                {
                    AuthManager.SignOut(authTicket.UserData, ajaxRequest, "AuthTicketExpired");
                    return;
                }

                UserProfile user = AuthServices.GetUserProfile(authTicket.Name);
                if (user == null)
                {
                    AuthManager.SignOut(authTicket.UserData, ajaxRequest, "UserNotFound");
                    return;
                }

                UserSession session = AuthServices.GetUserSession(authTicket.UserData);
                if (session == null)
                {
                    if (absolutePath.Equals(FormsAuthentication.LoginUrl) || absolutePath.Equals(FormsAuthentication.LoginUrl + ".aspx"))
                    {
                        return;
                    }

                    AuthManager.SignOut(authTicket.UserData, ajaxRequest, "SessionTimeOut");
                    return;
                }

                if (session.LastAccess.Add(FormsAuthentication.Timeout) > DateTime.UtcNow)
                {
                    if (Request.HttpMethod == "GET" || Request.HttpMethod == "HEAD")
                    {
                        AuthServices.SlideExpiration(session);
                    }

                    AuthServices.SlideExpiration(session);

                    if (RedirectToDefaultUrl(absolutePath))
                        Response.Redirect(FormsAuthentication.DefaultUrl, false);

                    return;
                }

                if (authTicket.IsPersistent && session.LastAccess.AddDays(30) > DateTime.UtcNow)
                {
                    // Updating user cache with this call in case user has logged with persistent and has a inactive session.
                    user = AuthServices.GetUserProfile(authTicket.Name, false);
                    if (user.HasVerifiedEmail == false)
                    {
                        AuthManager.SignOut(authTicket.UserData, ajaxRequest, "PersistentNonVerifiedEmail");
                        return;

                    }

                    if (Request.HttpMethod == "GET" || Request.HttpMethod == "HEAD")
                    {
                        AuthServices.SlideExpiration(session);
                    }

                    AuthServices.SlideExpiration(session);

                    if (RedirectToDefaultUrl(absolutePath))
                        Response.Redirect(FormsAuthentication.DefaultUrl, false);

                    return;
                }

                AuthManager.SignOut(authTicket.UserData, ajaxRequest, "NotValidSession");
            }
            catch (Exception _ex)
            {
                //EventLogger.Write(_ex);
                //AuthManager.SignOut();
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(""));
                return;
            }
        }

        public bool RedirectToDefaultUrl(string absolutePath)
        {
            if (absolutePath.Equals(FormsAuthentication.LoginUrl) || absolutePath.Equals(FormsAuthentication.LoginUrl + ".aspx"))
                return true;

            if (absolutePath.Equals("/"))
                return true;

            if (absolutePath.Equals("/passwordreset.aspx") || absolutePath.Equals("/passwordreset"))
                return true;

            if (absolutePath.Equals("/forgotpassword.aspx") || absolutePath.Equals("/forgotpassword") || absolutePath.Equals("/forgot-password"))
                return true;

            return false;
        }



        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}