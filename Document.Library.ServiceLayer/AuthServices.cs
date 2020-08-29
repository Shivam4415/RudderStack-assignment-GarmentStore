using Document.Library.Entity;
using Document.Library.Globals;
using Document.Library.Repo.Repository;
using System;
using System.Text;
using System.Web;
using System.Web.Security;

namespace Document.Library.ServiceLayer
{
    public static class AuthServices
    {


        public static UserProfile GetUserProfile(string email, bool cached = true)
        {
            try
            {
                //if(cached ==false)
                //    return new AuthRepository().GetUserProfile(email);
                UserProfile _user = null;

                if (cached)
                    _user = CacheManager.Get(email, "Profile") as UserProfile;

                if (_user != null)
                    return _user;

                _user = new AuthRepository().GetUserProfile(email);

                if (_user != null)
                {
                    CacheManager.Set(email, _user, "Profile");
                }

                return _user; ;
            }
            catch
            {
                throw;
            }
        }


        public static UserSession GetUserSession(string sessionId)
        {
            try
            {
                UserSession _session = CacheManager.Get(sessionId.ToString(), "Identity") as UserSession;

                if (_session != null)
                    return _session;

                Guid _sessionid;

                if (Guid.TryParse(sessionId, out _sessionid))
                {
                    _session = new AuthRepository().GetUserSession(_sessionid);

                    if (_session != null)
                    {
                        CacheManager.Set(sessionId, _session, "Identity");
                    }

                    return _session;
                }

                return null;
            }
            catch
            {
                throw;
            }
        }


        public static void RemoveUserSession(Guid sessionId)
        {
            try
            {
                CacheManager.Remove(sessionId.ToString(), "Identity");
                new AuthRepository().RemoveUserSession(sessionId);
            }
            catch
            {
                throw;
            }
        }

        public static void SlideExpiration(UserSession currentSession)
        {
            try
            {
                if (currentSession != null)
                {
                    if ((DateTime.UtcNow - currentSession.LastAccess).TotalSeconds > 60)
                    {
                        currentSession.LastAccess = DateTime.UtcNow;

                        new AuthRepository().SlideUserSessionExpiration(currentSession.Id, currentSession.LastAccess, currentSession.LastCheckAccessToken);

                    }

                }
            }
            catch
            {
                throw;
            }
        }

        public static string SetUserSession(UserSession session)
        {
            try
            {
                UserSession list = new AuthRepository().SetUserSession(session);

                CacheManager.Set(session.Id.ToString(), session, "Identity");

                if (session == null)
                    return string.Empty;

                StringBuilder _log = new StringBuilder();

                _log.Append(session.Id.ToString() + "|");

                //CacheManager.Remove(session.Id.ToString(), "Identity");

                return _log.ToString();
            }
            catch
            {
                throw;
            }
        }

        public static void UpdateUserSessionCache(UserSession currentSession)
        {
            try
            {
                if (currentSession != null)
                {
                    CacheManager.Set(currentSession.Id.ToString(), currentSession, "Identity");

                }
            }
            catch
            {
                throw;
            }
        }

        public static void UpdateUserSession(UserSession session)
        {
            try
            {
                new AuthRepository().UpdateUserSession(session);
                UpdateUserSessionCache(session);
            }
            catch
            {
                throw;
            }
        }




        public static UserProfile UpdateUserProfileCache(string email)
        {
            try
            {
                UserProfile _user = new AuthRepository().GetUserProfile(email);

                if (_user != null)
                {
                    CacheManager.Set(email, _user, "Profile");
                }

                return _user; ;
            }
            catch
            {
                throw;
            }
        }

        public static void RemoveUserProfileCache(string email)
        {
            try
            {
                CacheManager.Remove(email, "Profile");
            }
            catch { throw; }
        }

        public static UserProfile GetUserProfile(Guid userId)
        {
            try
            {
                return new AuthRepository().GetUserProfile(userId);
            }
            catch
            {
                throw;
            }
        }


        public static UserProfile GetUser(string email)
        {
            try
            {
                UserProfile user = new UserProfile();
                //get user from repo
                user.Name = "Shivam";
                user.Email = "shivam441";
                user.Password = "123456";
                return user;
            }
            catch
            {
                throw;
            }
        }


        public static HttpCookie SignIn(UserProfile user)
        {
            try
            {
                UserSession session = new UserSession();
                Random random = new Random();
                session.Id = new Guid();
                session.LastAccess = DateTime.UtcNow;
                session.Email = user.Email;
                session.UserId = user.Id;
                session.IsPersistent = false;


                //Call db to save user session

                FormsAuthenticationTicket ticket = null;

                if (session.IsPersistent)
                {
                    ticket = new FormsAuthenticationTicket(1, user.Email, DateTime.UtcNow, DateTime.UtcNow.AddDays(30), true, session.Id.ToString());
                }
                else
                {
                    ticket = new FormsAuthenticationTicket(1, user.Email, DateTime.UtcNow, DateTime.UtcNow.Add(FormsAuthentication.Timeout), false, session.Id.ToString());
                }


                HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));
                authCookie.Domain = FormsAuthentication.CookieDomain;
                authCookie.HttpOnly = true;
                authCookie.Secure = FormsAuthentication.RequireSSL;

                return authCookie;

            }
            catch
            {
                throw;
            }
        }


    }

}
