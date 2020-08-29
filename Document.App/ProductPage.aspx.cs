using Document.App.App_Start;
using Document.Library.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Document.App
{
    public partial class ProductPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UserProfile user = AuthManager.CurrentUser;
            meObject.Value = user.Id.ToString();


        }

        protected void OnSignOut(object sender, EventArgs a)
        {
            try
            {
                AuthManager.SignOut();
            }
            catch
            {
                Response.Redirect(FormsAuthentication.LoginUrl, false);
            }

        }


    }
}