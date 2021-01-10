using Document.App.App_Start;
using Document.Library.Entity;
using Document.Library.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Document.App
{
    public partial class LoginPage : System.Web.UI.Page
    {
        protected void button_SignUp(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("/register", false);
            }
            catch (Exception ex)
            {
                Response.Redirect("Error.aspx");
            }
        }

        protected void btnLogin(object sender, EventArgs e)
        {
            try
            {
                string email = txtEmail.Value;
                string password = txtPassword.Value;

                UserProfile user = AuthServices.GetUserProfile(email, false);

                if (user.Password != password)
                {
                    MessageBox.InnerText = "The email and password you entered did not match our record";
                    MessageBox.Visible = true;
                    return;
                }

                HttpCookie cookie = AuthManager.SignIn(user, true);
                Response.SetCookie(cookie);

                string script = "<script type='text/javascript'>window.onload = function () {M.DomUtil.getBrowserForIe();}; </script>";
                ClientScript.RegisterStartupScript(GetType(), "Load", script);

                Page.ClientScript.RegisterStartupScript(Page.GetType(), "identify", "M.identifyUser(" + user.Id +", " + user + ")", true);
                Response.Redirect(FormsAuthentication.DefaultUrl, false);

            }
            catch
            {
                Response.Redirect("Error.aspx");
            }
        }
    }
}