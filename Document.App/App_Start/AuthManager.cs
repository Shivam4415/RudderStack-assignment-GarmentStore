using Document.Library.Entity;
using Document.Library.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Security;

namespace Document.App.App_Start
{
    public static class AuthManager
    {

        public static UserSession CurrentSession
        {
            get
            {
                try
                {
                    if ((HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName] == null))
                        return null;

                    FormsAuthenticationTicket authTicket;
                    try
                    {
                        authTicket = FormsAuthentication.Decrypt(HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName].Value);
                    }
                    catch
                    {
                        return null;
                    }

                    if (string.IsNullOrEmpty(authTicket.UserData))
                        return null;

                    return AuthServices.GetUserSession(authTicket.UserData);
                }
                catch
                {
                    throw;
                }
            }
        }
        public static UserProfile CurrentUser
        {
            get
            {
                try
                {
                    if (HttpContext.Current?.User?.Identity == null)
                        return null;

                    if (!HttpContext.Current.User.Identity.IsAuthenticated)
                        return null;

                    if (!string.IsNullOrWhiteSpace(HttpContext.Current.User.Identity.Name))
                    {
                        return AuthServices.GetUserProfile(HttpContext.Current.User.Identity.Name);
                    }
                    else
                    {
                        UserSession session = CurrentSession;
                        if (session == null)
                            return null;

                        return AuthServices.GetUserProfile(session.Email);
                    }
                }
                catch
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Future Use
        /// </summary>
        /// <param name="user"></param>
        /// <param name="isPersistent"></param>
        /// <returns></returns>
        public static HttpCookie SignIn(UserProfile user, bool isPersistent = false)
        {
            try
            {
                UserSession session = new UserSession();
                session.Id = Guid.NewGuid();
                session.LastAccess = DateTime.UtcNow;
                session.Email = user.Email;
                session.IsPersistent = isPersistent;
                session.UserId = user.Id;
                session.LastCheckAccessToken = DateTime.UtcNow;


                string _logContent = AuthServices.SetUserSession(session);


                FormsAuthenticationTicket authTicket = null;

                if (session.IsPersistent)
                    authTicket = new FormsAuthenticationTicket(1, user.Email.ToLower(), DateTime.UtcNow, DateTime.UtcNow.AddDays(30), true, session.Id.ToString());
                else
                    authTicket = new FormsAuthenticationTicket(1, user.Email.ToLower(), DateTime.UtcNow, DateTime.UtcNow.Add(FormsAuthentication.Timeout), false, session.Id.ToString());

                HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(authTicket));
                authCookie.Expires = authTicket.Expiration;
                authCookie.Domain = FormsAuthentication.CookieDomain;
                authCookie.HttpOnly = true;
                authCookie.Secure = FormsAuthentication.RequireSSL;
                return authCookie;

                //if (!string.IsNullOrEmpty(_logContent))
                //    EventLogger.Write(_logContent, LogEventType.SignOut, user.AccountId, user.Id);

                //EventLogger.Write("Success|Mirror:" + (user.AllowMirrorSessions) + "|" + session.Id, LogEventType.Authenticated, user.AccountId, user.Id);

            }
            catch
            {
                throw;
            }
        }


        public static void SignOut()
        {
            try
            {
                UserSession session = CurrentSession;
                if (session != null)
                {
                    AuthServices.RemoveUserSession(session.Id);
                }
                if (HttpContext.Current.Session != null)
                    HttpContext.Current.Session.Clear();

                FormsAuthentication.SignOut();

                HttpContext.Current.Response.Redirect(FormsAuthentication.LoginUrl, false);
                return;
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Redirect(FormsAuthentication.LoginUrl, false);
            }
        }

        /// <summary>
        /// Signout Function called on Global to remove unvalidated session
        /// </summary>
        /// <param name="SuppressFormsAuthenticationRedirect"></param>
        /// <param name="sessionId"></param>
        public static void SignOut(string sessionId, bool SuppressFormsAuthenticationRedirect = false, string logMessage = null)
        {
            try
            {
                UserSession session = null;
                if (!string.IsNullOrWhiteSpace(sessionId))
                    session = AuthServices.GetUserSession(sessionId);

                if (session != null)
                {
                    AuthServices.RemoveUserSession(session.Id);
                }

                //EventLogger.Write("TimeOut|" + session?.Id == null ? "Null Session Id " : session?.Id.ToString() + "|" + sessionId + "|" + logMessage, LogEventType.SignOut, session?.AccountId, session?.UserId);


                if (HttpContext.Current.Session != null)
                    HttpContext.Current.Session.Clear();

                FormsAuthentication.SignOut();


                if (SuppressFormsAuthenticationRedirect)
                {
                    HttpContext.Current.Response.SuppressFormsAuthenticationRedirect = true;
                    HttpContext.Current.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                }
                else
                    FormsAuthentication.RedirectToLoginPage();
            }
            catch (Exception ex)
            {
                //EventLogger.Write(ex);
            }
        }


        /// <summary>
        /// When User update his profile 
        /// </summary>
        /// <param name="email"></param>
        public static void UpdateAuthTicket(string email)
        {
            try
            {
                HttpCookie authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);

                if (!(authTicket == null && authTicket.Expired))
                {
                    FormsAuthenticationTicket newAuthTicket = new FormsAuthenticationTicket(authTicket.Version, email.ToLower(), authTicket.IssueDate, authTicket.Expiration, authTicket.IsPersistent, authTicket.UserData);

                    UserSession session = AuthServices.GetUserSession(authTicket.UserData);

                    session.Email = email;

                    AuthServices.UpdateUserSessionCache(session);

                    authCookie.Value = FormsAuthentication.Encrypt(newAuthTicket);
                    authCookie.Expires = authTicket.Expiration;
                    authCookie.Domain = FormsAuthentication.CookieDomain;
                    HttpContext.Current.Response.Cookies.Set(authCookie);
                }
            }
            catch (Exception ex)
            {
                //EventLogger.Write(ex);
            }

        }


    }

}